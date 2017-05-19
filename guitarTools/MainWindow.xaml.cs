using System.Windows;
using GuitarScales.Pages;
using System;

namespace GuitarScales
{
    /// <summary>
    /// Contains the window logic.
    /// Creates UI elements with default settings upon initiation.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Main.Source = new Uri("Pages/Welcome.xaml", UriKind.Relative);
        }

        #endregion
    }
}