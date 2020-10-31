using proyecto2Lenguajes.ObjectsCompile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyecto2Lenguajes.Controlers
{
    class CreateTreeFile
    {
        private Tree treeSyntactic;
        private String textWrite;
        private NodeTree firstNode;

        public CreateTreeFile(Tree treeSyntactic1)
        {
            this.treeSyntactic = treeSyntactic1;
            createStringSave();
            saveTree();
        }

        private void createStringSave()
        {
            textWrite = "digraph G {";
            setFirstNode();
            textWrite += "}";
        }

        private void setFirstNode()
        {
            String codeNode = treeSyntactic.getRootNode().getCode();
            String nameNode = treeSyntactic.getRootNode().getName();
            textWrite += '\n';
            textWrite += codeNode + "[label = \""+ nameNode + "\"];";
            setNodeAllNodes(treeSyntactic.getRootNode());
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

        






    }
}
