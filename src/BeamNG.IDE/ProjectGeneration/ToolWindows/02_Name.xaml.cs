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

namespace BeamNG.IDE.ProjectGeneration.ToolWindows
{
    /// <summary>
    /// Interaction logic for _02_Name.xaml
    /// </summary>
    public partial class _02_Name : Page
    {
        public _02_Name()
        {
            InitializeComponent();
        }

        private void IDEButton_Click(object sender, RoutedEventArgs e)
        {
            name.Text = "Hallo";
        }
    }
}
