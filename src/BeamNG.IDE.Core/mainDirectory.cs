using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeamNG.IDE.Core
{
    public class mainDirectory
    {
         string mainFolder;

        private void createDirectory()
        {
            mainFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            mainFolder = Path.Combine(mainFolder, "BeamNG.IDE");
            if (!Directory.Exists(mainFolder))
                Directory.CreateDirectory(mainFolder);
        }

        private void createRecentProjects()
        {
            string recent = mainFolder + "/recent.rec";
            if (!System.IO.File.Exists(recent))
                File.Create(recent);
        }

        private void createSettings()
        {
            string config = mainFolder + "/AppSettings.conf";
            if (!System.IO.File.Exists(config))
                File.Create(config);
        }

        private void createUserSettings()
        {
            string config = mainFolder + "/UserSettings.conf";
            if (!System.IO.File.Exists(config))
                File.Create(config);
        }

        public void initialize()
        {
            createDirectory();
            createRecentProjects();
            createSettings();
            createUserSettings();
        }
    }
}
