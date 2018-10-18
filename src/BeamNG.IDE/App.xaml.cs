using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BeamNG.IDE
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        StartUp.SplashScreenForm splashScreen = new StartUp.SplashScreenForm();
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            splashScreen.Show();
            BeamNG.IDE.Core.mainDirectory genMain = new Core.mainDirectory();
            genMain.initialize();
            StartPage.StartPage startPage = new StartPage.StartPage(splashScreen);
            startPage.Show();
        }
    }
}
