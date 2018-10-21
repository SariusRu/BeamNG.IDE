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
    /// Interaction logic for WindowBar.xaml
    /// </summary>
    public partial class WindowBar : UserControl
    {
        [Description("Name of the Window"), Category("Common")]
        public string WindowName { get; set; }

        [Description("Can Maximize"), Category("Custom")]
        public bool canMaximize { get; set; }

        [Description("Can Minimize"), Category("Custom")]
        public bool canMinimize { get; set; }

        [Description("Can be closed"), Category("Custom")]
        public bool canClose { get; set; }

        bool mouseClicked = false;
        private Point lastLocation;

        public WindowBar()
        {
            InitializeComponent();            
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement feSource = e.Source as FrameworkElement;
            Window parentWindow = Window.GetWindow(this);
            switch (feSource.Name)
            {
                case "minimize":
                    parentWindow.WindowState = WindowState.Minimized;
                    break;
                case "maximize":
                    parentWindow.WindowState = WindowState.Maximized;
                    break;
                case "close":                   
                    parentWindow.Close();
                    break;
            }
            e.Handled = true;

        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseClicked = false;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement feSource = e.Source as FrameworkElement;
            Window parentWindow = Window.GetWindow(this);            

            if (e.ChangedButton == MouseButton.Left)
            {
                lastLocation = Mouse.GetPosition(this);
                mouseClicked = true;
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            FrameworkElement feSource = e.Source as FrameworkElement;
            Window parentWindow = Window.GetWindow(this);
            if (mouseClicked)
            {
                parentWindow.Left = PointToScreen(Mouse.GetPosition(this)).X - lastLocation.X;
                parentWindow.Top = PointToScreen(Mouse.GetPosition(this)).Y - lastLocation.Y;
            }
        }
    }
}
