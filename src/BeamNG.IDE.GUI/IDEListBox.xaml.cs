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
    /// Interaction logic for IDEListBox.xaml
    /// </summary>
    public partial class IDEListBox : UserControl
    {
        [Description("Test text displayed in the textbox"), Category("Data")]
        public ItemCollection ItemSource
        {
            get;
        }

        public IDEListBox()
        {
            InitializeComponent();
        }
    }
}
