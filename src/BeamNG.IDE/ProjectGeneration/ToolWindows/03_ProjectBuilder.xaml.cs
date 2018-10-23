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
    /// Interaktionslogik für _03_ProjectBuilder.xaml
    /// </summary>
    public partial class _03_ProjectBuilder : Page
    {
        public _03_ProjectBuilder()
        {
            InitializeComponent();
            IDE.Core.ToolBox getTools = new Core.ToolBox();
            IDE.Core.ToolCategory[] tools = getTools.getToolBox();
            List<Core.Tool> items = new List<Core.Tool>();
            for (int i = 0; i < tools.Length; i++)
            {
                    GUI.IDEListBoxItem customDesign = new GUI.IDEListBoxItem();
                    customDesign.Width = overView.Width;
                    customDesign.Text = tools[i].category;
                    overView.Items.Add(customDesign);
            }
        }
    }
}
