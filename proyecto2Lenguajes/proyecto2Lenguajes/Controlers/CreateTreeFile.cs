using proyecto2Lenguajes.GUILogic;
using proyecto2Lenguajes.ObjectsCompile;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyecto2Lenguajes.Controlers
{
    class CreateTreeFile
    {
       // private Tree treeSyntactic;
        private TreeAS treeAS;
        private String textWrite;
        private NodeTree firstNode;

        public CreateTreeFile(TreeAS treeAS)//Tree treeSyntactic1,
        {
            //this.treeSyntactic = treeSyntactic1;
            this.treeAS = treeAS;
            createStringSave();
            //saveTree();
        }

        private void createStringSave()
        {
            textWrite = "digraph G {";
            setFirstNode();
            textWrite += "}";
        }

        private void setFirstNode()
        {
            // String codeNode = treeSyntactic.getRootNode().getCode();
            // String nameNode = treeSyntactic.getRootNode().getName();
             String codeNode = treeAS.getRootNode().getCode();
            String nameNode = treeAS.getRootNode().getName();
            textWrite += '\n';
            textWrite += codeNode + "[label = \""+ nameNode + "\"];";
            //setNodeAllNodes(treeSyntactic.getRootNode());
            setNodeAllNodes(treeAS.getRootNode());
        }


        private void setNodeAllNodes(NodeTree nodeInsert)
        {
            List<NodeTree> nodesChilds = nodeInsert.getChilds();
            foreach (var nodeC in nodesChilds)
            {
                setLabelNode(nodeC);
                setNodeAllNodes(nodeC);               
            }
        }

        public void setLabelNode(NodeTree node)
        {
            String codeNode = node.getCode();
            String nameNode = node.getName();
            textWrite += '\n';
            textWrite += codeNode + "[label = \"" + nameNode + "\"];";
            NodeTree nodeParent = node.getNodeParent();
            String codeNodeParent = nodeParent.getCode();
            textWrite += '\n';
            textWrite += codeNodeParent + " -> " + codeNode+";";
        }

        public void saveTree()
        {
            SaveFileDialog saveAs = new SaveFileDialog();
            saveAs.Filter = "Documento tipo txt |*.txt";
            saveAs.Title = "Guardar Texto Arbol";
            var res = saveAs.ShowDialog();
            if (res == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(saveAs.FileName))
                    {
                        writer.WriteLine(textWrite);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                
            }
        }

        public String saveAndLoadTree()
        {
            String pathImage = "";
            SaveFileDialog saveAs = new SaveFileDialog();
            saveAs.Filter = "Documento tipo dot |*.dot";
            saveAs.Title = "Guardar Texto Arbol";
            var res = saveAs.ShowDialog();
            if (res == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(saveAs.FileName))
                    {
                        writer.WriteLine(textWrite);
                    }
                    String graphVizString = @"" + textWrite;
                    Bitmap bm = CreateBitMap.Run(graphVizString);

                    for (int x = 0; x < bm.Width; x++)
                    {
                        for (int y = 0; y < bm.Height; y++)
                        {
                            Color clr = bm.GetPixel(x, y);
                            Color newClr = Color.FromArgb(clr.R, 0, 0);
                        }
                    }
                    string fileNameSaveAs = saveAs.FileName;
                    string nameFile = fileNameSaveAs.Replace(".dot", "");
                    bm.Save(nameFile + @".png");
                    MessageBox.Show("Se ha creado el png correctamente");
                    pathImage = nameFile + ".png";
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }

            }
            return pathImage;
        }

        






    }
}
