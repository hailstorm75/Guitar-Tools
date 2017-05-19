using System.Windows.Controls;
using System.Windows.Media;
using ServicesLibrary;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using Controls;

namespace FretboardLibrary
{
    /// <summary>
    /// Represents a note on the fretboard.
    /// It is instantiated and managed by the Fretboard object.
    /// </summary>
    public class FretNote : UserControl
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
        /// Defines FretNote visual state
        /// </summary>
        private bool IsActive { get; set; }

        #endregion

        #region Variables

        CircularLabel note;

        /// <summary>
        /// Set of music keys
        /// </summary>
        private static string[] MusicKeys = { "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B" };

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="index">Passes its value to Index property</param>
        /// <param name="size">Defines the Width and Height of the FretNote</param>
        /// <param name="isActive">Passes its value to IsActive property</param>
        /// <param name="root">Passes its value to Root property</param>
        public FretNote(int index, double size, bool isActive, int root)
        {
            #region Defining variables
            Index = index;
            Root = root;
            IsActive = isActive;
            #endregion

            note = new CircularLabel()
            {
                Text = MusicKeys[Index],
                BorderOpacity = IsActive ? 1 : 0.3,
                BorderOutline = Root == Index ? Brushes.Gold : Brushes.Black
            };

            SetToolTip();

            AddChild(note);
        }

        #endregion

        #region Property Updators

        /// <summary>
        /// Changes opacity based on the <paramref name="IsActive"/> value.
        /// </summary>
        /// <param name="IsActive">Defines if the FretNote is active or not</param>
        public void ChangeState(bool IsActive)
        {
            note.BorderOpacity = IsActive ? 1 : 0.3;
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

            note.BorderOutline = Root == Index ? Brushes.Gold : Brushes.Black;
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

            note.Text = MusicKeys[(new IntLimited(shiftTo, 0, 12)).Value];
        }

        /// <summary>
        /// Sets the tooltip based on the Index and Root properties values.
        /// </summary>
        private void SetToolTip()
        {
            IntLimited interval = new IntLimited(Index - Root, 0, 12);

            note.Alt = (from node in XDocument.Load(System.IO.Directory.GetCurrentDirectory() + @"\Data\Data.xml")
                                                      .Descendants("Ratios").Elements("Ratio")
                        where node.Attribute("id").Value == interval.Value.ToString()
                        select new StringBuilder(node.Element("Value").Value)
                                                .Append(" ")
                                                .Append(node.Element("Name").Value)
                                                .ToString()).Single();
        }

        #endregion
    }
}