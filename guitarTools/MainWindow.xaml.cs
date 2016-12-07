using System.Windows;
using System.Collections.Generic;
using guitarTools.Classes.Wheel_Select;
using guitarTools.Classes.Fretboard;

namespace guitarTools
{
    public partial class MainWindow : Window
    {
        // TODO Write documentation for MainWindow

        public MainWindow()
        {
            InitializeComponent();

            #region Defining variables
            ushort frets = 12*2; ushort strings = 7;
            List<List<FretNote>> NoteList = new List<List<FretNote>>();
            #endregion

            Fretboard fretboard = new Fretboard(mainGrid, noteGrid, strings, frets, NoteList, 4);

            // TODO Delete test elements
            WheelButton a = new WheelButton(uiC1);
            WheelButton b = new WheelButton(uiC2);
        }
    }
}