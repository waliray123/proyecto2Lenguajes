using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto2Lenguajes.ObjectsCompile
{
    class Tree
    {
        private NodeTree rootNode;
        private NodeTree nodeUsing;
        private int codeUsing;
        private List<String> namesNoTerminal = new List<string>() { "E", "V", "R", "O", "L", "C", "C'", "X", "I", "B", "B'", "Q", "Q'", "Y", "J", "U", "W", "T" };


        public Tree(String nodeName) {
            codeUsing = 0;
            String nodeCode = codeUsing.ToString(); 
            rootNode = new NodeTree(nodeCode,nodeName);
            nodeUsing = rootNode;
        }

        public void setValues(String values)
        {
            String[] valuesToInsert = values.Split('.');
            List<NodeTree> childsNodeUsing = nodeUsing.getChilds();
            foreach (String item in valuesToInsert)
            {
                if (valuesToInsert[valuesToInsert.Length -1].Equals("reduce") == false)
                {
                    codeUsing++;
                    NodeTree nodeInsert = new NodeTree(codeUsing.ToString(), item);
                    nodeInsert.setNodeParent(this.nodeUsing);
                    childsNodeUsing.Add(nodeInsert);
                }                
            }
            this.nodeUsing.setChilds(childsNodeUsing);
            setNewNodeUsing();
        }

        private void setNewNodeUsing()
        {
            Boolean foundNewNode = false;
            List<NodeTree> childsNodeUsing = nodeUsing.getChilds();
            foreach (NodeTree node in childsNodeUsing)
            {
                String name = node.getName();
                List<NodeTree> childsNodes  = node.getChilds();
                if (namesNoTerminal.Contains(name) && childsNodes.Count == 0)
                {
                    this.nodeUsing = node;
                    foundNewNode = true;
                    break;
                }
            }
            if (foundNewNode == false)
            {
                NodeTree nodeParent = nodeUsing.getNodeParent();
                if (nodeParent != null)
                {
                    nodeUsing = nodeParent;
                    setNewNodeUsing();
                }                
            }

        }



    }
}
