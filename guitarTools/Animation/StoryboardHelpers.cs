using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace GuitarScales
{
  /// <summary>
  /// </summary>
  public static class StoryboardHelpers
  {
    #region Load Animations

    /// <summary>
    /// Slide animation
    /// </summary>
    /// <param name="storyboard">Animation storyboard</param>
    /// <param name="seconds">Animation duration</param>
    /// <param name="offset">Slide offset</param>
    /// <param name="decelerationRatio">Animation smooth finish</param>
    public static void AddSlideFromRight(this Storyboard storyboard, float seconds, double offset,
        double decelerationRatio = 0.9)
    {
      var animation = new ThicknessAnimation
      {
        Duration = new Duration(TimeSpan.FromSeconds(seconds)),
        From = new Thickness(offset, 0, -offset, 0),
        To = new Thickness(0),
        DecelerationRatio = decelerationRatio
      };

      Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
      storyboard.Children.Add(animation);
    }

    /// <summary>
    /// Slide animation
    /// </summary>
    /// <param name="storyboard">Animation storyboard</param>
    /// <param name="seconds">Animation duration</param>
    /// <param name="offset">Slide offset</param>
    /// <param name="decelerationRatio">Animation smooth finish</param>
    public static void AddSlideFromLeft(this Storyboard storyboard, float seconds, double offset,
        double decelerationRatio = 0.9)
    {
      var animation = new ThicknessAnimation
      {
        Duration = new Duration(TimeSpan.FromSeconds(seconds)),
        From = new Thickness(-offset, 0, offset, 0),
        To = new Thickness(0),
        DecelerationRatio = decelerationRatio
      };

      Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
      storyboard.Children.Add(animation);
    }

    /// <summary>
    /// Fade animation
    /// </summary>
    /// <param name="storyboard">Animation storyboard</param>
    /// <param name="seconds">Animation duration</param>
    public static void AddFadeIn(this Storyboard storyboard, float seconds)
    {
      var animation = new DoubleAnimation
      {
        Duration = new Duration(TimeSpan.FromSeconds(seconds)),
        From = 0,
        To = 1
      };

      Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
      storyboard.Children.Add(animation);
    }

    #endregion

    #region Unload Animations

    /// <summary>
    /// Slide animation
    /// </summary>
    /// <param name="storyboard">Animation storyboard</param>
    /// <param name="seconds">Animation duration</param>
    /// <param name="offset">Slide offset</param>
    /// <param name="decelerationRatio">Animation smooth finish</param>
    public static void AddSlideToRight(this Storyboard storyboard, float seconds, double offset,
        double decelerationRatio = 0.9)
    {
      var animation = new ThicknessAnimation
      {
        Duration = new Duration(TimeSpan.FromSeconds(seconds)),
        From = new Thickness(0),
        To = new Thickness(offset, 0, -offset, 0),
        DecelerationRatio = decelerationRatio
      };

      Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
      storyboard.Children.Add(animation);
    }

    /// <summary>
    /// Slide animation
    /// </summary>
    /// <param name="storyboard">Animation storyboard</param>
    /// <param name="seconds">Animation duration</param>
    /// <param name="offset">Slide offset</param>
    /// <param name="decelerationRatio">Animation smooth finish</param>
    public static void AddSlideToLeft(this Storyboard storyboard, float seconds, double offset,
        double decelerationRatio = 0.9)
    {
      var animation = new ThicknessAnimation
      {
        Duration = new Duration(TimeSpan.FromSeconds(seconds)),
        From = new Thickness(0),
        To = new Thickness(-offset, 0, offset, 0),
        DecelerationRatio = decelerationRatio
      };

      Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
      storyboard.Children.Add(animation);
    }

    /// <summary>
    /// Fade animation
    /// </summary>
    /// <param name="storyboard">Animation storyboard</param>
    /// <param name="seconds">Animation duration</param>
    public static void AddFadeOut(this Storyboard storyboard, float seconds)
    {
      var animation = new DoubleAnimation
      {
        Duration = new Duration(TimeSpan.FromSeconds(seconds)),
        From = 1,
        To = 0
      };

      Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
      storyboard.Children.Add(animation);
    }

    #endregion
  }
}