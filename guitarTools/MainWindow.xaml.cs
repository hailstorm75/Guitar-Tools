using FretboardLibrary;
using Controls;
using ServicesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Xml.Linq;
using GuitarScales.Pages;

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
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Main.Content = new Guitar();
        }

        #endregion
    }
}