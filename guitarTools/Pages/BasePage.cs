using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GuitarScales.Pages
{
  /// <summary>
  /// Base class for pages
  /// </summary>
  public class BasePage : Page
  {
    #region Constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    public BasePage()
    {
      if (PageLoadAnimation != PageAnimation.None)
        Visibility = Visibility.Collapsed;

      Loaded += BasePage_Loaded;
      Unloaded += BasePage_Unloaded;
    }

    #endregion

    /// <summary>
    /// Executes selected animation
    /// </summary>
    public async Task AnimateIn()
    {
      if (PageLoadAnimation == PageAnimation.None) return;

      switch (PageLoadAnimation)
      {
        case PageAnimation.SlideAndFadeInFromRight:
          await this.SlideAndFadeInFromRight(SlideSeconds);
          break;
        case PageAnimation.SlideAndFadeInFromLeft:
          await this.SlideAndFadeInFromLeft(SlideSeconds);
          break;
      }
    }

    /// <summary>
    /// </summary>
    public async Task AnimateOut()
    {
      if (PageUnloadAnimation == PageAnimation.None) return;

      switch (PageUnloadAnimation)
      {
        case PageAnimation.SlideAndFadeOutToLeft:
          await this.SlideAndFadeOutToLeft(SlideSeconds);
          break;
        case PageAnimation.SlideAndFadeOutToRight:
          await this.SlideAndFadeOutToRight(SlideSeconds);
          break;
      }
    }

    #region Properties

    /// <summary>
    /// Defines animation type which is played when the page is loaded
    /// </summary>
    public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeInFromRight;

    /// <summary>
    /// Defines animation type which is played when the page is unloaded
    /// </summary>
    public PageAnimation PageUnloadAnimation { get; set; } = PageAnimation.SlideAndFadeOutToLeft;

    /// <summary>
    /// Defines the animation duration
    /// </summary>
    protected float SlideSeconds { get; set; } = 1;

    #endregion

    #region Animation Load / Unload

    /// <summary>
    /// </summary>
    protected async void BasePage_Loaded(object sender, RoutedEventArgs e)
    {
      await AnimateIn();
    }

    /// <summary>
    /// </summary>
    protected async void BasePage_Unloaded(object sender, RoutedEventArgs e)
    {
      await AnimateOut();
    }

    #endregion
  }
}