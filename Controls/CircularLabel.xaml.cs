using System.Windows.Controls;
using System.Windows.Media;

namespace Controls
{
  /// <summary>
  /// Interaction logic for CircularLabel.xaml
  /// </summary>
  public partial class CircularLabel : UserControl
  {
    #region Contructor

    /// <summary>
    /// Default constructor
    /// </summary>
    public CircularLabel()
    {
      InitializeComponent();
    }

    #endregion

    #region Fields

    private string mText;
    private string mAlt;
    private double mRotationAngle;
    private double mBorderOpacity;
    private Brush mBorderBackground;
    private Brush mBorderOutline;
    private int mSize;

    #endregion

    #region Properties

    /// <summary>
    /// TextBox content
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
    /// ToolTip content
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
    /// TextBox rotation angle
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
    /// Border opacity
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
    /// Border background
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
    /// Border brush
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
    /// ViewBox width and height
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
  }
}