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


        Core.recentProjects rct = new Core.recentProjects();
        StartUp.SplashScreenForm splash = new StartUp.SplashScreenForm();
        public ProjectSelector(StartUp.SplashScreenForm splashScreen)
        {
            BeamNG.IDE.Core.recentProjects recent = new Core.recentProjects();
            InitializeComponent();
            initializeRecentProjects();
            splash = splashScreen;
            //Check for file errors - read from toolbox-File and Check if for every file a dummy file exists
            checkForErrors();

        }

        private Boolean checkForErrors()
        {
            Core.ToolBox getTools = new Core.ToolBox();
            Core.ToolBox.ToolCategory[] tools = getTools.getToolBox();
            for(int i = 0; i < tools.Length; i++)
            {
                for(int j = 0; j < tools[i].Tools.Length; j++)
                {
                    string name = tools[i].Tools[j].toolName;
                    MessageBox.Show(name);
                }
                
            }
            return false;
        }

        private void initializeRecentProjects()
        {
            Core.recentPrj[] rctArray = rct.getProjects();
            this.recentList.ItemsSource = rctArray;
        }

        private void recentList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {   
            try
            {
                Core.recentPrj selected = (Core.recentPrj)recentList.SelectedItem;
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            catch (System.NullReferenceException){ }
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
    }
}


