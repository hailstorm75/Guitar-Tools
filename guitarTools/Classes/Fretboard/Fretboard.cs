using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace guitarTools.Classes.Fretboard
{
    class Fretboard
    {
        #region SQL Setup
        SqlConnection sqlConn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\hp\Documents\Projects\Visual Studio\C#\guitarToolsForm\guitarTools\SQL\Data.mdf';Integrated Security=True;Connect Timeout=30");
        SqlCommand sqlComm = new SqlCommand();
        SqlDataReader sqlRead;
        #endregion

        public List<List<FretNote>> NoteList { get; set; }
        private Grid MainGrid { get; set; }
        private Grid NoteGrid { get; set; }
        private ushort Strings { get; set; }
        private ushort Frets { get; set; }
        private double Width { get; set; }
        private double Height { get; set; }
        private double Size { get; set; }
        private int Tuning { get; set; }
        
        public Fretboard(Grid mainGrid, Grid noteGrid, ushort strings, ushort frets, List<List<FretNote>> noteList, int tuning)
        {
            MainGrid = mainGrid;
            NoteGrid = noteGrid;
            Strings = strings;
            Frets = frets;
            Width = mainGrid.Width / frets;
            Height = mainGrid.Height / strings;
            Size = Width <= Height ? Width : Height;
            NoteList = noteList;
            Tuning = tuning;

            CreateGrid();
            CreateNotes();
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
            string[] data = FetchData<string>("SELECT Interval FROM tableTuning WHERE Id = " + 10, 1)[0].Split(' ');

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
                    Y2 = NoteGrid.Height - NoteGrid.Height / Strings,
                    StrokeThickness = i != 1 ? 1 : 2, // Inline IF ELSE
                    Stroke = Brushes.Black
                };
                MainGrid.Children.Add(fret);
            }
        }

        private List<T> FetchData<T>(string command, ushort columnCount)
        {
            // Opening SQL connection and setting up  
            #region SQL Setup
            sqlConn.Open();
            sqlComm.Connection = sqlConn;
            sqlComm.CommandText = command;
            sqlRead = sqlComm.ExecuteReader();
            #endregion

            // Defining generic list
            List<T> data = new List<T>();

            // Executes only if table has content
            #region Data extraction
            if (sqlRead.HasRows)
            {
                while (sqlRead.Read())
                {
                    for (int column = 0; column < columnCount; column++)
                        data.Add((T)Convert.ChangeType(sqlRead[column], typeof(T))); // Converts to selected generic type and appends to list
                }
            }
            else MessageBox.Show("ERROR: No rows");
            #endregion

            // Closing SQL connection
            sqlConn.Close();

            return data;
        }
    }
}
