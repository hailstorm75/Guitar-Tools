using ServicesLibrary;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Controls
{
    /// <summary>
    /// RadialMenu contains logic for the custom UserControl.
    /// </summary> 
    public partial class RadialMenu : UserControl
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private Canvas Container { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private string[] Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SelectedIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler SelectionChanged;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="selectedIndex"></param>
        public RadialMenu(string[] items, int selectedIndex = 0)
        {
            InitializeComponent();

            Items = items;
            SelectedIndex = selectedIndex;
            SelectedItem.Content = Items[SelectedIndex];

            Container = new Canvas() { Width = 100, Height = 100 };
            MouseOver.MouseWheel += new MouseWheelEventHandler(MenuMouseWheel);

            for (int i = 0; i < items.Length; i++)
            {
                double iter = (-90 + i * 360 / Items.Length) * Math.PI / 180;
                CircularLabel item = new CircularLabel()
                {
                    RotationAngle = i * 30,
                    Text = Items[i],
                    Size = 18
                };

                Container.Children.Add(item);
                Canvas.SetLeft(item, (39.5 * Math.Cos(iter) + 50) - 18 / 2);
                Canvas.SetTop(item, (39.5 * Math.Sin(iter) + 50) - 18 / 2);
            }

            Container.RenderTransformOrigin = new Point(0.5, 0.5);
            Container.RenderTransform = new RotateTransform(-SelectedIndex * 360 / Items.Length);

            ControlRing.Children.Add(Container);
        }

        /// <summary>
        /// 
        /// </summary>
        private void RaiseSelectionChanged()
        {
            SelectionChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
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
