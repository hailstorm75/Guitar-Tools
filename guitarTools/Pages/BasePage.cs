using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GuitarScales.Pages
{
    /// <summary>
    /// Base class for pages
    /// </summary>
    public class BasePage : Page
    {
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

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public BasePage()
        {
            if (PageLoadAnimation != PageAnimation.None)
                Visibility = System.Windows.Visibility.Collapsed;

            Loaded += BasePage_Loaded;
            //Unloaded += BasePage_Unloaded;
        }

        #endregion

        #region Animation Load / Unload

        /// <summary>
        /// 
        /// </summary>
        protected async void BasePage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await AnimateIn();
        }

        /// <summary>
        /// 
        /// </summary>
        protected async void BasePage_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await AnimateOut();
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
                    await PageAnimations.SlideAndFadeInFromRight(this, SlideSeconds);
                    break;
                case PageAnimation.SlideAndFadeInFromLeft:
                    await PageAnimations.SlideAndFadeInFromLeft(this, SlideSeconds);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task AnimateOut()
        {
            if (PageUnloadAnimation == PageAnimation.None) return;

            switch (PageUnloadAnimation)
            {
                case PageAnimation.SlideAndFadeOutToLeft:
                    await PageAnimations.SlideAndFadeOutToLeft(this, SlideSeconds);
                    break;
                case PageAnimation.SlideAndFadeOutToRight:
                    await PageAnimations.SlideAndFadeOutToRight(this, SlideSeconds);
                    break;
            }
        }
    }
}
