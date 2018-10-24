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

namespace BeamNG.IDE.GUI
{
    /// <summary>
    /// Interaction logic for IDEViewBox.xaml
    /// </summary>
    public partial class IDEViewBox : UserControl
    {
        public IDEViewBox()
        {
            InitializeComponent();
            Core.ToolBox getTools = new Core.ToolBox();
            Core.ToolBox.ToolCategory[] list;
            list = getTools.getToolBox();
            foreach(Core.ToolBox.ToolCategory Tools in list)
            {
                IDEViewBoxItem item = new IDEViewBoxItem();
                item.Width = 800;
                item.Text = Tools.category;
                main.Items.Add(item);
            }

        }

        
    }
}
