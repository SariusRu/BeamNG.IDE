using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        Point _lastMouseDown;
        builderItem draggedItem, _target;

        public ProjectBuilder()
        {
            InitializeComponent();
            //Generate Content of toolbox
            Core.ToolBox getTools = new Core.ToolBox();
            Core.ToolBox.ToolCategory[] tools = getTools.getToolBox();
            //Adding Items to Toolbox
            category.ItemsSource = tools;
            //setting Properties
            toolsBox.AllowDrop = true;
            mainBuilder.AllowDrop = true;
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
                Point p = e.GetPosition(mainBuilder);
                Core.ToolBox.Tool tool = e.Data.GetData("myFormat") as Core.ToolBox.Tool;
                TreeView treeView = sender as TreeView;
                builderItem itemToAdd = new builderItem();
                itemToAdd.builderTool = tool;
                itemToAdd.Header = tool.toolHeader;
                treeView.Items.Add(itemToAdd);
            }
        }
        private void mainBuilder_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }
        private bool getItem(Point p)
        {
            Core.ToolBox.Tool hovering = mainBuilder.InputHitTest(p) as Core.ToolBox.Tool;
            if(hovering != null)
            {
                MessageBox.Show(hovering.GetType().ToString());
            }
            return true;
        }
        private void TreeView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                _lastMouseDown = e.GetPosition(mainBuilder);
            }

        }
        private void treeView_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point currentPosition = e.GetPosition(mainBuilder);


                    if ((Math.Abs(currentPosition.X - _lastMouseDown.X) > 10.0) ||
                        (Math.Abs(currentPosition.Y - _lastMouseDown.Y) > 10.0))
                    {
                        draggedItem = (builderItem)mainBuilder.SelectedItem;
                        if (draggedItem != null)
                        {
                            DragDropEffects finalDropEffect = DragDrop.DoDragDrop(mainBuilder, mainBuilder.SelectedValue,
                                DragDropEffects.Move);
                            //Checking target is not null and item is dragging(moving)
                            if ((finalDropEffect == DragDropEffects.Move) && (_target != null))
                            {
                                // A Move drop was accepted
                                if (!draggedItem.Header.ToString().Equals(_target.Header.ToString()))
                                {
                                    CopyItem(draggedItem, _target);
                                    _target = null;
                                    draggedItem = null;
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            try
            {

                Point currentPosition = e.GetPosition(mainBuilder);


                if ((Math.Abs(currentPosition.X - _lastMouseDown.X) > 10.0) ||
                    (Math.Abs(currentPosition.Y - _lastMouseDown.Y) > 10.0))
                {
                    // Verify that this is a valid drop and then store the drop target
                    builderItem item = GetNearestContainer(e.OriginalSource as UIElement);
                    if (CheckDropTarget(draggedItem, item))
                    {
                        e.Effects = DragDropEffects.Move;
                    }
                    else
                    {
                        e.Effects = DragDropEffects.None;
                    }
                }
                e.Handled = true;
            }
            catch (Exception)
            {
            }
        }
        private void treeView_Drop(object sender, DragEventArgs e)
        {
            try
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;

                // Verify that this is a valid drop and then store the drop target
                builderItem TargetItem = GetNearestContainer(e.OriginalSource as UIElement);
                if (TargetItem != null && draggedItem != null)
                {
                    _target = TargetItem;
                    e.Effects = DragDropEffects.Move;
                }
            }
            catch (Exception)
            {
            }
        }
        private bool CheckDropTarget(builderItem _sourceItem, builderItem _targetItem)
        {
            //Check whether the target item is meeting your condition
            bool _isEqual = false;
            if (!_sourceItem.Header.ToString().Equals(_targetItem.Header.ToString()))
            {
                _isEqual = true;
            }
            return _isEqual;

        }
        private void CopyItem(builderItem _sourceItem, builderItem _targetItem)
        {
            try
            {
                //adding dragged customTreeViewItem in target customTreeViewItem
                addChild(_sourceItem, _targetItem);

                //finding Parent customTreeViewItem of dragged customTreeViewItem 
                builderItem ParentItem = FindVisualParent<builderItem>(_sourceItem);
                // if parent is null then remove from TreeView else remove from Parent customTreeViewItem
                if (ParentItem == null)
                {
                    mainBuilder.Items.Remove(_sourceItem);
                }
                else
                {
                    ParentItem.Items.Remove(_sourceItem);
                }
            }
            catch
            {

            }
            //}

        }
        public void addChild(builderItem _sourceItem, builderItem _targetItem)
        {
            // add item in target customTreeViewItem 
            builderItem item1 = new builderItem();
            item1.Header = _sourceItem.Header;
            item1.builderTool = _sourceItem.builderTool;
            _targetItem.Items.Add(item1);
            foreach (builderItem item in _sourceItem.Items)
            {
                addChild(item, item1);
            }
        }
        static TObject FindVisualParent<TObject>(UIElement child) where TObject : UIElement
        {
            if (child == null)
            {
                return null;
            }

            UIElement parent = VisualTreeHelper.GetParent(child) as UIElement;

            while (parent != null)
            {
                TObject found = parent as TObject;
                if (found != null)
                {
                    return found;
                }
                else
                {
                    parent = VisualTreeHelper.GetParent(parent) as UIElement;
                }
            }

            return null;
        }
        private builderItem GetNearestContainer(UIElement element)
        {
            // Walk up the element tree to the nearest tree view item.
            builderItem container = element as builderItem;
            while ((container == null) && (element != null))
            {
                element = VisualTreeHelper.GetParent(element) as UIElement;
                container = element as builderItem;
            }
            return container;
        }

        private void IDEButton_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }

    public class builderItem : TreeViewItem
    {
        
        [Description("Test text displayed in the textbox"), Category("Data")]
        public Core.ToolBox.Tool builderTool{ get; set;}


        public builderItem()
        {
            builderTool = new Core.ToolBox.Tool(null, null, null, null);
        }
    }
}

