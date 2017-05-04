using FretboardLibrary;
using RadialMenuLibrary;
using ServicesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Xml.Linq;

namespace GuitarScales
{
    /// <summary>
    /// Contains the window logic.
    /// Creates UI elements with default settings upon initiation.
    /// </summary>

    public partial class MainWindow : Window
    {
        #region Properties
        Fretboard fretboard;
        RadialMenu cbRoot;
        List<List<FretNote>> NoteList;

        private XDocument Doc { get; set; }

        public bool HiddenMenu { get; set; }
        public StackPanel SettingsPanel { get; set; }

        public bool init = false;
        public string[] MusicKeys = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        public ComboBox[,] Menu;
        public int Active = 2;
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            #region Defining variables
            ushort frets = 12 * 1;
            ushort strings = 6;
            NoteList = new List<List<FretNote>>(); // Row is a string, column is a fret
            Doc = new XDocument(XDocument.Load(System.IO.Directory.GetCurrentDirectory() + @"\Data\Data.xml"));

            HiddenMenu = true;
            SettingsPanel = null;
            #endregion

            string[] MusicKeys = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

            cbRoot = new RadialMenu(MusicKeys, 4);
            cbRoot.SelectionChanged += new EventHandler(Root_SelectionChanged);

            ViewRoot.Child = cbRoot;

            // Creating default fretboard
            fretboard = new Fretboard(mainGrid, strings, frets, NoteList, 4, "Standard E", "Ionian");

            SetupControls(4, 0, strings);
            SetupSearchScale();
        }

        #region Setup Settings
        private void SetupControls(int root, int scale, int strings)
        {
            // The root notes are constant - no need to fetch from database
            cbRoot.SelectedIndex = root; // Setting default root note to "E"

            Dispatcher.Invoke(() => { FillTunings(strings); });
            Dispatcher.Invoke(() => { FillScales(scale); });
        }

        private void FillTunings(int strings)
        {
            if (cbTuning.HasItems)
            {
                cbTuning.Items.Clear();
            }

            // Adding tunings from database
            var tunings = from node in Doc.Descendants("Tunings").Elements("Tuning")
                          where node.Attribute("strings").Value == strings.ToString()
                          select node.Element("Name").Value;
            
            foreach (var item in tunings)
                cbTuning.Items.Add(item);
            cbTuning.SelectedIndex = 0;
        }

        private void FillScales(int scale)
        {
            // Adding scales from database
            var scales = from node in Doc.Descendants("Scales").Elements("Scale")
                         select node.Element("Name").Value;

            foreach (var item in scales)
                cbScale.Items.Add(item);
            cbScale.SelectedIndex = scale;
        }

        private void SetupSearchScale()
        {
            var chords = from node in Doc.Descendants("Chords").Elements("Chord")
                         select node.Element("Name").Value;

            Dispatcher.Invoke(() =>
            {
                foreach (var item in MusicKeys)
                {
                    tbOne.Items.Add(item);
                    tbTwo.Items.Add(item);
                    tbThree.Items.Add(item);
                }
            }, DispatcherPriority.ContextIdle);

            Dispatcher.Invoke(() =>
            {
                foreach (var item in chords)
                {
                    cbOne.Items.Add(item);
                    cbTwo.Items.Add(item);
                    cbThree.Items.Add(item);
                }
            }, DispatcherPriority.ContextIdle);

            Menu = new ComboBox[,] { { tbOne, tbTwo, tbThree }, { cbOne, cbTwo, cbThree } };
            init = true;
        }
        #endregion

        #region ComboBoxes
        public void Root_SelectionChanged(object sender, EventArgs e)
        {
            if (fretboard.Root != cbRoot.SelectedIndex)
                fretboard.UpdateRoot(cbRoot.SelectedIndex);
        }

        private void cbTuning_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (fretboard.Tuning != cbTuning.SelectedValue.ToString())
                    fretboard.UpdateTuning(cbTuning.SelectedValue.ToString());
            }
            catch { }        
        }

        public void cbScale_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if (fretboard.Scale != cbScale.SelectedValue.ToString())
                    fretboard.UpdateScale(cbScale.SelectedValue.ToString());
            }
            catch { }
            
        }
        #endregion

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = HiddenMenu ? Resources["sbShowLeftMenu"] as Storyboard :
                                         Resources["sbHideLeftMenu"] as Storyboard;

            sb.Begin(pnlLeftMenu);
            HiddenMenu = !HiddenMenu;
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
            Semaphore sem = new Semaphore(1, Environment.ProcessorCount);
            
            for (int row = 0; row < Menu.GetLength(1) - Active; row++)
            {
                Dispatcher.Invoke(() => {
                    sem.WaitOne();
                    string[] chord = (from node in Doc.Descendants("Chords").Elements("Chord")
                                      where node.Element("Name").Value == (string)Menu[1, row].SelectedValue
                                      select node.Element("Interval").Value).Single().Split(' ');

                    int pointB = Array.IndexOf(MusicKeys, Menu[1, row]);
                    if (pointA - pointB != 0)
                    {
                        IntLimited shiftBy = new IntLimited(pointA + pointB, 0, 12);
                    }
                    foreach (var item in chord)
                        if (!chordNotes.Contains(int.Parse(item)))
                            chordNotes.Add(int.Parse(item));
                    sem.Release();
                }, DispatcherPriority.ContextIdle);
            }
            
            chordNotes.Sort();

            var scales = from node in Doc.Descendants("Scales").Elements("Scale")
                            select node.Element("Interval").Value;

            List<string> found = new List<string>();
            foreach (var item in scales)
            {
                (new Thread(() => {
                    sem.WaitOne();
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
                    sem.Release();
                })).Start();    
            }

            if (found.Count > 0)
                foreach (var item in found)
                    lbResults.Items.Add(
                        (from node in Doc.Descendants("Scales").Elements("Scale")
                        where node.Element("Interval").Value == item
                        select node.Element("Name").Value).Single()
                        );
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
            Root_SelectionChanged(cbRoot, null);
            cbScale_SelectionChanged(cbScale, null);
        }
        #endregion

        private void FretboardApply_Click(object sender, RoutedEventArgs e)
        {
            if (SliderFrets.Value != fretboard.Frets || SliderStrings.Value != fretboard.Strings)
            {
                int oldStrings = fretboard.Strings;
                fretboard.ClearNotes();
                fretboard = new Fretboard(mainGrid, 
                                          (ushort)SliderStrings.Value, 
                                          (ushort)SliderFrets.Value, 
                                          NoteList, 
                                          4,
                                          (from node in Doc.Descendants("Tunings").Elements("Tuning")
                                           where node.Attribute("strings").Value == SliderStrings.Value.ToString()
                                           select node.Element("Name").Value).First(), 
                                          "Ionian");

                if (SliderStrings.Value != oldStrings)
                {
                    FillTunings((int)SliderStrings.Value);
                }
            }           
        }
    }
}