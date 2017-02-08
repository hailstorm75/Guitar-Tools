using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Collections.Generic;
using FretboardLibrary;
using ServicesLibrary;
using System.Windows.Media.Animation;
using System.Windows.Controls;

namespace GuitarScales
{
    /// <summary>
    /// Contains the window logic.
    /// Creates UI elements with default settings upon initiation.
    /// </summary>

    public partial class MainWindow : Window
    {
        Fretboard fretboard;

        #region Properties
        public bool Hidden { get; set; }
        public StackPanel SettingsPanel { get; set; }

        public bool init = false;
        public string[] MusicKeys = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        public ComboBox[,] Menu;
        public int Active = 2;
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            Hidden = true;
            SettingsPanel = null;

            if (!SQLCommands.CheckConnection())
            {
                MessageBox.Show(@"ERROR: Missing data file: Data.mdf
Make sure the application is in the same folder as the data file.

The application will now close.", "Guitar Tools");
                Close();
            }

            #region Defining variables
            ushort frets = 12*1;
            ushort strings = 6;
            List<List<FretNote>> NoteList = new List<List<FretNote>>(); // Row is a string, column is a fret
            #endregion

            // Creating default fretboard
            fretboard = new Fretboard(mainGrid, noteGrid, strings, frets, NoteList, 4, "Standard E", "Ionian");

            #region Setting up Controls
            // The root notes are constant - no need to fetch from database
            cbRoot.SelectedIndex = 4; // Setting default root note to "E"

            // Adding tunings from database
            foreach (var item in SQLCommands.FetchList<string>("SELECT Name FROM tableTuning WHERE Strings = " + strings))
                cbTuning.Items.Add(item);

            cbTuning.SelectedIndex = 0; // Setting default tuning to "Standard"

            // Adding scales from database
            foreach (var item in SQLCommands.FetchList<string>("SELECT Name FROM tableScales"))
                cbScale.Items.Add(item);

            cbScale.SelectedIndex = 0;
            #endregion

            #region Setting up Search scale
            foreach (var item in MusicKeys)
            {
                tbOne.Items.Add(item);
                tbTwo.Items.Add(item);
                tbThree.Items.Add(item);
            }

            string[] chords = SQLCommands.FetchList<string>("SELECT Name FROM tableChords").ToArray();

            foreach (var item in chords)
            {
                cbOne.Items.Add(item);
                cbTwo.Items.Add(item);
                cbThree.Items.Add(item);
            }

            Menu = new ComboBox[,] { { tbOne, tbTwo, tbThree }, { cbOne, cbTwo, cbThree } };

            init = true;
            #endregion
        }

        #region ComboBoxes
        public void cbRoot_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (fretboard.Root != cbRoot.SelectedIndex)
                fretboard.UpdateRoot(cbRoot.SelectedIndex);
        }

        private void cbTuning_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (fretboard.Tuning != cbTuning.SelectedValue.ToString())
                fretboard.UpdateTuning(cbTuning.SelectedValue.ToString());
        }

        public void cbScale_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (fretboard.Scale != cbScale.SelectedValue.ToString())
                fretboard.UpdateScale(cbScale.SelectedValue.ToString());
        }
        #endregion

        #region WindowEvents
        private void WindowTop_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (WindowState == WindowState.Maximized)
                {
                    WindowState = WindowState.Normal;
                    Top = 0;
                    Left = Mouse.GetPosition(this).X - ActualWidth / 2;
                }

                DragMove();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            //WindowStyle = WindowStyle.SingleBorderWindow;
            WindowState = WindowState.Minimized;
        }

        private void MainWindow_OnActivated(object sender, EventArgs e)
        {
            //change the WindowStyle back to None, but only after the Window has been activated
            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() => WindowStyle = WindowStyle.None));
        }
        #endregion

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb;

            if (Hidden)
            {
                sb = Resources["sbShowLeftMenu"] as Storyboard;
            }
            else
            {
                sb = Resources["sbHideLeftMenu"] as Storyboard;
            }

            sb.Begin(pnlLeftMenu);
            Hidden = !Hidden;
        }

        #region Settings Panels
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = Resources["sbShowSetting"] as Storyboard;
            string panelName = (sender as Button).Name.Replace("btn", "");
            SettingsPanel = (StackPanel)FindName(panelName);
            sb.Begin(SettingsPanel);
        }

        private void Settings_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (SettingsPanel != null && !SettingsPanel.IsMouseOver)
            {
                Storyboard sb = Resources["sbHideSetting"] as Storyboard;
                sb.Begin(SettingsPanel);
                SettingsPanel = null;
            }
        }
        #endregion

        #region Search Scale
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            lbResults.Items.Clear();

            List<int> chordNotes = new List<int>();

            int pointA = Array.IndexOf(MusicKeys, tbOne.SelectedItem);

            for (int row = 0; row < Menu.GetLength(1) - Active; row++)
            {
                string[] Chord = SQLCommands.FetchList<string>("SELECT Interval FROM tableChords WHERE Name = '" + Menu[1, row].SelectedValue + "'")[0].Split(' ');

                int pointB = Array.IndexOf(MusicKeys, Menu[1, row]);

                if (pointA - pointB != 0)
                {
                    IntLimited shiftBy = new IntLimited(pointA + pointB, 0, 12);
                }

                foreach (var item in Chord)
                    if (!chordNotes.Contains(int.Parse(item)))
                        chordNotes.Add(int.Parse(item));
            }

            chordNotes.Sort();

            string[] scales = SQLCommands.FetchList<string>("SELECT Interval FROM tableScales").ToArray();

            List<string> found = new List<string>();

            foreach (var item in scales)
            {
                for (int note = 0; note < chordNotes.Count; note++)
                {
                    if (item.IndexOf(note.ToString()) != -1)
                    {
                        if (note == chordNotes.Count - 1)
                        {
                            found.Add(item);
                        }
                    }
                    else break;
                }
            }

            if (found.Count > 0)
                foreach (var item in found)
                    lbResults.Items.Add(SQLCommands.FetchList<string>("SELECT Name FROM tableScales WHERE Interval = '" + item + "'")[0]);
            else lbResults.Items.Add("Unknown scale");
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (init)
            {
                btnSearch.IsEnabled = false;
                if (tbOne.SelectedIndex != 0)
                {
                    cbOne.IsEnabled = true;

                    if (cbOne.SelectedIndex != 0)
                    {
                        btnSearch.IsEnabled = true;
                        tbTwo.IsEnabled = true;
                        Active = 2;

                        if (tbTwo.SelectedIndex != 0)
                        {
                            btnSearch.IsEnabled = false;
                            cbTwo.IsEnabled = true;

                            if (cbTwo.SelectedIndex != 0)
                            {
                                btnSearch.IsEnabled = true;
                                tbThree.IsEnabled = true;
                                Active = 1;

                                if (tbThree.SelectedIndex != 0)
                                {
                                    btnSearch.IsEnabled = false;
                                    cbThree.IsEnabled = true;

                                    if (cbThree.SelectedIndex != 0)
                                    {
                                        btnSearch.IsEnabled = true;
                                        Active = 0;
                                    }
                                    else
                                    {
                                        btnSearch.IsEnabled = false;
                                    }
                                }
                                else
                                {
                                    btnSearch.IsEnabled = true;
                                    cbThree.IsEnabled = false;
                                }
                            }
                            else
                            {
                                btnSearch.IsEnabled = false;
                                tbThree.IsEnabled = false;
                                cbThree.IsEnabled = false;
                            }
                        }
                        else
                        {
                            btnSearch.IsEnabled = true;
                            cbTwo.IsEnabled = false;
                            tbThree.IsEnabled = false;
                            cbThree.IsEnabled = false;
                        }
                    }
                    else
                    {
                        btnSearch.IsEnabled = false;
                        tbTwo.IsEnabled = false;
                        cbTwo.IsEnabled = false;
                        tbThree.IsEnabled = false;
                        cbThree.IsEnabled = false;
                    }
                }
                else
                {
                    cbOne.IsEnabled = false;
                    tbTwo.IsEnabled = false;
                    cbTwo.IsEnabled = false;
                    tbThree.IsEnabled = false;
                    cbThree.IsEnabled = false;
                }
            }
        }

        private void lbResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbScale.SelectedValue = lbResults.SelectedValue;
            cbRoot.SelectedIndex = Array.IndexOf(MusicKeys, tbOne.SelectedValue);
            cbRoot_SelectionChanged(cbRoot, null);
            cbScale_SelectionChanged(cbScale, null);
        }
        #endregion
    }
}