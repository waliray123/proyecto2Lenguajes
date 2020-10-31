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


        public TreeAS(String nodeName)
        {
            codeUsing = 0;
            String nodeCode = codeUsing.ToString();
            rootNode = new NodeTree("node" + nodeCode, nodeName);
            nodeUsing = rootNode;
        }

        public void setNode(token tokenSet)
        {

        }



    }
}
