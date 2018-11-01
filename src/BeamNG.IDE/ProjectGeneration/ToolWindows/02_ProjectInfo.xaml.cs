using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaktionslogik für _03_ProjectInfo.xaml
    /// </summary>
    public partial class ProjectInfo : Page
    {
        projectInformation newPrj = new projectInformation();

        public ProjectInfo()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            BeamNG.IDE.ProjectGeneration.ToolWindows.ProjectBuilder nv = new ProjectBuilder();
            bool created = createVehicle();
            if(created)
            {
                NavigationService.Navigate(nv);
            }
            else
            {
                MessageBox.Show("Something went wrong!");
            }
        }

        private void createPrjObj()
        {
            if(name.Text != "Enter Name...")
            {
                newPrj.name = name.Text;
            }
            if(brand.Text != "Enter Brand...")
            {
                newPrj.brand = brand.Text;
            }
            if (author.Text != "Enter your Name...")
            {
                newPrj.author = author.Text;
            }
            if (country.Text != "Enter the Country...")
            {
                newPrj.country = country.Text;
            }
            if (type.Text != "Enter the Type...")
            {
                newPrj.type = type.Text;
            }
            if (derbyClass.Text != "Enter the Derby Class...")
            {
                newPrj.derby = derbyClass.Text;
            }
            if (bodyStyle.Text != "Enter the Body Style...")
            {
                newPrj.style = bodyStyle.Text;
            }
            if (bodyStyle.Text != "Enter the Body Style...")
            {
                newPrj.style = bodyStyle.Text;
            }
            if (from.Text != "From...")
            {
                newPrj.yearFrom = Convert.ToInt16(from.Text);
            }
            if (till.Text != "From...")
            {
                newPrj.yearTill = Convert.ToInt16(till.Text);
            }
        }

        private bool createVehicle()
        {
            //Create Project-Object
            createPrjObj();
            bool created;
            Core.getModLocation loc = new Core.getModLocation();
            string Path = loc.getModLoc();
            string root = Path;
            string folderName = name.Text;
            folderName = folderName.ToLower();
            string subdir = Path + "\\" + folderName + "\\vehicles\\" + folderName;
            if(name.Text != "Enter Name...")
            {
                if (!Directory.Exists(subdir))
                {
                    Directory.CreateDirectory(subdir);
                    created = true;
                }
                else
                {
                    created = false;
                }
                string infoFile = subdir + "\\info.json";
                using (FileStream fs = File.Create(infoFile))
                {
                    fs.Close();
                    writeToJSON(infoFile);
                }
            }
            else
            {
                created = false;
            }
            return created;
        }

        private void writeToJSON(string infoFile)
        {
            PropertyInfo[] properties = newPrj.GetType().GetProperties();
            string[] output = new string[10];
            output[0] = "{";
            output[9] = "}";
            foreach (PropertyInfo prj in properties)
            {
                switch(prj.Name)
                {
                    case "name":
                        output[1] = "\"Name\":\"" + prj.GetValue(newPrj, null) + "\",";
                        break;
                    case "brand":
                        output[2] = "\"Brand\":\"" + prj.GetValue(newPrj, null) + "\",";
                        break;
                    case "author":
                        output[3] = "\"Author\":\"" + prj.GetValue(newPrj, null) + "\",";
                        break;
                    case "country":
                        output[4] = "\"Country\":\"" + prj.GetValue(newPrj, null) + "\",";
                        break;
                    case "type":
                        output[5] = "\"Type\":\"" + prj.GetValue(newPrj, null) + "\",";
                        break;
                    case "derby":
                        output[6] = "\"Derby Class\":\"" + prj.GetValue(newPrj, null) + "\",";
                        break;
                    case "style":
                        output[7] = "\"Body Style\":\"" + prj.GetValue(newPrj, null) + "\",";
                        break;
                    case "yearFrom":
                        output[8] = "\"Years\":{\"min\":" + prj.GetValue(newPrj, null) + ",";
                        break;
                    case "yearTill":
                        output[8] = output[8] + "\"max\":" + prj.GetValue(newPrj, null) + "}";
                        break;
                }
            }
            for(int i = 0; i<output.Length; i++)
            {
                MessageBox.Show(output[i]);
            }
            File.WriteAllLines(infoFile, output);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z0-9öäü_-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            GUI.IDETextBox sendingTextBox = (GUI.IDETextBox)sender;
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);            
        }
    }



    public class projectInformation
    {
        public string name { get; set; }
        public string brand { get; set; }
        public string author { get; set; }
        public string country { get; set; }
        public string type { get; set; }
        public string derby { get; set; }
        public string style { get; set; }
        public int yearFrom { get; set; }
        public int yearTill { get; set; }

        public projectInformation()
        {
            
        }
    }
}
