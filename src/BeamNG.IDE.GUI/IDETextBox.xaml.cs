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
    /// Interaction logic for IDETextBox.xaml
    /// </summary>
    /// 

    public partial class IDETextBox : UserControl
    {
        [Description("Test text displayed in the textbox"), Category("Data")]
        public string Text
        {
            get { return main.Text; }
            set { main.Text = value; }
        }

        SolidColorBrush back = new SolidColorBrush();

        public IDETextBox()
        {
            InitializeComponent();
        }

        private void border_MouseEnter(object sender, MouseEventArgs e)
        {
            back.Color = (Color)ColorConverter.ConvertFromString("#403d3d");
            border.Background = back;
            
        }

        private void border_MouseLeave(object sender, MouseEventArgs e)
        {
            if(main.IsFocused != true)
            {
                back.Color = (Color)ColorConverter.ConvertFromString("#FF211F1F");
                border.Background = back;
            }
        }

        private void main_GotFocus(object sender, RoutedEventArgs e)
        {
            back.Color = (Color)ColorConverter.ConvertFromString("#403d3d");
            border.Background = back;
            main.SelectAll();
        }

        private void main_LostFocus(object sender, RoutedEventArgs e)
        {
            back.Color = (Color)ColorConverter.ConvertFromString("#FF211F1F");
            border.Background = back;
        }

        private void border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            main.Focus();
            main.SelectAll();
        }
    }
}
