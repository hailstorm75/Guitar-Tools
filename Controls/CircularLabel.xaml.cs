using System.Windows.Controls;
using System.Windows.Media;

namespace Controls
{
    /// <summary>
    /// Interaction logic for CircularLabel.xaml
    /// </summary>
    public partial class CircularLabel : UserControl
    {
        #region Private Variables

        private string mText;
        private string mAlt;
        private double mRotationAngle;
        private double mBorderOpacity;
        private Brush mBorderBackground;
        private Brush mBorderOutline;
        private int mSize;

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get { return mText; }
            set
            {
                if (mText == value)
                    return;

                mText = value;

                textBlock.Text = mText;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Alt
        {
            get { return mAlt; }
            set
            {
                if (mAlt == value)
                    return;

                mAlt = value;

                border.ToolTip = mAlt;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double RotationAngle
        {
            get { return mRotationAngle; }
            set
            {
                if (mRotationAngle == value)
                    return;

                mRotationAngle = value;

                textBlock.RenderTransform = new RotateTransform(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double BorderOpacity
        {
            get { return mBorderOpacity; }
            set
            {
                if (mBorderOpacity == value)
                    return;

                mBorderOpacity = value;

                border.Opacity = mBorderOpacity;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Brush BorderBackground
        {
            get { return mBorderBackground; }
            set
            {
                if (mBorderBackground == value)
                    return;

                mBorderBackground = value;

                border.Background = mBorderBackground;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Brush BorderOutline
        {
            get { return mBorderOutline; }
            set
            {
                if (mBorderOutline == value)
                    return;

                mBorderOutline = value;

                border.BorderBrush = mBorderOutline;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Size
        {
            get { return mSize; }
            set
            {
                if (mSize == value)
                    return;

                mSize = value;

                viewBox.Width = mSize;
                viewBox.Height = mSize;
            }
        }

        #endregion

        #region Contructor

        /// <summary>
        /// 
        /// </summary>
        public CircularLabel()
        {
            InitializeComponent();
        }

        #endregion
    }
}
