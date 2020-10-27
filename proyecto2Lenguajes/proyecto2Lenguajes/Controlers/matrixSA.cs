using proyecto2Lenguajes.ObjectsCompile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto2Lenguajes.Controlers
{
    class matrixSA
    {
        private List<RowSA> tableSA = new List<RowSA>();

        public matrixSA() {
            initTable();
        }
        //values = numD, cad, char
        private void initTable() {
            RowSA row;
            tableSA.Add(row = new RowSA("E", "principal", "principal.(.).{.O.}"));
            tableSA.Add(row = new RowSA("V", "entero", "entero.id.I.X.;.O"));
            tableSA.Add(row = new RowSA("V", "decimal", "decimal.id.I.X.;.O"));
            tableSA.Add(row = new RowSA("V", "cadena", "cadena.id.I.X.;.O"));
            tableSA.Add(row = new RowSA("V", "booleano", "booleano.id.I.X.;.O"));
            tableSA.Add(row = new RowSA("V", "caracter", "caracter.id.I.X.;.O"));
            tableSA.Add(row = new RowSA("R", "imprimir", "imprimir.(.C.).;.O"));
            tableSA.Add(row = new RowSA("R", "leer", "leer.(.C.).;.O"));
            tableSA.Add(row = new RowSA("O", "entero", "V"));
            tableSA.Add(row = new RowSA("O", "decimal", "V"));
            tableSA.Add(row = new RowSA("O", "cadena", "V"));
            tableSA.Add(row = new RowSA("O", "booleano", "V"));
            tableSA.Add(row = new RowSA("O", "caracter", "V"));
            tableSA.Add(row = new RowSA("O", "imprimir", "R"));
            tableSA.Add(row = new RowSA("O", "leer", "R"));
            tableSA.Add(row = new RowSA("O", "id", "id.=.L.;"));
            tableSA.Add(row = new RowSA("O", "}", "reduce"));
            tableSA.Add(row = new RowSA("O", "SI", "Q"));
            tableSA.Add(row = new RowSA("O", "MIENTRAS", "Y"));
            tableSA.Add(row = new RowSA("O", "HACER", "J"));
            tableSA.Add(row = new RowSA("O", "DESDE", "U"));
            tableSA.Add(row = new RowSA("L", "numE", "numE"));
            tableSA.Add(row = new RowSA("L", "bool", "bool"));
            tableSA.Add(row = new RowSA("L", "numD", "numD"));
            tableSA.Add(row = new RowSA("L", "cad", "cad"));
            tableSA.Add(row = new RowSA("L", "char", "char"));
            tableSA.Add(row = new RowSA("L", "id", "id"));
            tableSA.Add(row = new RowSA("C", "numE", "L.C'"));
            tableSA.Add(row = new RowSA("C", "bool", "L.C'"));
            tableSA.Add(row = new RowSA("C", "numD", "L.C'"));
            tableSA.Add(row = new RowSA("C", "cad", "L.C'"));
            tableSA.Add(row = new RowSA("C", "char", "L.C'"));
            tableSA.Add(row = new RowSA("C", "id", "L.C'"));
            tableSA.Add(row = new RowSA("C'", "+", "+.C"));
            tableSA.Add(row = new RowSA("C'", ")", "reduce"));
            tableSA.Add(row = new RowSA("X", "=", "=.L"));
            tableSA.Add(row = new RowSA("X", ";", "reduce"));
            tableSA.Add(row = new RowSA("I", "=", "reduce"));
            tableSA.Add(row = new RowSA("I", ";", "reduce"));
            tableSA.Add(row = new RowSA("I", ",", ",.id.I"));
            tableSA.Add(row = new RowSA("B", "id", "id.W.L.B'"));
            tableSA.Add(row = new RowSA("B", "numE", "numE.W.L.B'"));
            tableSA.Add(row = new RowSA("B", "bool", "bool"));
            tableSA.Add(row = new RowSA("B", "numD", "numD.W.L.B'"));
            tableSA.Add(row = new RowSA("B", "cad", "cad.W.L.B'"));
            tableSA.Add(row = new RowSA("B", "char", "char.W.L.B'"));
            tableSA.Add(row = new RowSA("B'", ")", "reduce"));
            tableSA.Add(row = new RowSA("B'", "||", "||.B"));
            tableSA.Add(row = new RowSA("B'", "&&", "&&.B"));
            tableSA.Add(row = new RowSA("Q", "SI", "SI.(.B.).{.O.}.Q'.O"));
            tableSA.Add(row = new RowSA("Q'", "SINO_SI", "SINO_SI.(.B.).{.O.}.Q'"));
            tableSA.Add(row = new RowSA("Q'", "SINO", "SINO.{.O.}"));
            //Q' es especial si viene cualquier otra cosa se elimina
            tableSA.Add(row = new RowSA("Y", "MIENTRAS", "MIENTRAS.(.B.).{.O.}.O"));
            tableSA.Add(row = new RowSA("J", "HACER", "HACER.{.O.}.MIENTRAS.(.B.).O"));
            tableSA.Add(row = new RowSA("U", "DESDE", "DESDE.id.=.numE.HASTA.id.W.numE.INCREMENTO.numE.{.O.}.O"));
            tableSA.Add(row = new RowSA("W", ">", ">"));
            tableSA.Add(row = new RowSA("W", "<", "<"));
            tableSA.Add(row = new RowSA("W", ">=", ">="));
            tableSA.Add(row = new RowSA("W", "<=", "<="));
            tableSA.Add(row = new RowSA("W", "==", "=="));
            tableSA.Add(row = new RowSA("W", "!=", "!="));
            tableSA.Add(row = new RowSA("T", "||", "||"));
            tableSA.Add(row = new RowSA("T", "&&", "&&"));
        }

        public List<RowSA> getTable() {
            return this.tableSA;
        }


    }
}
