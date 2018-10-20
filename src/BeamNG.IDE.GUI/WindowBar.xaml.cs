﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeamNG.IDE.GUI
{
    /// <summary>
    /// Interaction logic for WindowBar.xaml
    /// </summary>
    public partial class WindowBar : UserControl
    {
        public WindowBar()
        {
            InitializeComponent();            
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement feSource = e.Source as FrameworkElement;
            Window parentWindow = Window.GetWindow(this);
            switch (feSource.Name)
            {
                case "minimize":
                    parentWindow.WindowState = WindowState.Minimized;
                    break;
                case "maximize":
                    parentWindow.WindowState = WindowState.Maximized;
                    break;
                case "close":                   
                    parentWindow.Close();
                    break;
            }
            e.Handled = true;

        }
    }
}