using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto2Lenguajes.ObjectsCompile
{
    class RowSA
    {
        private String row;
        private String col;
        private String val;

        public RowSA(String row, String col, String val)
        {
            this.row = row;
            this.col = col;
            this.val = val;
        }
        public String getRow()
        {
            return this.row;
        }
        public String getCol()
        {
            return this.col;
        }
        public String getVal()
        {
            return this.val;
        }
        public void setRow(String rowG)
        {
            this.row = rowG;
        }
        public void setCol(String colG)
        {
            this.col = colG;
        }
        public void setVal(String valG)
        {
            this.val = valG;
        }
    }
}
