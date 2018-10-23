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
    /// Interaction logic for Name.xaml
    /// </summary>
    public partial class Name : Page
    {
        BeamNG.IDE.Core.currentProject.currentPrj cur;

        public Name(BeamNG.IDE.Core.currentProject.currentPrj current)
        {
            cur = current;
            InitializeComponent();
        }

        private void browsePath_Click(object sender, RoutedEventArgs e)
        {

        }

        private void setPath_Click(object sender, RoutedEventArgs e)
        {
            cur.filePath = path.Text;
        }

        private void setName_Click(object sender, RoutedEventArgs e)
        {
            cur.projectName = name.Text;
        }

        private void TextBlock_Click(object sender, RoutedEventArgs e)
        {

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            BeamNG.IDE.ProjectGeneration.ToolWindows._03_ProjectBuilder nv = new _03_ProjectBuilder();
            NavigationService.Navigate(nv);
        }
    }
}
