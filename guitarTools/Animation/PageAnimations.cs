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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="seconds"></param>
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
        /// <param name="page"></param>
        /// <param name="seconds"></param>
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
    }
}
