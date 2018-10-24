using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Windows;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace BeamNG.IDE.Core
{
    public class recentProjects
    {
        public recentPrj[] getProjects()
        {
            string recent = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            recent = recent + "/BeamNG.IDE/recent.rec";
            recentPrj[] projectsReal = new recentPrj[1];
            DateTime myDate = DateTime.ParseExact("2000.01.01 00:00:00", "yyyy.MM.dd HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
            projectsReal[0] = new recentPrj(null, myDate, null, null);
            XmlTextReader reader = new XmlTextReader(recent);
            int counter = 0;
            int counterInFile = 1;
            int fileLenght = File.ReadLines(recent).Count();
            fileLenght = fileLenght - 3;
            fileLenght = fileLenght / 6;

            while (reader.Read())
            {
                if(counter < fileLenght)
                {
                    if (counter == projectsReal.Length)
                    {
                        Array.Resize(ref projectsReal, projectsReal.Length + 1);
                        projectsReal[projectsReal.Length - 1] = new recentPrj(null, myDate, null, null);
                    }
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Text: //Display the text in each element.
                            if (counterInFile == 1)
                            {
                                projectsReal[counter].projectName = reader.Value;
                            }
                            if (counterInFile == 2)
                            {
                                myDate = DateTime.ParseExact(reader.Value, "yyyy, MM, dd HH, mm, ss",
                                          System.Globalization.CultureInfo.InvariantCulture);
                                projectsReal[counter].Date = myDate;
                            }
                            if (counterInFile == 3)
                            {
                                projectsReal[counter].filePath = reader.Value;
                            }
                            if( counterInFile == 4)
                            {
                                if(reader.Value == "plane")
                                {
                                    projectsReal[counter].type = "plane";
                                    projectsReal[counter].bitmap = genBitmap(projectsReal[counter]);
                                }
                                if (reader.Value == "ship")
                                {
                                    projectsReal[counter].type = "ship";
                                    projectsReal[counter].bitmap = genBitmap(projectsReal[counter]);
                                }
                                if (reader.Value == "car")
                                {
                                    projectsReal[counter].type = "car";
                                    projectsReal[counter].bitmap = genBitmap(projectsReal[counter]);
                                }
                                if (reader.Value == "truck")
                                {
                                    projectsReal[counter].type = "truck";
                                    projectsReal[counter].bitmap = genBitmap(projectsReal[counter]);
                                }
                            }
                            counterInFile++;
                            if (counterInFile == 5)
                            {
                                counterInFile = 1;
                                counter++;
                            }
                            
                            break;
                    }
                }
            }
            return projectsReal;
        }

        private BitmapSource genBitmap(recentPrj last)
        {
            BitmapSource bitmap;
            Bitmap bitmapTemp = Properties.Resources.ProjectTemplate;
            bitmapTemp = new Bitmap(bitmapTemp, new System.Drawing.Size(300, 60));
            Graphics g = Graphics.FromImage(bitmapTemp);
            g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            Bitmap symbol = new Bitmap(50, 50);
            if (last.type == "car")
            {
                symbol = new Bitmap(Properties.Resources.car, 50, 50);
            }
            if (last.type == "ship")
            {
                symbol = new Bitmap(Properties.Resources.boat, 50, 50);
            }
            if (last.type == "truck")
            {
                symbol = new Bitmap(Properties.Resources.truck, 50, 50);
            }
            if (last.type == "plane")
            {
                symbol = new Bitmap(Properties.Resources.airplane, 50, 50);
            }
            symbol.MakeTransparent();
            g.DrawImage(symbol, new System.Drawing.Point(5, 5));
            if(last.projectName.Length > 25)
            {
                last.projectName = last.projectName.Substring(0,22) + "...";
            }
            g.DrawString(last.projectName, new Font("Arial", 13), Brushes.Gray, 60,1);
            if (last.filePath.Length > 39)
            {
                last.filePath = last.filePath.Substring(0, 36) + "...";
            }
            g.DrawString(last.filePath, new Font("Arial", 8), Brushes.Gray, 62, 23);
            TimeSpan Interval = new TimeSpan(0, 60, 0);
            if(DateTime.Now.Subtract(last.Date)< new TimeSpan(1, 0, 0))
            {
                g.DrawString("less than a hour ago...", new Font("Arial", 8), Brushes.Gray, 62, 45);
            }
            if (DateTime.Now.Subtract(last.Date) < new TimeSpan(24, 0, 0))
            {
                g.DrawString("today", new Font("Arial", 8), Brushes.Gray, 62, 45);
            }
            if (DateTime.Now.Subtract(last.Date) < new TimeSpan(7*24, 0, 0))
            {
                g.DrawString("this week", new Font("Arial", 8), Brushes.Gray, 62, 45);
            }
            if (DateTime.Now.Subtract(last.Date) < new TimeSpan(30 * 7 * 24, 0, 0))
            {
                g.DrawString("this month", new Font("Arial", 8), Brushes.Gray, 62, 45);
            }
            if (DateTime.Now.Subtract(last.Date) > new TimeSpan(30 * 7 * 24, 0, 0))
            {
                g.DrawString("A long time ago in a galaxy far, far away...", new Font("Arial", 8), Brushes.Gray, 62, 45);
            }
            ConvertBitmapToBitmapSource converter = new ConvertBitmapToBitmapSource();

            bitmap = converter.ConvertToBitmapSource(bitmapTemp);
            return bitmap;
        }
    }



    public class recentPrj
    {
        public string projectName { get; set; }
        public DateTime Date { get; set; }
        public string filePath { get; set; }
        public string type { get; set; }
        public BitmapSource bitmap { get; set; }
        public recentPrj(string prjName, DateTime date, string FilePath, string projectType)
        {
            projectName = prjName;
            Date = date;
            filePath = FilePath;
            type = projectType;            
        }
        
    }
}
