using System.Windows;
using System.Collections.Generic;
using guitarTools.Classes.Wheel_Select;
using guitarTools.Classes.Fretboard;
using guitarTools.Classes;

namespace guitarTools
{
    /// <summary>
    /// Contains the window logic.
    /// Creates UI elements with default settings upon initiation.
    /// </summary>

    public partial class MainWindow : Window
    {
        Fretboard fretboard;
        public MainWindow()
        {
            InitializeComponent();

            #region Defining variables
            ushort frets = 12*2;
            ushort strings = 7;
            List<List<FretNote>> NoteList = new List<List<FretNote>>(); // Row is a string, column is a fret
            #endregion

            // Creating default fretboard
            fretboard = new Fretboard(mainGrid, noteGrid, strings, frets, NoteList, 4);

            foreach (var item in SQLCommands.FetchList<string>("SELECT Name FROM tableScales"))
                cbScale.Items.Add(item);
        }

        private void cbRoot_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (fretboard.Root != cbRoot.SelectedIndex)
                fretboard.UpdateRoot(cbRoot.SelectedIndex);
        }
    }
}