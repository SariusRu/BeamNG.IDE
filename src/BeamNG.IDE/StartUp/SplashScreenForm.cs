using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeamNG.IDE.StartUp
{
    public partial class SplashScreenForm : Form
    {
        Bitmap bitmap;
        public SplashScreenForm()
        {
            BeamNG.IDE.Core.Information version = new Core.Information();
            string versionText = version.getVersion();
            this.GetType().Assembly.GetManifestResourceNames();
            bitmap = Properties.Resources.SplashScreen;
            bitmap = new Bitmap(bitmap, new Size(600, 300));
            this.BackColor = Color.LimeGreen;
            this.TransparencyKey = Color.LimeGreen;
            this.Height = bitmap.Height;
            this.Width = bitmap.Width;
            using (Font font = new Font("Arial", 10))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.DrawString(versionText, font, System.Drawing.Brushes.White, 190 - 3 * versionText.Length, 160);
                }
            }
            this.BackgroundImage = bitmap;
            InitializeComponent();
        }
    }
}
