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

namespace BeamNG.IDE.StartPage
{
    /// <summary>
    /// Interaktionslogik für StartPage2.xaml
    /// </summary>
    public partial class StartPage : Window
    {
        Core.recentProjects rct = new Core.recentProjects();
        StartUp.SplashScreenForm splash = new StartUp.SplashScreenForm();
        public StartPage(StartUp.SplashScreenForm splashScreen)
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
    }
}


