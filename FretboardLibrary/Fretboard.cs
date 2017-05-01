using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ServicesLibrary;
using System.Xml.Linq;

namespace FretboardLibrary
{
    /// <summary>
    /// Fretboard creates and manages fret notes, markers and their grid
    /// </summary>

    public class Fretboard
    {
        #region Properties
        private XDocument Doc { get; set; }
        private List<List<FretNote>> NoteList { get; set; }
        private Grid MainGrid { get; set; }
        private Grid NoteGrid { get; set; }
        public ushort Strings { get; set; }
        public ushort Frets { get; set; }
        private double Width { get; set; }
        private double Height { get; set; }
        private double Size { get; set; }
        public int Root { get; set; }
        public string Tuning { get; set; }
        public string Scale { get; set; }
        #endregion

        public Fretboard(Grid mainGrid, ushort strings, ushort frets, List<List<FretNote>> noteList, int root, string tuning, string scale)
        {
            // Assigning values to properties
            MainGrid = mainGrid;

            NoteGrid = new Grid();
            NoteGrid.VerticalAlignment = VerticalAlignment.Top;
            NoteGrid.ShowGridLines = false;
            MainGrid.Children.Add(NoteGrid);

            Doc = new XDocument(XDocument.Load(@"C:\Users\Denis\Documents\Visual Studio 2017\Projects\Guitar-Tools\guitarTools\Data\Data.xml"));
            Strings = strings;
            Frets = frets;
            Width = mainGrid.Width / frets;
            Height = mainGrid.Height / strings;
            Size = Width <= Height ? Width : Height;
            NoteList = noteList;
            Root = root;
            Tuning = tuning;
            Scale = scale;

            // Creating the fretboard            
            CreateGrid();
            CreateNotes();
            CreateMarkers();
            DrawFretboard();
        }

        private void CreateGrid()
        {
            // Adding columns and rows to grid
            for (ushort i = 0; i <= Frets; i++) NoteGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (ushort i = 0; i <= Strings; i++) NoteGrid.RowDefinitions.Add(new RowDefinition());
        }

        private void CreateNotes()
        {
            // Passing selected tuning from database to array
            string[] tuning = (from node in Doc.Descendants("Tunings").Elements("Tuning")
                               where node.Element("Name").Value == Tuning && node.Attribute("strings").Value == Strings.ToString()
                               select node.Element("Interval").Value).Single().Split(' ');

            string[] scale = (from node in Doc.Descendants("Scales").Elements("Scale")
                              where node.Element("Name").Value == Scale
                              select node.Element("Interval").Value).Single().Split(' ');


            // Creating notes and adding them to the grid
            for (int numString = 0; numString < Strings; numString++)
            {
                List<FretNote> tempNoteList = new List<FretNote>();
                int index = int.Parse(tuning[numString]);     // Getting individual string tunning

                for (int numFret = 0; numFret <= Frets; numFret++)
                {
                    // Calculating note order based on tuning and root note
                    IntLimited key = new IntLimited(numFret, 0, 12);
                    key.Value = key + index;

                    IntLimited a = new IntLimited(key.Value - Root, 0, 12);

                    //MessageBox.Show(a.Value.ToString());

                    // Checking if note fits scale                  
                    bool IsActive = scale.Contains(a.Value.ToString()) ? true : false;

                    // Creating the note
                    FretNote note = new FretNote(key.Value, Size * 0.8, IsActive, Root, new Point(numFret, numString), NoteGrid);
                    tempNoteList.Add(note);
                }

                NoteList.Add(tempNoteList); // Generating list of notes for future reference
            }
        }

        private void CreateMarkers()
        {
            // Creating fret markers and adding them to the grid
            for (int x = 3, y = 1; x < Frets; x += 2, y++)
            {
                FretMarker marker;
                if (y % 5 != 0)
                {
                    marker = new FretMarker(NoteGrid, new Point(x, Strings), x.ToString());
                }
                else
                {
                    marker = new FretMarker(NoteGrid, new Point(x + 1, Strings), (12 * (y / 5)).ToString());
                    x += 2;
                }
            }
        }

        private void DrawFretboard()
        {
            NoteGrid.Height = Size * Strings;

            // Drawing the frets
            for (ushort i = 1; i <= Frets; i++)
            {
                Line fret = new Line()
                {
                    X1 = MainGrid.Width / (Frets + 1) * i,
                    X2 = MainGrid.Width / (Frets + 1) * i,
                    Y1 = 0,
                    Y2 = Size * Strings - Size,
                    StrokeThickness = i != 1 ? 1 : 2, // Inline IF ELSE
                    Stroke = Brushes.Black
                };
                MainGrid.Children.Add(fret);
            }
        }

        public void UpdateRoot(int newRoot)
        {
            if (newRoot != Root)
            {
                Root = newRoot;

                string[] scale = (from node in Doc.Descendants("Scales").Elements("Scale")
                                  where node.Element("Name").Value == Scale
                                  select node.Element("Interval").Value).Single().Split(' ');

                // Shifting scale to new root note
                foreach (List<FretNote> String in NoteList)
                {
                    foreach (FretNote Note in String)
                    {
                        IntLimited a = new IntLimited(Note.Index - Root, 0, 12);
                        Note.ChangeState(scale.Contains((a.Value).ToString()) ? true : false);
                        Note.HighlightRoot(Root);
                    }
                }
            }
        }

        public void UpdateScale(string newScale)
        {
            Scale = newScale;
            string[] scale = (from node in Doc.Descendants("Scales").Elements("Scale")
                              where node.Element("Name").Value == Scale
                              select node.Element("Interval").Value).Single().Split(' ');

            // Shifting scale to new root note
            foreach (List<FretNote> String in NoteList)
            {
                foreach (FretNote Note in String)
                {
                    IntLimited a = new IntLimited(Note.Index - Root, 0, 12);
                    Note.ChangeState(scale.Contains((a.Value).ToString()) ? true : false);
                }
            }
        }

        public void UpdateTuning(string newTuning)
        {
            // TODO Clean up
            Tuning = newTuning;

            string[] scale = (from node in Doc.Descendants("Scales").Elements("Scale")
                              where node.Element("Name").Value == Scale
                              select node.Element("Interval").Value).Single().Split(' ');
            string[] tuning = (from node in Doc.Descendants("Tunings").Elements("Tuning")
                               where node.Element("Name").Value == Tuning && node.Attribute("strings").Value == Strings.ToString()
                               select node.Element("Interval").Value).Single().Split(' ');
        
            for (int numString = 0; numString < NoteList.Count; numString++)
            {
                int index = int.Parse(tuning[numString]);     // Getting individual string tunning

                for (int numFret = 0; numFret < NoteList[numString].Count; numFret++)
                {
                    // Calculating note order based on tuning and root note
                    IntLimited key = new IntLimited(numFret, 0, 12);
                    key.Value = key + index;

                    // Shifting the note
                    NoteList[numString][numFret].ShiftTuning(key.Value);

                    // Checking if note fits scale
                    NoteList[numString][numFret].ChangeState(scale.Contains(new IntLimited(key.Value - Root, 0, 12).Value.ToString()) ? true : false);

                    // Highlighting the root
                    NoteList[numString][numFret].HighlightRoot(Root);
                }
            }
        }

        public void ClearNotes()
        {
            NoteGrid.Children.Clear();
            MainGrid.Children.Clear();
            NoteList.Clear();
        }
    }
}