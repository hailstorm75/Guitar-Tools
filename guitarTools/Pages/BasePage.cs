using System.Threading.Tasks;
using System.Windows.Controls;

namespace GuitarScales.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public class BasePage : Page
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeInFromRight;

        /// <summary>
        /// 
        /// </summary>
        public PageAnimation PageUnloadAnimation { get; set; } = PageAnimation.SlideAndFadeOutFromLeft;

        /// <summary>
        /// 
        /// </summary>
        protected float SlideSeconds { get; set; } = 1;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public BasePage()
        {
            if (PageLoadAnimation != PageAnimation.None)
            {
                Visibility = System.Windows.Visibility.Collapsed;
            }

            Loaded += BasePage_Loaded;
        }

        #endregion

        #region Animation Load / Unload

        /// <summary>
        /// 
        /// </summary>
        private async void BasePage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await AnimateIn();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task AnimateIn()
        {
            if (PageLoadAnimation == PageAnimation.None) return;

            switch (PageLoadAnimation)
            {
                case PageAnimation.SlideAndFadeInFromRight:
                    await PageAnimations.SlideAndFadeInFromRight(this, SlideSeconds);
                    break;
                case PageAnimation.SlideAndFadeOutFromLeft:
                    break;
                case PageAnimation.SlideAndFadeInFromLeft:
                    await PageAnimations.SlideAndFadeInFromLeft(this, SlideSeconds);
                    break;
            }
        }

        #endregion
    }
}
