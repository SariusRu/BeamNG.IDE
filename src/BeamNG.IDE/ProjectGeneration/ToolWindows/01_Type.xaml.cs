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
    /// Interaktionslogik für Type.xaml
    /// </summary>
    public partial class Type : Page
    {
        
        BeamNG.IDE.Core.currentProject.currentPrj cur;
        public Type()
        {
            DateTime currentTime = DateTime.Now;
            cur = new Core.currentProject.currentPrj(null, currentTime, null, null);
            InitializeComponent();
        }

        private void car_Click(object sender, RoutedEventArgs e)
        {
            cur.type = "car";
            showNameScreen();
        }

        private void plane_Click(object sender, RoutedEventArgs e)
        {
            cur.type = "plane";
        }

        private void ship_Click(object sender, RoutedEventArgs e)
        {
            cur.type = "ship";
        }

        private void Prop_Click(object sender, RoutedEventArgs e)
        {
            cur.type = "prop";
        }

        private void custom_Click(object sender, RoutedEventArgs e)
        {
            cur.type = "custom";
        }

        private void modification_Click(object sender, RoutedEventArgs e)
        {
            cur.type = "modification";
        }

        private void showNameScreen()
        {
            //BeamNG.IDE.ProjectGeneration.ToolWindows.ProjectInfo nm = new ProjectInfo();
            //NavigationService.Navigate(nm);
            BeamNG.IDE.ProjectGeneration.ToolWindows.ProjectBuilder nm = new ProjectBuilder();
            NavigationService.Navigate(nm);
        }
    }
}
