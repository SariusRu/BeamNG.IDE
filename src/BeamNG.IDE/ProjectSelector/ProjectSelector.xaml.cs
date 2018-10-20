using System;
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
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Runtime.InteropServices;

namespace BeamNG.IDE.ProjectSelector
{
    /// <summary>
    /// Interaktionslogik für ProjectSelector2.xaml
    /// </summary>
    public partial class ProjectSelector : Window
    {
        private bool mouseClicked;
        private Point lastLocation;

        Core.recentProjects rct = new Core.recentProjects();
        StartUp.SplashScreenForm splash = new StartUp.SplashScreenForm();
        public ProjectSelector(StartUp.SplashScreenForm splashScreen)
        {
            BeamNG.IDE.Core.recentProjects recent = new Core.recentProjects();
            InitializeComponent();
            initializeRecentProjects();
            splash = splashScreen;
        }

        private void initializeRecentProjects()
        {
            Core.recentPrj[] rctArray = rct.getProjects();
            this.recentList.ItemsSource = rctArray;
        }

        private void recentList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Core.recentPrj selected = (Core.recentPrj)recentList.SelectedItem;
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Start();
            timer.Tick += (object h, EventArgs f) =>
            {
                timer.Stop();
                splash.Close();
            };
        }
       
        private void Open_Click(object sender, RoutedEventArgs e)
        {
               ProjectGeneration.newProject newPrj = new ProjectGeneration.newProject();
               newPrj.Show();
               this.Close();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            ProjectGeneration.newProject newPrj = new ProjectGeneration.newProject();
            newPrj.Show();
            this.Close();
        }

        private void bar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                lastLocation = Mouse.GetPosition(bar);
                mouseClicked = true;
            }
        }

        private void bar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseClicked = false;
        }

        private void bar_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseClicked)
            {
                this.Left = PointToScreen(Mouse.GetPosition(this)).X - lastLocation.X;
                this.Top = PointToScreen(Mouse.GetPosition(this)).Y - lastLocation.Y;
            }
        }
    }
}


