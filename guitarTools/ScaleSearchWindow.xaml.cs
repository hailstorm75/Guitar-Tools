using System.Windows;
using System.Windows.Input;

namespace guitarTools
{
    /// <summary>
    /// Interaction logic for ScaleSearchWindow.xaml
    /// </summary>
    public partial class ScaleSearchWindow : Window
    {
        public ScaleSearchWindow()
        {
            InitializeComponent();
        }

        private void WindowTop_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (WindowState == WindowState.Maximized)
                {
                    WindowState = WindowState.Normal;
                    Top = 0;
                    Left = Mouse.GetPosition(this).X - ActualWidth / 2;
                }

                DragMove();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
