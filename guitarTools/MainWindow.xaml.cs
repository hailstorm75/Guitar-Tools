using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;
using guitarTools.Classes.Wheel_Select;
using guitarTools.Classes.Fretboard;

namespace guitarTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            #region Defining variables
            ushort frets = 12*2; ushort strings = 7;

            List<List<FretNote>> NoteList = new List<List<FretNote>>();
            #endregion

            Fretboard fretboard = new Fretboard(mainGrid, noteGrid, strings, frets, NoteList, 4);

            WheelButton a = new WheelButton(uiC1);
            WheelButton b = new WheelButton(uiC2);
        }
    }
}