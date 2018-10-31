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
    /// Interaction logic for IDEHelp.xaml
    /// </summary>
    public partial class IDEHelp : UserControl
    {
        [Description("Text shown if Mouse is hovering"), Category("Common")]
        public string customHelptext
        {
            get; set;
        }
        [Description("Webpage shown if Button was clicked"), Category("Common")]
        public string customHelpPage
        {
            get; set;
        }

        public IDEHelp()
        {
            InitializeComponent();
        }

        private void UserControl_Click(object sender, RoutedEventArgs e)
        {
            if(customHelpPage!="X")
            {
                string url = "http://";
                url = url + customHelpPage;
                System.Diagnostics.Process.Start(url);
            }
        }
    }
}
