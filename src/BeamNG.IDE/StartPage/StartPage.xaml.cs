using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace BeamNG.IDE.StartPage
{
    /// <summary>
    /// Interaktionslogik für StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Start();
            timer.Tick += (object h, EventArgs f) =>
            {
                timer.Stop();
                splash.Close();
            };
        }

        private void recentList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Core.recentPrj selected = (Core.recentPrj)recentList.SelectedItem;
            MessageBox.Show("CLICK!");
        }
    }
}
