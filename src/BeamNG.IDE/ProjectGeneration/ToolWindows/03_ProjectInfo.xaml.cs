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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeamNG.IDE.ProjectGeneration.ToolWindows
{
    /// <summary>
    /// Interaktionslogik für _03_ProjectInfo.xaml
    /// </summary>
    public partial class ProjectInfo : Page
    {
        public ProjectInfo()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            BeamNG.IDE.ProjectGeneration.ToolWindows.ProjectBuilder nv = new ProjectBuilder();
            NavigationService.Navigate(nv);
        }
    }
}
