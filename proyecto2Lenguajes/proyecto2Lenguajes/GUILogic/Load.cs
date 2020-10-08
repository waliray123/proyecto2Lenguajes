using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyecto2Lenguajes.GUILogic
{
    class Load
    {
        private RichTextBox richtextBox;

        public Load(RichTextBox richtextBox1)
        {
            this.richtextBox = richtextBox1;
        }

        public String getData()
        {
            String pathFile = "";
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Documento tipo gt |*.gt";
            open.Title = "Cargar Archivo";
            var res = open.ShowDialog();
            if (res == DialogResult.OK)
            {
                StreamReader read = new StreamReader(open.FileName);
                pathFile = open.FileName;
                richtextBox.Text = read.ReadToEnd();
                read.Close();
            }
            return pathFile;
        }

    }
}
