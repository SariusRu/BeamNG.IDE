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
    public class ToolBox
    {
        Tool[] toolArray = new Tool[1];
        public ToolCategory[] getToolBox()
        {
            ToolCategory[] toolCategories;
            string toolXML = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            toolXML = toolXML + "/BeamNG.IDE/tools.rec";            
            toolArray[0] = new Tool(null, null, null, null);
            XmlTextReader reader = new XmlTextReader(toolXML);
            int counter = 0;
            int counterInFile = 1;
            int fileLenght = File.ReadLines(toolXML).Count();
            fileLenght = fileLenght - 3;
            fileLenght = fileLenght / 10;

            while (reader.Read())
            {
                if (counter < fileLenght)
                {
                    if (counter == toolArray.Length)
                    {
                        Array.Resize(ref toolArray, toolArray.Length + 1);
                        toolArray[toolArray.Length - 1] = new Tool(null, null, null, null);
                    }
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Text: //Display the text in each element.
                            if (counterInFile == 1)
                            {
                                toolArray[counter].toolName = reader.Value;
                            }
                            if (counterInFile == 2)
                            {
                                toolArray[counter].toolHeader = reader.Value;
                            }
                            if (counterInFile == 3)
                            {
                                toolArray[counter].category = reader.Value;
                            }
                            if (counterInFile == 4)
                            {
                                toolArray[counter].bitmapSrc = reader.Value;
                            }
                            if (counterInFile == 5)
                            {
                                toolArray[counter].helpText = reader.Value;
                            }
                            if (counterInFile == 6)
                            {
                                toolArray[counter].amount= reader.Value;
                            }
                            if (counterInFile == 7)
                            {
                                toolArray[counter].fatherSection = reader.Value;
                            }
                            if (counterInFile == 8)
                            {
                                toolArray[counter].hasChildrens = Convert.ToBoolean(reader.Value);
                            }
                            counterInFile++;
                            if (counterInFile == 9)
                            {
                                counterInFile = 1;
                                counter++;
                            }
                            
                            break;
                    }
                }
            }
            toolCategories = getCategories(toolArray);
            return toolCategories;
        }

        public ToolCategory[] getCategories(Tool[] toolsTmp)
        {
            Tool[] tools = toolsTmp;
            ToolCategory[] toolMenu = new ToolCategory[1] { new ToolCategory(null, null) };
            for(int i = 0; i<toolArray.Length;i++)
            {
                if (i != 0)
                {
                    Array.Resize(ref toolMenu, toolMenu.Length + 1);
                    toolMenu[toolMenu.Length - 1] = new ToolCategory(null, null);
                }
                if (!containsObject(tools[i], toolMenu))
                {
                    toolMenu[toolMenu.Length -1].category = tools[i].category;
                }
                else
                {
                    Array.Resize(ref toolMenu, toolMenu.Length - 1);
                }
            }            
            return toolMenu;
        }

        public bool containsObject(Tool categoryToCheck, ToolCategory[] toolList)
        {
            ToolCategory[] toolCategory = toolList;
            bool isInArray = false;
            for(int i = 0; i<toolCategory.Length;i++)
            {
                if(categoryToCheck.category == toolCategory[i].category)
                {
                    isInArray = true;
                    return isInArray;
                }
            }
            isInArray = false;
            return isInArray;

        }

            //private BitmapSource genBitmap(recentPrj last)
            //{
            //BitmapSource bitmap;
            //Bitmap bitmapTemp = Properties.Resources.ProjectTemplate;
            //bitmapTemp = new Bitmap(bitmapTemp, new System.Drawing.Size(300, 60));
            //Graphics g = Graphics.FromImage(bitmapTemp);
            //g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            //Bitmap symbol = new Bitmap(50, 50);
            //if (last.type == "car")
            //{
            //    symbol = new Bitmap(Properties.Resources.car, 50, 50);
            //}
            //if (last.type == "ship")
            //{
            //    symbol = new Bitmap(Properties.Resources.boat, 50, 50);
            //}
            //if (last.type == "truck")
            //{
            //    symbol = new Bitmap(Properties.Resources.truck, 50, 50);
            //}
            //if (last.type == "plane")
            //{
            //    symbol = new Bitmap(Properties.Resources.airplane, 50, 50);
            //}
            //symbol.MakeTransparent();
            //g.DrawImage(symbol, new System.Drawing.Point(5, 5));
            //if (last.projectName.Length > 25)
            //{
            //    last.projectName = last.projectName.Substring(0, 22) + "...";
            //}
            //g.DrawString(last.projectName, new Font("Arial", 13), Brushes.Gray, 60, 1);
            //if (last.filePath.Length > 39)
            //{
            //    last.filePath = last.filePath.Substring(0, 36) + "...";
            //}
            //g.DrawString(last.filePath, new Font("Arial", 8), Brushes.Gray, 62, 23);
            //TimeSpan Interval = new TimeSpan(0, 60, 0);
            //if (DateTime.Now.Subtract(last.Date) < new TimeSpan(1, 0, 0))
            //{
            //    g.DrawString("less than a hour ago...", new Font("Arial", 8), Brushes.Gray, 62, 45);
            //}
            //if (DateTime.Now.Subtract(last.Date) < new TimeSpan(24, 0, 0))
            //{
            //    g.DrawString("today", new Font("Arial", 8), Brushes.Gray, 62, 45);
            //}
            //if (DateTime.Now.Subtract(last.Date) < new TimeSpan(7 * 24, 0, 0))
            //{
            //    g.DrawString("this week", new Font("Arial", 8), Brushes.Gray, 62, 45);
            //}
            //if (DateTime.Now.Subtract(last.Date) < new TimeSpan(30 * 7 * 24, 0, 0))
            //{
            //    g.DrawString("this month", new Font("Arial", 8), Brushes.Gray, 62, 45);
            //}
            //if (DateTime.Now.Subtract(last.Date) > new TimeSpan(30 * 7 * 24, 0, 0))
            //{
            //    g.DrawString("A long time ago in a galaxy far, far away...", new Font("Arial", 8), Brushes.Gray, 62, 45);
            //}
            //bitmap = ConvertToBitmapSource(bitmapTemp);
            //return bitmap;
            //}

            private static BitmapSource ConvertToBitmapSource(Bitmap bitmap)
        {
            if (bitmap == null)
                return null;
            //BitmapImage b=new BitmapImage();
            BitmapSource bitSrc = null;
            var hBitmap = IntPtr.Zero;


            try
            {
                hBitmap = bitmap.GetHbitmap();
                bitSrc = Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
            catch (Win32Exception)
            {
                bitSrc = null;
            }
            catch (ArgumentException)
            {
                if (hBitmap != IntPtr.Zero)
                    DeleteObject(hBitmap);
                hBitmap = IntPtr.Zero;
            }
            catch
            {
                if (hBitmap != IntPtr.Zero)
                    DeleteObject(hBitmap);
                bitSrc = null;
                hBitmap = IntPtr.Zero;
            }
            finally
            {
                //bitmap.Dispose();
                if (hBitmap != IntPtr.Zero)
                    DeleteObject(hBitmap);
            }
            return bitSrc;
        }
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr hObject);
    }



    public class Tool
    {
        public string toolName { get; set; }
        public string toolHeader { get; set; }
        public string category { get; set; }
        public string bitmapSrc { get; set; }
        public BitmapSource bitmap { get; set; }
        public string helpText { get; set; }
        public string amount { get; set; }
        public string fatherSection { get; set; }
        public bool hasChildrens { get; set; }
        public Tool(string tlName, string tlHeader, string categoryTmp, string bitmapSource)
        {
            bitmapSrc = bitmapSource;
            toolName = tlName;
            toolHeader = tlHeader;
            category = categoryTmp;
        }
    }

    public class ToolCategory
    {
        public Tool[] Tools { get; set; }
        public string category { get; set; }

        public ToolCategory(Tool[] toolsTmp, string categoryTmp)
        {
            Tools = toolsTmp;
            category = categoryTmp;
        }
    }
}
