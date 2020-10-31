using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto2Lenguajes.ObjectsCompile
{
    class NodeWaited
    {
        private String name;
        private String value;
        private NodeTree parent;

        public NodeWaited(String name, NodeTree parent)
        {
            this.name = name;
            this.parent = parent;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public void setValue(String value)
        {
            this.value= value;
        }

        public void setParent(NodeTree parent)
        {
            this.parent = parent;
        }

        public String getName()
        {
            return this.name;
        }

        public NodeTree getParent()
        {
            return this.parent;
        }
    }
}
