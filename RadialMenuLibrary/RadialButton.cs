using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RadialMenuLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class RadialButton : Control
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        private Viewbox Box { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Point Position { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private int Size = 18; 
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="position"></param>
        /// <param name="angle"></param>
        /// <param name="grid"></param>
        public RadialButton(string content, Point position, double angle, Canvas grid)
        {
            Position = position;

            #region Creating objects visual representation
            Border Body = new Border()
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Background = Brushes.SlateBlue,
                Opacity = 1,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = Size,
                Height = Size,
                CornerRadius = new CornerRadius((Size * 2) / 4)
            };

            //Creating a Label
            Label Text = new Label()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Brushes.White,
                Content = content,
            };

            //Creating a Viewbox
            Box = new Viewbox()
            {
                //Parenting the elements accordingly: Label -> Viewbox -> Border
                Child = Text,
                RenderTransformOrigin = new Point(0.5, 0.5),
                RenderTransform = new RotateTransform(angle)
            };

            Body.Child = Box;

            Canvas.SetLeft(Body, Position.X - Size / 2);
            Canvas.SetTop(Body, Position.Y - Size / 2);

            //Adding the created object to the grid
            grid.Children.Add(Body);
            #endregion
        }
    }
}
