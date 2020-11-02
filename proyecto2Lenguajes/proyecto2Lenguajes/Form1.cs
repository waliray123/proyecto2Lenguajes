using proyecto2Lenguajes.Controlers;
using proyecto2Lenguajes.GUILogic;
using proyecto2Lenguajes.ObjectsCompile;
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
    public partial class Form1 : Form
    {
        private String pathFile;
        private int numberErrors;
        private ControlCompile control;
        private TreeAS tree;
        private String suggest;
        private List<String> reserverdWords = new List<string>() { "SI", "SINO", "SINO_SI", "MIENTRAS", "DESDE"};

        public Form1()
        {
            InitializeComponent();
            pathFile = "";
            numberErrors = 0;            
            this.control = new ControlCompile(this.richTextBox1, this.dataGridView1, ref this.numberErrors);
            this.control.setForm1(this);
        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
            this.pathFile = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.control = new ControlCompile(this.richTextBox1, this.dataGridView1, ref this.numberErrors);
            this.control.setForm1(this);
            this.control.compileAllCode();
            int position = this.richTextBox1.SelectionStart;
            int line = this.richTextBox1.GetLineFromCharIndex(position) + 1;
            int column = position - this.richTextBox1.GetFirstCharIndexOfCurrentLine();
            this.label2.Text = line.ToString();
            this.label4.Text = column.ToString();
            if (this.numberErrors <= 0)
            {
                MessageBox.Show("Se analizo con exito");
            }
            else
            {
                MessageBox.Show("No se compilo con exito, Existen errores por arreglar");
            }

        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Load load = new Load(this.richTextBox1);
            this.pathFile = load.getData();
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pathFile.Equals(""))
            {
                MessageBox.Show("No se ha cargado ningun archivo", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Save save = new GUILogic.Save(this.richTextBox1);
                save.saveData(this.pathFile);
                MessageBox.Show("Se guardo con exito");
            }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save save = new GUILogic.Save(this.richTextBox1);
            save.saveAsData();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            int position = this.richTextBox1.SelectionStart;
            int line = this.richTextBox1.GetLineFromCharIndex(position) + 1;
            int column = position - this.richTextBox1.GetFirstCharIndexOfCurrentLine();
            this.label2.Text = line.ToString();
            this.label4.Text = column.ToString();

            //this.control.reviewChars();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void abrirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Load load = new Load(this.richTextBox1);
            this.pathFile = load.getData();
        }

        private void guardarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (pathFile.Equals(""))
            {
                MessageBox.Show("No se ha cargado ningun archivo", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Save save = new GUILogic.Save(this.richTextBox1);
                save.saveData(this.pathFile);
                MessageBox.Show("Se guardo con exito");
            }
        }

        private void guardarComoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Save save = new GUILogic.Save(this.richTextBox1);
            save.saveAsData();
        }

        private void nuevoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
            this.pathFile = "";
        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            String allText = this.richTextBox1.Text;
            String[] tokens = allText.Split(' ');
            String lastToken1 = tokens[tokens.Length - 1];
            String[] tokens2 = lastToken1.Split('\n');
            String lastToken2 = tokens2[tokens2.Length - 1];
            Console.WriteLine(lastToken2);
            for (int i = 0; i < this.reserverdWords.Count; i++)
            {
                if (this.reserverdWords[i].Contains(lastToken2))
                {
                    this.listBox1.Items.Add(this.reserverdWords[i]);
                }
            }
        }

        private void exportarArbolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tree != null)
            {
                CreateTreeFile createTreeFile = new CreateTreeFile(this.tree);
                String pathImage = createTreeFile.saveAndLoadTree();
                FormImage formI = new FormImage(pathImage);
                formI.Show();
            }
            else
            {
                MessageBox.Show(this, "No se ha creado ningun árbol","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        public void setTreeAS(object tree)
        {
            this.tree = (TreeAS)tree;
        }
    }
    
}
