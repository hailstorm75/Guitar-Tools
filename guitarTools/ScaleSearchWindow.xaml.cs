using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using guitarTools.Classes;
using System.Windows.Controls;

namespace guitarTools
{
    /// <summary>
    /// Interaction logic for ScaleSearchWindow.xaml
    /// </summary>
    public partial class ScaleSearchWindow : Window
    {
        public bool init = false;
        public string[] MusicKeys = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        public ComboBox[,] Menu;
        public int Active = 2;

        public ScaleSearchWindow()
        {
            InitializeComponent();

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
        }

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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
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

            string a = "";

            foreach (var item in chordNotes)
            {
                a += item;
                a += " ";
            }

            a.TrimEnd();

            string[] scales = SQLCommands.FetchList<string>("SELECT Name FROM tableScales WHERE Interval LIKE '0 2'").ToArray();

            foreach (var item in scales)
            {
                MessageBox.Show(item);
            }
        }
    }
}
