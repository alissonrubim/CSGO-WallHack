using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlowGlowHack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private WallHack wallHack;
        

        private void buttonWallHack_Click(object sender, EventArgs e)
        {
            if (wallHack == null || !wallHack.IsRunning)
            {
                buttonWallHack.Text = "Desabilitar";
                WallHack wallHack = new WallHack();
                wallHack.ErrorHandler = (Exception ee) =>
                {
                    MessageBox.Show(ee.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Invoke(new Action(() =>
                    {
                        buttonWallHack.Text = "Habilitar";
                        wallHack.Stop();
                    }));
                };
                wallHack.Start();
            }
            else
            {
                buttonWallHack.Text = "Habilitar";
                wallHack.Stop();
            }
        }
    }
}
