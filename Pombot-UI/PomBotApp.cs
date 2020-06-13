using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pombot_UI
{
    public partial class PomBotApp : Form
    {
        public PomBotApp()
        {
            InitializeComponent();
        }

        private void closeBT_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
