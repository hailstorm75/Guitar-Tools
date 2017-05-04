using ServicesLibrary;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RadialMenuLibrary
{
    /// <summary>
    /// Interaction logic for RadialMenu.xaml
    /// </summary>
    public partial class RadialMenu : UserControl
    {
        #region Properties
        private Canvas Container { get; set; }
        private string[] Items { get; set; }
        public int SelectedIndex { get; set; } 
        #endregion

        public event EventHandler SelectionChanged;

        private void RaiseSelectionChanged()
        {
            SelectionChanged?.Invoke(this, new EventArgs());
        }

        public RadialMenu(string[] items, int selectedIndex = 0)
        {
            InitializeComponent();

            Items = items;
            SelectedIndex = selectedIndex;
            SelectedItem.Content = Items[SelectedIndex];

            Container = new Canvas() { Width = 100, Height = 100 };
            Container.MouseWheel += new MouseWheelEventHandler(MenuMouseWheel);

            for (int i = 0; i < items.Length; i++)
            {
                double iter = (-90 + i * 360 / Items.Length) * Math.PI / 180;
                RadialButton btn = new RadialButton(
                    Items[i],
                    new Point(
                       39.5 * Math.Cos(iter) + 50,
                       39.5 * Math.Sin(iter) + 50),
                       i * 30, Container);
            }

            Container.RenderTransformOrigin = new Point(0.5, 0.5);
            Container.RenderTransform = new RotateTransform(-SelectedIndex * 360 / Items.Length);

            ControlRing.Children.Add(Container);
        }

        private void MenuMouseWheel(object sender, MouseWheelEventArgs e)
        {
            IntLimited i = new IntLimited(SelectedIndex + e.Delta / 120, 0, Items.Length);
            SelectedIndex = i.Value;
            RaiseSelectionChanged();

            SelectedItem.Content = Items[SelectedIndex];
            Container.RenderTransformOrigin = new Point(0.5, 0.5);

            Container.RenderTransform = new RotateTransform(-SelectedIndex * 360 / Items.Length);
        }
    }
}
