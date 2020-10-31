using proyecto2Lenguajes.Controlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto2Lenguajes.ObjectsCompile
{
    class TreeAS
    {
        private NodeTree rootNode;
        private NodeTree nodeUsing;
        private int codeUsing;
        private List<String> namesNoTerminal = new List<string>() { "E", "V", "R", "O", "L", "C", "C'", "X", "I", "B", "B'", "Q", "Q'", "Y", "J", "U", "W", "T", "P", "P'" };
        private List<String> nodeWaitValue = new List<string>() { "id", "char","cad","numE","numD","bool" };
        private List<NodeWaited> nodesWaited;
        private List<RowSA> tableSA = new List<RowSA>();


        public TreeAS(String nodeName)
        {
            nodesWaited = new List<NodeWaited>();
            matrixSA matrix = new matrixSA();
            this.tableSA = matrix.getTable();
            codeUsing = 0;
            String nodeCode = codeUsing.ToString();
            rootNode = new NodeTree("node" + nodeCode, nodeName);
            nodeUsing = rootNode;
            nodesWaited.Add(new NodeWaited(nodeName,rootNode));
        }
        

        public void setTokensToWait(String values) {
            String[] valuesToInsert = values.Split('.');
            nodesWaited.RemoveAt(nodesWaited.Count - 1);
            for (int i = (valuesToInsert.Length - 1); i >= 0; i--)
            {                
                String name = valuesToInsert[i];
                if (name.Equals("reduce") == false)
                {
                    NodeWaited nodeWait = new NodeWaited(name, nodeUsing);
                    nodesWaited.Add(nodeWait);
                }                
            }
        }

        public void setNoTerminal(String noTerminal, token nextToken) {
            String tokenRequired = nextToken.getName();
            String nameLastNode = nodesWaited[nodesWaited.Count - 1].getName();
            Boolean isError = true;
            foreach (RowSA row in this.tableSA)
            {
                String nameNoTerminal = row.getRow();
                String column = row.getCol();
                if (column == tokenRequired && nameNoTerminal == noTerminal && nameNoTerminal == nameLastNode)
                {
                    NodeTree nodeToUse = new NodeTree(codeUsing.ToString(), nameNoTerminal);
                    NodeTree nodeParentFromUse = nodesWaited[nodesWaited.Count - 1].getParent();
                    nodeParentFromUse.setNewChild(nodeToUse);
                    nodeToUse.setNodeParent(nodeParentFromUse);
                    this.nodeUsing = nodeToUse;
                    String values = row.getVal();
                    setTokensToWait(values);
                    this.codeUsing++;
                    isError = false;
                    break;
                }                
            }
            if (isError == true && nameLastNode.Equals("Q'"))
            {
                nodesWaited.RemoveAt(nodesWaited.Count - 1);
                setNoTerminal(noTerminal, nextToken);
            }
        }

        public void setNode(token tokenSet)
        {
            if (tokenSet.getName().Equals("}"))
            {
                Boolean a = true;
            }
            int sizeNodesWaited = nodesWaited.Count;
            String nameLastNode = nodesWaited[sizeNodesWaited-1].getName();
            if (tokenSet.getName().Equals(nameLastNode))
            {
                addNodeTree(tokenSet, nodesWaited[sizeNodesWaited - 1]);
            }
            else
            {
                nodesWaited.RemoveAt(nodesWaited.Count - 1);
                setNode(tokenSet);
            }
        }

        public void addNodeTree(token tokenSet, NodeWaited nodeInsert)
        {
            codeUsing++;
            NodeTree parent = nodeInsert.getParent();
            String name = nodeInsert.getName();
            if (this.nodeWaitValue.Contains(nodeInsert.getName()))
            {
                String newName = tokenSet.getVal();
                name = newName;
            }
            NodeTree newNode = new NodeTree("node" + codeUsing.ToString(),name);
            newNode.setNodeParent(parent);
            parent.setNewChild(newNode);
            nodesWaited.RemoveAt(nodesWaited.Count - 1);
        }

        public NodeTree getRootNode()
        {
            return this.rootNode;
        }

    }
}
