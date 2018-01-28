using System;
using System.Windows;
using System.Windows.Navigation;

namespace GuitarScales.Pages
{
  /// <summary>
  /// Interaction logic for Welcome.xaml
  /// </summary>
  public partial class Welcome : BasePage
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    public Welcome()
    {
      PageLoadAnimation = PageAnimation.SlideAndFadeInFromLeft;
      PageUnloadAnimation = PageAnimation.None;
      InitializeComponent();
    }

    private void ButtonOptionA_Click(object sender, RoutedEventArgs e)
    {
      var ns = NavigationService.GetNavigationService(this);
      ns.Navigate(new Uri("Pages/Guitar.xaml", UriKind.Relative));
    }

    private void ButtonOptionB_Click(object sender, RoutedEventArgs e)
    {
      var ns = NavigationService.GetNavigationService(this);
      ns.Navigate(new Uri("Piano.xaml", UriKind.Relative));
    }
  }
}