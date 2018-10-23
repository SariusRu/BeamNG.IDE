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
    /// Interaktionslogik für IDEListBoxItem.xaml
    /// </summary>
    /// 
    

    public partial class IDEListBoxItem : UserControl
    {
        [Description("The String wthin the Custom TextBox"), Category("Data")]
        public string Text { get; set; }

        public IDEListBoxItem()
        {
            InitializeComponent();
        }
    }
}
