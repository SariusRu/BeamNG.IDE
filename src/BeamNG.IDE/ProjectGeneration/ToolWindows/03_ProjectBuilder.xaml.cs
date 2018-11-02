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


        //Registering of the MouseMove, triggering of the DoDragDrop
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

        //
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
                if(getItem(p))
                obj.Text = getItem(p).ToString();
                Core.ToolBox.Tool tool = e.Data.GetData("myFormat") as Core.ToolBox.Tool;
                TreeView treeView = sender as TreeView;
                customTreeViewItem itemToAdd = new customTreeViewItem();
                itemToAdd.treeViewTool = tool;
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
           
            pos.Text = p.ToString();
            Core.ToolBox.Tool hovering = mainBuilder.InputHitTest(p) as Core.ToolBox.Tool;
            if(hovering != null)
            {
                MessageBox.Show(hovering.GetType().ToString());
            }
            return true;
        }

        Point _lastMouseDown;
        customTreeViewItem draggedItem, _target;

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
                        draggedItem = (customTreeViewItem)mainBuilder.SelectedItem;
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
                    customTreeViewItem item = GetNearestContainer(e.OriginalSource as UIElement);
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
                customTreeViewItem TargetItem = GetNearestContainer(e.OriginalSource as UIElement);
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
        private bool CheckDropTarget(customTreeViewItem _sourceItem, customTreeViewItem _targetItem)
        {
            //Check whether the target item is meeting your condition
            bool _isEqual = false;
            if (!_sourceItem.Header.ToString().Equals(_targetItem.Header.ToString()))
            {
                _isEqual = true;
            }
            return _isEqual;

        }
        private void CopyItem(customTreeViewItem _sourceItem, customTreeViewItem _targetItem)
        {

            //Asking user wether he want to drop the dragged customTreeViewItem here or not
            if (MessageBox.Show("Would you like to drop " + _sourceItem.Header.ToString() + " into " + _targetItem.Header.ToString() + "", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    //adding dragged customTreeViewItem in target customTreeViewItem
                    addChild(_sourceItem, _targetItem);

                    //finding Parent customTreeViewItem of dragged customTreeViewItem 
                    customTreeViewItem ParentItem = FindVisualParent<customTreeViewItem>(_sourceItem);
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
            }

        }
        public void addChild(customTreeViewItem _sourceItem, customTreeViewItem _targetItem)
        {
            // add item in target customTreeViewItem 
            customTreeViewItem item1 = new customTreeViewItem();
            item1.Header = _sourceItem.Header;
            _targetItem.Items.Add(item1);
            foreach (customTreeViewItem item in _sourceItem.Items)
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
        private customTreeViewItem GetNearestContainer(UIElement element)
        {
            // Walk up the element tree to the nearest tree view item.
            customTreeViewItem container = element as customTreeViewItem;
            while ((container == null) && (element != null))
            {
                element = VisualTreeHelper.GetParent(element) as UIElement;
                container = element as customTreeViewItem;
            }
            return container;
        }

    }

    public class customTreeViewItem : TreeViewItem
    {
        public Core.ToolBox.Tool treeViewTool;

        public customTreeViewItem()
        {
        }
    }
}

