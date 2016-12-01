using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace guitarTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        #region SQL Setup
        SqlConnection sqlConn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\hp\Documents\Projects\Visual Studio\C#\guitarToolsForm\guitarTools\SQL\Data.mdf';Integrated Security=True;Connect Timeout=30");
        SqlCommand sqlComm = new SqlCommand();
        SqlDataReader sqlRead;
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            #region Defining variables
            ushort frets = 12*2; ushort strings = 7;

            double width = mainGrid.Width / frets;
            double height = mainGrid.Height / strings;
            double size = width <= height ? width : height;

            List<List<FretNote>> NoteList = new List<List<FretNote>>();
            #endregion

            #region Creating interface
            CreateGrid(ref frets, ref strings);
            CreateNotes(ref frets, ref strings, ref size, 4, ref NoteList);
            DrawFretboard(ref width, ref height, ref frets, ref strings, ref size);
            #endregion
        }

        #region UI Construction
        private void CreateGrid( ref ushort frets, ref ushort strings)
        {
            // Adding columns and rows to grid
            for (ushort i = 0; i <= frets; i++) noteGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (ushort i = 0; i <= strings; i++) noteGrid.RowDefinitions.Add(new RowDefinition());
        }

        private void CreateNotes(ref ushort frets, ref ushort strings, ref double size, int tuning, ref List<List<FretNote>> NoteList)
        {
            string[] data = FetchData<string>("SELECT Interval FROM tableTuning WHERE Id = " + 10,1)[0].Split(' ');
            
            // Creating notes and adding them to the grid
            for (int numString = 0; numString < strings; numString++)
            {
                List<FretNote> tempNoteList = new List<FretNote>();
                int index = int.Parse(data[numString]);

                for (int numFret = 0; numFret <= frets; numFret++)
                {
                    IntLimited key = new IntLimited(numFret + tuning, 0, 12);
                    key.GetValue = key + index;

                    FretNote note = new FretNote(key.GetValue, size*0.8, true, new Point(numFret, numString), ref noteGrid);
                    tempNoteList.Add(note);         
                }

                NoteList.Add(tempNoteList);
            }
            
            // Creating fret markers and adding them to the grid
            for (int x = 3, y = 1; x < frets; x += 2, y++)
            {
                FretMarker marker;
                if (y % 5 != 0)
                {
                    marker = new FretMarker(noteGrid, new Point(x, strings), x.ToString());
                }
                else
                {
                    marker = new FretMarker(noteGrid, new Point(x + 1, strings), (12 * (y / 5)).ToString());
                    x += 2;
                }
            }
        }

        private void DrawFretboard(ref double width, ref double height, ref ushort frets, ref ushort strings, ref double size)
        {
            noteGrid.Height = size * strings;

            // Drawing the frets
            for (ushort i = 1; i <= frets; i++)
            {
                Line fret = new Line()
                {
                    X1 = mainGrid.Width / (frets + 1) * i,
                    X2 = mainGrid.Width / (frets + 1) * i,
                    Y1 = 0,
                    Y2 = noteGrid.Height - noteGrid.Height / strings,
                    StrokeThickness = i != 1 ? 1 : 2, // Inline IF ELSE
                    Stroke = Brushes.Black
                };
                mainGrid.Children.Add(fret);
            }          
        }
        #endregion

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