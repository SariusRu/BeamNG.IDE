using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeamNG.IDE.Core
{
    public class Information
    {
        private String Version()
        {
            string Major = "0";
            string Minor = "0";
            string Build = "0";
            string Revision = "1";
            string VersionName = "alpha";

            string FullVersion = Major + "." + Minor + "." + Build + "." + Revision + " " + VersionName;
            return FullVersion;
        }

        public String getVersion()
        {
            string versionText = Version();
            return versionText;
        }

        
    }
}
