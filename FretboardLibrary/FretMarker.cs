using System.Windows;
using System.Windows.Controls;

namespace FretboardLibrary
{
    /// <summary>
    /// Contains the definition of the Fret Marker class.
    /// Creates the UI elementes, binds them and places into grid cell.
    /// It is created and managed by the Fretboard class
    /// </summary>

    public class FretMarker
    {
        /// <summary>
        /// Instantiates controls which represent the FretMarker and after grouping them appends them to the <paramref name="grid"/>.
        /// </summary>
        /// <param name="grid">The instantiated fret marker is appended to the children of given Grid</param>
        /// <param name="xy">Defines the position (row, column) on the <paramref name="grid"/></param>
        /// <param name="content">Defines the content of the fret marker label</param>
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
