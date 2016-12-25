using System.Windows;
using System.Windows.Controls;

namespace guitarTools
{
    /// <summary>
    /// Contains the definition of the Fret Marker object.
    /// Creates the UI elementes, binds them and places into grid cell.
    /// It is created and managed by the Fretboard object
    /// </summary>

    class FretMarker
    {
        public FretMarker(Grid grid, Point xy, string content)
        {
            // Creates a label which displays the fret number
            Label marker = new Label()
            {
                Content = content,
                HorizontalContentAlignment = HorizontalAlignment.Center
            };

            // Creating Viewbox to scale label relatively to the window size
            Viewbox box = new Viewbox()
            {
                Child = marker // Placing Label inside Viewbox
            };

            // Setting position in grid to place created object
            Grid.SetColumn(box, (int)xy.X);
            Grid.SetRow(box, (int)xy.Y);

            // Placing created object inside grid
            grid.Children.Add(box);
        }
    }
}
