using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeamNG.IDE.Core
{
    public class currentProject
    {
        public class currentPrj
        {
            public string projectName { get; set; }
            public DateTime Date { get; set; }
            public string filePath { get; set; }
            public string type { get; set; }            
            public currentPrj(string prjName, DateTime date, string FilePath, string projectType)
            {
                projectName = prjName;
                Date = date;
                filePath = FilePath;
                type = projectType;
            }

        }
    }
}
