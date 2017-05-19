using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace GuitarScales
{
    /// <summary>
    /// 
    /// </summary>
    public static class PageAnimations
    {
        #region Load Animations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">Page to animate</param>
        /// <param name="seconds">Animation duration</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromRight(this Page page, float seconds)
        {
            var sb = new Storyboard();
            sb.AddSlideFromRight(seconds, page.WindowWidth);
            sb.AddFadeIn(seconds);

            sb.Begin(page);

            page.Visibility = System.Windows.Visibility.Visible;

            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">Page to animate</param>
        /// <param name="seconds">Animation duration</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromLeft(this Page page, float seconds)
        {
            var sb = new Storyboard();
            sb.AddSlideFromLeft(seconds, page.WindowWidth);
            sb.AddFadeIn(seconds);

            sb.Begin(page);

            page.Visibility = System.Windows.Visibility.Visible;

            await Task.Delay((int)(seconds * 1000));
        }

        #endregion

        #region Unload Animations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">Page to animate</param>
        /// <param name="seconds">Animation duration</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToRight(this Page page, float seconds)
        {
            var sb = new Storyboard();
            sb.AddSlideToRight(seconds, page.WindowWidth);
            sb.AddFadeOut(seconds);

            sb.Begin(page);

            page.Visibility = System.Windows.Visibility.Visible;

            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">Page to animate</param>
        /// <param name="seconds">Animation duration</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToLeft(this Page page, float seconds)
        {
            var sb = new Storyboard();
            sb.AddSlideToLeft(seconds, page.WindowWidth);
            sb.AddFadeOut(seconds);

            sb.Begin(page);

            page.Visibility = System.Windows.Visibility.Visible;

            await Task.Delay((int)(seconds * 1000));
        }

        #endregion
    }
}
