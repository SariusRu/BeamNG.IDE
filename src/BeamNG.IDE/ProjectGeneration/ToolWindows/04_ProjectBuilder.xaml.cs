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
        Point startPoint;

        public ProjectBuilder()
        {
            InitializeComponent();
            Core.ToolBox getTools = new Core.ToolBox();
            Core.ToolBox.ToolCategory[] tools = getTools.getToolBox();
            category.ItemsSource = tools;
            toolsBox.AllowDrop = true;
            mainBuilder.AllowDrop = true;
            //toolsBox.AddHandler(UIElement.MouseMoveEvent, new MouseButtonEventHandler(toolsBox_Move), true);
        }

        private void toolsBox_Move(object sender, MouseButtonEventArgs e)
        {

        }

        private void category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Core.ToolBox.ToolCategory selected = (Core.ToolBox.ToolCategory)category.SelectedItem;
                toolsBox.ItemsSource = selected.Tools;
            }
            catch (System.NullReferenceException) { }
        }

        private void toolsBox_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void toolsBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void toolsBox_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if(e.LeftButton == MouseButtonState.Pressed)
            {
                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    ListView listView = sender as ListView;
                    ListViewItem Tool = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);

                    Core.ToolBox.Tool dragTool = (Core.ToolBox.Tool)listView.ItemContainerGenerator.ItemFromContainer(Tool);

                    DataObject dragData = new DataObject("myFormat", dragTool);
                    DragDrop.DoDragDrop(Tool, dragData, DragDropEffects.Copy | DragDropEffects.Move);
                }
            }
        }

        private static T FindAnchestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void mainBuilder_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Core.ToolBox.Tool tool = e.Data.GetData("myFormat") as Core.ToolBox.Tool;
                TreeView treeView = sender as TreeView;
                treeView.Items.Add(tool);
            }


        }

        private void mainBuilder_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }
    }
}
