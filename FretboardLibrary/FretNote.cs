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
    /// FretNote is an object which represents a note on the fretboard
    /// It is created and managed by the Fretboard object
    /// </summary>

    public class FretNote
    {
        #region Properties
        public int Index { get; set; }
        private int Root { get; set; }
        private bool IsActive { get; set; }
        private string[] MusicKeys = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        private Label noteText;
        private Border noteBody;
        private Viewbox box;
        #endregion

        private static readonly Object key = new Object();

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
                //BorderBrush = Brushes.Black,
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

            //Creating a Label
            noteText = new Label()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Brushes.White,
                Content = MusicKeys[Index]
            };

            //Creating a Viewbox
            box = new Viewbox()
            {
                //Parenting the elements accordingly: Label -> Viewbox -> Border
                Child = noteText
            };

            noteBody.Child = box;

            SetToolTip();

            Grid.SetColumn(noteBody, (int)xy.X);
            Grid.SetRow(noteBody, (int)xy.Y);

            //Adding the created object to the grid
            grid.Children.Add(noteBody);
            #endregion
        }

        public void ChangeState(bool IsActive)
        {
            noteBody.Opacity = IsActive ? 1 : 0.3;
        }

        public void HighlightRoot(int root)
        {
            Root = root;
            SetToolTip();
            noteBody.BorderBrush = Root == Index ? Brushes.Gold : Brushes.Black;
        }

        public void ShiftTuning(int ShiftBy)
        {
            Index = ShiftBy;
            SetToolTip();
            noteText.Content = MusicKeys[(new IntLimited(ShiftBy, 0, 12)).Value];
        }

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