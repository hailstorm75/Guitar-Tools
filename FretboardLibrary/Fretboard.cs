using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ServicesLibrary;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;

namespace FretboardLibrary
{
    /// <summary>
    /// Fretboard creates and manages fret notes, markers and their grid.
    /// </summary>
    public class Fretboard
    {
        #region Properties
        /// <summary>
        /// Used to access ../Data/Data.xml
        /// </summary>
        private XDocument Doc { get; set; }

        /// <summary>
        /// 2D list of fret notes and fret markers.
        /// </summary>
        private List<List<FretNote>> NoteList { get; set; }

        /// <summary>
        /// Parent grid.
        /// </summary>
        private Grid MainGrid { get; set; }

        /// <summary>
        /// Grid containing all fretboard elements.
        /// </summary>
        private Grid NoteGrid { get; set; }
        
        /// <summary>
        /// Number of fretboard strings.
        /// </summary>
        public ushort Strings { get; set; }

        /// <summary>
        /// Number of fretboard frets.
        /// </summary>
        public ushort Frets { get; set; }

        /// <summary>
        /// Width of the fretboard.
        /// </summary>
        private double Width { get; set; }

        /// <summary>
        /// Height of the fretboard.
        /// </summary>
        private double Height { get; set; }

        /// <summary>
        /// Size of fret notes. Property value is passed to FretNote constructor.
        /// </summary>
        private double Size { get; set; }

        /// <summary>
        /// Index of the root note.
        /// </summary>
        public int Root { get; set; }

        /// <summary>
        /// Name of the tuning.
        /// </summary>
        public string Tuning { get; set; }

        /// <summary>
        /// Name of the scale.
        /// </summary>
        public string Scale { get; set; }
        #endregion

        /// <summary>
        /// Assigns values to properties and calls methods which create the fretboard.
        /// </summary>
        /// <param name="mainGrid">Passes its value to MainGrid property</param>
        /// <param name="strings">Passes its value to Strings property</param>
        /// <param name="frets">Passes its value to Frets property</param>
        /// <param name="root">Passes its value to Root property</param>
        /// <param name="tuning">Passes its value to Tuning property</param>
        /// <param name="scale">Passes its value to Scale property</param>
        public Fretboard(Grid mainGrid, ushort strings, ushort frets, int root, string tuning, string scale)
        {
            // Assigning values to properties
            MainGrid = mainGrid;
            NoteList = new List<List<FretNote>>();

            NoteGrid = new Grid() { VerticalAlignment = VerticalAlignment.Top, ShowGridLines = false };
            MainGrid.Children.Add(NoteGrid);

            Doc = new XDocument(XDocument.Load(Directory.GetCurrentDirectory() + @"\Data\Data.xml"));
            Strings = strings;
            Frets = frets;
            Width = mainGrid.Width / frets;
            Height = mainGrid.Height / strings;
            Size = Width <= Height ? Width : Height;
            Root = root;
            Tuning = tuning;
            Scale = scale;

            // Creating the fretboard            
            CreateGrid();
            CreateNotes();
            CreateMarkers();
            DrawFretboard();
        }

        /// <summary>
        /// Adds columns and rows to the NoteGrid
        /// </summary>
        private void CreateGrid()
        {
            for (ushort i = 0; i <= Frets; i++) NoteGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (ushort i = 0; i <= Strings; i++) NoteGrid.RowDefinitions.Add(new RowDefinition());
        }

        /// <summary>
        /// Fetches tuning and scale intervals from the data file.
        /// Creates notes and fills the note list.
        /// </summary>
        private void CreateNotes()
        {
            // Passing selected tuning from database to array
            string[] tuning = (from node in Doc.Descendants("Tunings").Elements("Tuning")
                               where node.Element("Name").Value == Tuning && node.Attribute("strings").Value == Strings.ToString()
                               select node.Element("Interval").Value).Single().Split(' ');
            string[] tones = (from node in Doc.Descendants("Tunings").Elements("Tuning")
                              where node.Element("Name").Value == Tuning && node.Attribute("strings").Value == Strings.ToString()
                              select node.Element("Tone").Value).Single().Split(' ');
            string[] scale = (from node in Doc.Descendants("Scales").Elements("Scale")
                              where node.Element("Name").Value == Scale
                              select node.Element("Interval").Value).Single().Split(' ');

            // Creating notes and adding them to the grid
            for (int numString = 0; numString < Strings; numString++)
            {
                List<FretNote> tempNoteList = new List<FretNote>();
                
                int index = int.Parse(tuning[numString]);     // Getting individual string tunning
                int tone = int.Parse(tones[numString]);

                for (int numFret = 0; numFret <= Frets; numFret++)
                {
                    // Calculating note order based on tuning and root note
                    IntLimited key = new IntLimited(numFret, 0, 12);
                    key.Value = key + index;

                    IntLimited a = new IntLimited(key.Value - Root, 0, 12);

                    // Checking if note fits scale                  
                    bool IsActive = scale.Contains(a.Value.ToString()) ? true : false;

                    // Creating the note
                    FretNote note = new FretNote(key.Value, Size * 0.8, IsActive, Root, tone + numFret);
                    Grid.SetColumn(note, numFret);
                    Grid.SetRow(note, numString);
                    NoteGrid.Children.Add(note);

                    tempNoteList.Add(note);
                }

                NoteList.Add(tempNoteList); // Generating list of notes for future reference
            }
        }

        /// <summary>
        /// Creates fret markers.
        /// </summary>
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

        /// <summary>
        /// Draws fretboard nut and frets.
        /// </summary>
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

        /// <summary>
        /// Updates fret note's from the NoteList.
        /// Updates their state and highlight properties.
        /// </summary>
        /// <param name="newRoot">Passes its value to Root property</param>
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

        /// <summary>
        /// Updates fret note's from the NoteList.
        /// Updates their state property.
        /// </summary>
        /// <param name="newScale">Passes its values to Scale property</param>
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

        /// <summary>
        /// Updates fret note's from the NoteList.
        /// Updates their state and hightlight properties.
        /// Shifts their tunings.
        /// </summary>
        /// <param name="newTuning">Passes its value to Tuning property</param>
        public void UpdateTuning(string newTuning)
        {
            Tuning = newTuning;

            string[] scale = (from node in Doc.Descendants("Scales").Elements("Scale")
                              where node.Element("Name").Value == Scale
                              select node.Element("Interval").Value).Single().Split(' ');
            string[] tuning = (from node in Doc.Descendants("Tunings").Elements("Tuning")
                               where node.Element("Name").Value == Tuning && node.Attribute("strings").Value == Strings.ToString()
                               select node.Element("Interval").Value).Single().Split(' ');
            string[] tones = (from node in Doc.Descendants("Tunings").Elements("Tuning")
                              where node.Element("Name").Value == Tuning && node.Attribute("strings").Value == Strings.ToString()
                              select node.Element("Tone").Value).Single().Split(' ');

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

                    // Change tone
                    NoteList[numString][numFret].Tone = int.Parse(tones[numString]) + numFret;

                    // Checking if note fits scale
                    NoteList[numString][numFret].ChangeState(scale.Contains(new IntLimited(key.Value - Root, 0, 12).Value.ToString()) ? true : false);

                    // Highlighting the root
                    NoteList[numString][numFret].HighlightRoot(Root);
                }
            }
        }

        /// <summary>
        /// Clears the Fretboard and parent Control children.
        /// Clears the NoteList list.
        /// </summary>
        public void ClearNotes()
        {
            // NoteGrid.Children.Clear();
            MainGrid.Children.Clear();
            NoteList.Clear();
        }
    }
}