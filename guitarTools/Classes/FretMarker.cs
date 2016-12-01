using System.Windows;
using System.Windows.Controls;

namespace guitarTools
{
    class FretMarker
    {
        public FretMarker(Grid grid, Point xy, string content)
        {
            Label marker = new Label()
            {
                Content = content,
                HorizontalContentAlignment = HorizontalAlignment.Center
            };

            Viewbox box = new Viewbox()
            {
                Child = marker
            };

            Grid.SetColumn(box, (int)xy.X);
            Grid.SetRow(box, (int)xy.Y);

            grid.Children.Add(box);
        }
    }
}
