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
    /// Interaktionslogik für ProjectBuilder.xaml
    /// </summary>
    public partial class ProjectBuilder : Page
    {
        int active;
        public ProjectBuilder()
        {
            InitializeComponent();
            Core.ToolBox getTools = new Core.ToolBox();
            Core.ToolBox.ToolCategory[] tools = getTools.getToolBox();
            category.ItemsSource = tools;
        }

        private void category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Core.ToolBox.ToolCategory selected = (Core.ToolBox.ToolCategory)category.SelectedItem;
                tools.ItemsSource = selected.Tools;

            }
            catch (System.NullReferenceException) { }
        }
    }
}
