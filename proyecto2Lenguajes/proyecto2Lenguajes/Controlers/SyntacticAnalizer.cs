using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyecto2Lenguajes.Controlers
{
    class SyntacticAnalizer
    {
        List<string> pile;
        String tokenRequired;
        String initMethod;
        String tokenG;
        String linesReview;
        private int numberErrors;
        private DataGridView dataGridView;
        private List<string> reserverdWords;
        private List<string> reserverdBoolean;
        private List<string> arithmeticOperators;
        private List<string> relationalOperators;
        private List<string> logicOperators;
        private List<string> primitiveReservedWords;
        private List<string> variablesInit;

        public SyntacticAnalizer(DataGridView datagridview) {
            initCompile();
            this.pile = new List<string>();
            this.dataGridView = datagridview;
            numberErrors = 0;
        }

        private void initCompile()
        {
            //this.state = "0";
            reserverdWords = new List<string>() { "SI", "SINO", "SINO_SI", "MIENTRAS", "HACER", "DESDE", "HASTA", "INCREMENTO" };
            primitiveReservedWords = new List<string>() { "entero", "decimal", "cadena", "booleano", "caracter" };
            arithmeticOperators = new List<string>() { "+", "-", "*", "/", "++", "--" };
            relationalOperators = new List<string>() { ">=", "<=", "==", ">", "<", "!=" };
            reserverdBoolean = new List<string>() { "verdadero", "falso" };
            logicOperators = new List<string>() { "||", "&&", "!" };
            variablesInit = new List<string>();
            this.numberErrors = 0;
        }

        public void setToken(String token) {            
            this.tokenG = token;
            setRightToken();
            Console.WriteLine(token);
            if (token == "principal")
            {
                initMethod = token;
                this.tokenRequired = "(";
            }
            else if (token == "(")
            {
                if (pile.Count == 0)
                {
                    initMethod += token;
                }
            }
            else if (token == ")")
            {
                if (pile.Count == 0)
                {
                    initMethod += token;
                }
            }
            else if (token == "{")
            {
                if (pile.Count == 0)
                {
                    pile.Add("O");
                    initMethod += token;
                }
            }
            else {
                if (token == "}")
                {
                    if (initMethod == "principal(){" && pile[pile.Count - 1] == "O")
                    {
                        initMethod = "";
                    }
                    else
                    {
                        addError("El metodo esta mal construido");
                    }
                }
                else if (pile.Count > 0)
                {
                    reviewToken();
                }
                else {
                    addError("No se ha construido un metodo");
                }
            }
        }

        public void reviewToken()
        {
            int sizeP = pile.Count - 1;

            if (pile[sizeP] == "O")
            {
                if (this.primitiveReservedWords.Contains(this.tokenG))
                {
                    //addDiferentsToPile(";,L,=,I");
                    addDiferentsToPile("X,I");
                }
                else if (this.reserverdWords.Contains(this.tokenG))
                {
                    if (this.tokenG == "SI")
                    {
                        pile.Add("Q");
                    }
                    else if (this.tokenG == "MIENTRAS")
                    {
                        pile.Add("Y");
                    }
                }
            }
            else if (pile[sizeP] == "I" && tokenG == "id")
            {
                pile.RemoveAt(sizeP);
            }
            else if (pile[sizeP] == "X")//TODO
            {
                if (tokenG == ";")
                {
                    pile.RemoveAt(sizeP);
                }
                else if (tokenG == "=")
                {
                    pile.RemoveAt(sizeP);
                    addDiferentsToPile(";,L");
                }
            }
            else if (pile[sizeP] == "L" && reviewLocalVar() == true)
            {
                pile.RemoveAt(sizeP);
            }
            else if (pile[sizeP] == this.tokenG)
            {
                pile.RemoveAt(sizeP);
            }
        }

        private Boolean reviewLocalVar() {
            Boolean isCorrect = false;
            int result;
            double result2;
            if (int.TryParse(this.tokenG,out result))
            {
                isCorrect = true;
            } else if (double.TryParse(this.tokenG, out result2))
            {
                isCorrect = true;
            }
            else if (this.tokenG.Length == 1)
            {
                isCorrect = true;
            }
            else if (this.tokenG.Substring(tokenG.Length - 1, 1) == this.tokenG.Substring(0, 1))
            {
                Char last = Convert.ToChar(this.tokenG.Substring(0, 1));                
                if (last == '"')
                {
                    isCorrect = true;
                }                
            }
            else if (this.tokenG== "verdadero" || this.tokenG == "falso")
            {
                isCorrect = true;
            }
            return isCorrect;
        }

        private void setRightToken() {
            if (tokenG.Substring(0, 1) == "_")
            {
                tokenG = "id";                
            } else if (tokenG.Substring(0, 1) == "=")
            {
                tokenG = "=";
            }
        }
        public void addDiferentsToPile(String values)
        {
            String[] valuesToInsert = values.Split(',');
            foreach (String item in valuesToInsert)
            {
                pile.Add(item);
            }
        }

        public void addError(String typeError)
        {
            this.numberErrors++;
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(this.dataGridView);
            row.Cells[0].Value = this.numberErrors;
            row.Cells[1].Value = typeError;
            this.dataGridView.Rows.Add(row);
        }
    }
}

