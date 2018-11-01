using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeamNG.IDE.Core
{
    public class getModLocation
    {
        public string getInstallLocation()
        {
            string Out = getLocation();
            return Out;
        }

        public string getUserPath()
        {
            string Out = getLocation();
            string OutPut = location(Out);
            return OutPut;
        }

        public string getModLoc()
        {
            string tmp = getUserPath();
            tmp = tmp + "\\mods\\unpacked\\";
            return tmp;
        }


        private string getLocation()
        {
            string InstallPath = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\BeamNG","rootpath", null);
            if (InstallPath != null)
            {
                return InstallPath;
            }
            else
            {
                MessageBox.Show("You need to install BeamNG.drive!", "BeamNG.drive couldn't located", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }

        private string location(string InstallPath)
        {
            string UserPath = null;
            try
            {
                string[] lines = System.IO.File.ReadAllLines(InstallPath + "startup.ini");
                for(int u = 0; u < lines.Length; u++)
                {
                   if(lines[u].StartsWith("UserPath"))
                   {
                        UserPath = lines[u];
                        UserPath = UserPath.Replace("UserPath = ", "");
                   }
                }
                return UserPath;
            }
            catch
            {
                return UserPath;
            }
        }
    }
}
