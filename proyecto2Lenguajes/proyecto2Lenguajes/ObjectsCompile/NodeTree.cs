using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto2Lenguajes.ObjectsCompile
{
    class NodeTree
    {
        private String code;
        private String name;
        private List<NodeTree> childs;
        private NodeTree nodeParent;

        public NodeTree(String code, String name)
        {
            this.code = code;
            this.name = name;
            childs = new List<NodeTree>();
        }

        public String getName()
        {
            return this.name;
        }
        public String getCode()
        {
            return this.code;
        }
        public List<NodeTree> getChilds()
        {
            return this.childs;
        }
        public void setChilds(List<NodeTree> childs)
        {
            this.childs = childs;
        }

        public void setNodeParent(NodeTree node)
        {
            this.nodeParent = node;
        }

        public NodeTree getNodeParent()
        {
            return this.nodeParent;
        }
    }
}
