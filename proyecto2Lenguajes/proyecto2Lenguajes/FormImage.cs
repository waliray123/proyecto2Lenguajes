using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyecto2Lenguajes
{
    public partial class FormImage : Form
    {
        public FormImage(String pathImage)
        {
            InitializeComponent();
            this.pictureBox1.Image = Image.FromFile(pathImage);
        }

        private void FormImage_Load(object sender, EventArgs e)
        {

        }
    }
}
