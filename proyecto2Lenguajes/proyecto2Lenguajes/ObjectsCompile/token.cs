using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto2Lenguajes.ObjectsCompile
{
    class token
    {
        private String name;
        private int row;

        public token(String name, int row)
        {
            this.name = name;
            this.row = row;
        }

        public String getName()
        {
            return this.name;
        }

        public int getRow()
        {
            return this.row;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public void setRow(int row)
        {
            this.row = row;
        }
    }
}
