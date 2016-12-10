using System.Windows;
using System.Collections.Generic;
using guitarTools.Classes.Wheel_Select;
using guitarTools.Classes.Fretboard;

namespace guitarTools
{
    /// <summary>
    /// Contains the window logic.
    /// Creates UI elements with default settings upon initiation.
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            #region Defining variables
            ushort frets = 12*2;
            ushort strings = 7;
            List<List<FretNote>> NoteList = new List<List<FretNote>>(); // Row is a string, column is a fret
            #endregion

            // Creating default fretboard
            Fretboard fretboard = new Fretboard(mainGrid, noteGrid, strings, frets, NoteList, 4);
            
            fretboard.UpdateRoot(11);

            // TODO Delete test elements
            // WheelButton a = new WheelButton(uiC1);
            // WheelButton b = new WheelButton(uiC2);
        }
    }
}