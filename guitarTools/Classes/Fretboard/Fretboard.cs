using System;
using System.Threading;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading.Tasks;

namespace guitarTools.Classes.Fretboard
{
    // TODO Write documentation for Fretboard class
    class Fretboard
    {
        #region Properties
        private static List<List<FretNote>> NoteList { get; set; }
        private static Grid MainGrid { get; set; }
        private static Grid NoteGrid { get; set; }
        private static ushort Strings { get; set; }
        private static ushort Frets { get; set; }
        private static double Width { get; set; }
        private static double Height { get; set; }
        private static double Size { get; set; }
        private static int Tuning { get; set; }
        #endregion

        public Fretboard(Grid mainGrid, Grid noteGrid, ushort strings, ushort frets, List<List<FretNote>> noteList, int tuning)
        {
            // Assigning values to properties
            MainGrid = mainGrid;
            NoteGrid = noteGrid;
            Strings = strings;
            Frets = frets;
            Width = mainGrid.Width / frets;
            Height = mainGrid.Height / strings;
            Size = Width <= Height ? Width : Height;
            NoteList = noteList;
            Tuning = tuning;



            // Creating the fretboard            
            CreateGrid();
            CreateNotes();
            CreateMarkers();         
            DrawFretboard();
        }

        private static void CreateGrid()
        {
            // Adding columns and rows to grid
            for (ushort i = 0; i <= Frets; i++) NoteGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (ushort i = 0; i <= Strings; i++) NoteGrid.RowDefinitions.Add(new RowDefinition());
        }

        private static void CreateNotes()
        {
            string[] data = SQLCommands.FetchList<string>("SELECT Interval FROM tableTuning WHERE Id = " + 10, 1)[0].Split(' ');

            // Creating notes and adding them to the grid
            for (int numString = 0; numString < Strings; numString++)
            {
                List<FretNote> tempNoteList = new List<FretNote>();
                int index = int.Parse(data[numString]);

                for (int numFret = 0; numFret <= Frets; numFret++)
                {
                    IntLimited key = new IntLimited(numFret + Tuning, 0, 12);
                    key.GetValue = key + index;

                    FretNote note = new FretNote(key.GetValue, Size * 0.8, true, new Point(numFret, numString), NoteGrid);
                    tempNoteList.Add(note);
                }

                NoteList.Add(tempNoteList);
            }         
        }

        private static void CreateMarkers()
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

        private static void DrawFretboard()
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
    }
}
