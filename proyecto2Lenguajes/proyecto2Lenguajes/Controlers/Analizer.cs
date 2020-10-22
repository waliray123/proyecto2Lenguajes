using proyecto2Lenguajes.ObjectsCompile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyecto2Lenguajes.Controlers
{
    class Analizer
    {
        private List<string> pile;
        private List<string> tokens;
        private List<RowSA> tableSA = new List<RowSA>();
        private int numberErrors;
        private DataGridView dataGridView;        
        private List<String> namesNoTerminal = new List<string>() { "E", "V", "R", "O", "L","C","C'","X","I","B","B'","Q","Q'","Y","J","U","W","T" };
        private int numberOfToken;

        public Analizer(DataGridView datagridview, PileTokens pileTokens)
        {
            this.pile = new List<string>();
            this.dataGridView = datagridview;
            tokens = pileTokens.getPileTokens();
            getTableToSet();
            if (this.tokens.Count > 0)
            {
                analizeCode();
            }            
        }
        
        private void getTableToSet()
        {
            matrixSA matrix = new matrixSA();
            this.tableSA = matrix.getTable();
        }

        private void analizeCode()
        {
            pile.Add("E");
            this.numberOfToken = 0;
            while (this.pile.Count > 0)
            {
                int pileSize = this.pile.Count();
                String lastInPile = this.pile[pileSize-1];
                String tokenRequired = this.tokens[numberOfToken];
                if (namesNoTerminal.Contains(lastInPile))
                {
                    Boolean isError = true;
                    foreach (RowSA row in this.tableSA)
                    {
                        String nameNoTerminal = row.getRow();
                        String column = row.getCol();
                        if (column == tokenRequired && nameNoTerminal == lastInPile)
                        {
                            //añadir a la pila el valor
                            pile.RemoveAt(pileSize-1);
                            String values = row.getVal();
                            addDiferentsToPile(values);
                            isError = false;
                            break;
                        }
                    }
                    if (isError == true)
                    {
                        pile.RemoveAt(pileSize-1);
                        addError("Error sintactico");
                    }
                }
                else
                {
                    //el ultimo en la pila no es un no terminal
                    if (lastInPile == "reduce")
                    {
                        pile.RemoveAt(pileSize-1);
                    }
                    else
                    {
                        if (lastInPile == tokenRequired)
                        {
                            pile.RemoveAt(pileSize-1);
                            this.numberOfToken++;
                        }
                        else
                        {
                            pile.RemoveAt(pileSize-1);
                            addError("Se esperaba: " + lastInPile);
                        }                        
                    }
                }
            }
        }

        public void addDiferentsToPile(String values)
        {
            String[] valuesToInsert = values.Split('.');
            //foreach (String item in valuesToInsert)
            //{
            //    pile.Add(item);
            //}
            for (int i = (valuesToInsert.Length-1); i >= 0; i--)
            {
                String item = valuesToInsert[i];
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
