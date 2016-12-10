using System.Windows.Controls;
using System.Windows.Media;

namespace guitarTools.Classes.Wheel_Select
{
    // TODO Write code for WheelSelect class

    class WheelSelect
    {
        public WheelSelect()
        {
            Grid a = new Grid();
            Viewbox b = new Viewbox();
            WheelButton button = new WheelButton();
            button.RenderTransform = new RotateTransform(45);
            a.Children.Add(button);
        }
    }
}
