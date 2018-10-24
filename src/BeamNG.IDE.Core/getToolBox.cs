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
        public void getToolBox()
        {
            string tools = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            tools = tools + "/BeamNG.IDE/Tools.xml";
            toolArray[0] = new Tool(null, null, null, null);
            string xmlFile = File.ReadAllText(tools);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlFile);
            XmlNodeList nodeList = xmldoc.GetElementsByTagName("type");
            ToolCategory[] toolCategories = new ToolCategory[nodeList.Count];
            for (int j = 0; j < toolCategories.Length; j++)
            {
                toolCategories[j] = new ToolCategory(null, null);
            }
            int i = 0;
            int t = 0;
            int g = 0;
            foreach (XmlNode node in nodeList)
            {
                XmlNodeList Tools = node.ChildNodes;
                toolCategories[g].category = node.Attributes["name"].Value;
                t = 0;
                toolCategories[g].Tools = new Tool[node.ChildNodes.Count];
                foreach (XmlNode ToolList in node.ChildNodes)
                {

                    Tool Tmp = new Tool(null, null, null, null);
                    foreach (XmlNode tool in ToolList.ChildNodes)
                    {
                        //VALUES FOR PROPERTIES ARE GIVEN OUT HERE
                        switch(tool.Name)
                        {
                            case "name":
                                Tmp.toolName = tool.InnerText;
                                break;
                            case "header":
                                Tmp.toolHeader = tool.InnerText;
                                break;
                            case "source":
                                Tmp.bitmapSrc = tool.InnerText;
                                break;
                            case "helpText":
                                Tmp.helpText = tool.InnerText;
                                break;
                            case "amount":
                                Tmp.amount = tool.InnerText;
                                break;
                            case "fatherSection":
                                Tmp.fatherSection = tool.InnerText;
                                break;
                            case "Childs":
                                Tmp.hasChildrens = Convert.ToBoolean(tool.InnerText);
                                break;
                        }
                    }
                    toolCategories[g].Tools[t] = Tmp;
                    t++;

                }
                g++;

            }
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
}
