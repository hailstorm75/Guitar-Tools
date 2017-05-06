using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ServicesLibrary;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System;

namespace FretboardLibrary
{
    /// <summary>
    /// FretNote is a class which represents a note on the fretboard.
    /// It is instantiated and managed by the Fretboard object.
    /// </summary>
    public class FretNote
    {
        #region Properties
        /// <summary>
        /// ID of the FretNote
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Index of the Root
        /// </summary>
        private int Root { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private bool IsActive { get; set; }

        /// <summary>
        /// Set of music keys
        /// </summary>
        private static string[] MusicKeys = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        private Label noteText;
        private Border noteBody;
        private Viewbox box;
        #endregion

        /// <summary>
        /// Instantiates Controls which represent the FretNote and after grouping them all together appends them to the <paramref name="grid"/>.
        /// </summary>
        /// <param name="index">Passes its value to Index property</param>
        /// <param name="size">Defines the Width and Height of the FretNote</param>
        /// <param name="isActive">Passes its value to IsActive property</param>
        /// <param name="root">Passes its value to Root property</param>
        /// <param name="xy">Defines the position (row, column) on the <paramref name="grid"/></param>
        /// <param name="grid">The instantiated note is appended to the children of given Grid</param>
        public FretNote(int index, double size, bool isActive, int root, Point xy, Grid grid)
        {
            #region Defining variables
            Index = index;
            Root = root;
            IsActive = isActive;
            #endregion

            #region Creating objects visual representation

            //Creating a Border
            noteBody = new Border()
            {
                BorderBrush = Root == Index ? Brushes.Gold : Brushes.Black,
                BorderThickness = new Thickness(1),
                Background = Brushes.SlateBlue,
                Opacity = IsActive ? 1 : 0.3,                   //Inline IF ELSE operation
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = size,
                Height = size,
                CornerRadius = new CornerRadius((size * 2) / 4) //Calculating radius based on the Width and Height of the border
            };

            // Creating a Label
            noteText = new Label()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Brushes.White,
                Content = MusicKeys[Index]
            };

            // Creating a Viewbox
            box = new Viewbox()
            {
                //Parenting the elements accordingly: Label -> Viewbox -> Border
                Child = noteText
            };

            noteBody.Child = box;

            SetToolTip();

            Grid.SetColumn(noteBody, (int)xy.X);
            Grid.SetRow(noteBody, (int)xy.Y);

            // Adding the created object to the grid
            grid.Children.Add(noteBody);
            #endregion
        }

        /// <summary>
        /// Changes opacity based on the <paramref name="IsActive"/> value.
        /// </summary>
        /// <param name="IsActive">Defines if the FretNote is active or not</param>
        public void ChangeState(bool IsActive)
        {
            noteBody.Opacity = IsActive ? 1 : 0.3;
        }

        /// <summary>
        /// Changes border color based on the <paramref name="root"/> value.
        /// Resets the tooltip.
        /// </summary>
        /// <param name="root">Passes its value to the Root property</param>
        public void HighlightRoot(int root)
        {
            Root = root;
            SetToolTip();
            noteBody.BorderBrush = Root == Index ? Brushes.Gold : Brushes.Black;
        }

        /// <summary>
        /// Changes label content to new value based on the <paramref name="shiftTo"/> value.
        /// Resets the tooltip.
        /// </summary>
        /// <param name="shiftTo">Passes its value to the Index property</param>
        public void ShiftTuning(int shiftTo)
        {
            Index = shiftTo;
            SetToolTip();
            noteText.Content = MusicKeys[(new IntLimited(shiftTo, 0, 12)).Value];
        }

        /// <summary>
        /// Sets the tooltip based on the Index and Root properties values.
        /// </summary>
        private void SetToolTip()
        {
            IntLimited interval = new IntLimited(Index - Root, 0, 12);
            noteBody.ToolTip = (from node in XDocument.Load(System.IO.Directory.GetCurrentDirectory() + @"\Data\Data.xml")
                                                      .Descendants("Ratios").Elements("Ratio")
                                where node.Attribute("id").Value == interval.Value.ToString()
                                select new StringBuilder(node.Element("Value").Value)
                                                        .Append(" ")
                                                        .Append(node.Element("Name").Value)
                                                        .ToString()).Single();
        }
    }
}