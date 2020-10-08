using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyecto2Lenguajes.Controlers
{
    class ControlCompile
    {
        private RichTextBox richTextBox;
        private DataGridView dataGridView;
        private int numberErrors;
        private List<string> reserverdWords;
        private List<string> reserverdBoolean;
        private List<string> arithmeticOperators;
        private List<string> relationalOperators;
        private List<string> logicOperators;


        public ControlCompile(RichTextBox richTextbox, DataGridView datagridview, ref int numberErrors)
        {
            this.richTextBox = richTextbox;
            this.dataGridView = datagridview;
            this.numberErrors = numberErrors;
            initCompile();
            reviewChars();
        }

        private void initCompile()
        {
            //this.state = "0";
            reserverdWords = new List<string>() { "SI", "SINO", "SINO_SI", "MIENTRAS", "HACER", "DESDE", "HASTA", "INCREMENTO" };
            arithmeticOperators = new List<string>() { "+", "-", "*", "/", "++", "--" };
            relationalOperators = new List<string>() { ">=", "<=", "==", ">", "<", "!=" };
            reserverdBoolean = new List<string>() { "verdadero", "falso" };
            logicOperators = new List<string>() { "||", "&&", "!" };
            this.dataGridView.Rows.Clear();
            this.numberErrors = 0;
        }
        public void reviewChars()
        {
            this.dataGridView.Rows.Clear();
            this.numberErrors = 0;
            int state = 0;
            for (int i = 0; i < this.richTextBox.Text.Length; i++)
            {
                Char caracter = Convert.ToChar(this.richTextBox.Text.Substring(i, 1));
                switch (state)
                {
                    case 0:
                        if (Char.IsLetter(caracter))
                        {
                            goto case 1;
                        }
                        else if (Char.IsDigit(caracter))
                        {
                            goto case 2;
                        }
                        else if (caracter == '|')
                        {
                            goto case 4;
                        }
                        else
                        {
                            goto case 3;
                        }
                    case 1:
                        reviewIdentifier(ref i, caracter, this.richTextBox.Text);
                        break;
                    case 2:
                        reviewDigits(ref i);
                        break;
                    case 3:
                        reviewSymbols(ref i, caracter);
                        break;
                    case 4:
                        reviewOr(ref i);
                        break;
                }
                isLineBreak(caracter);
            }
        }

        private void reviewIdentifier(ref int i, Char character, String textRTB)
        {
            String word = "";
            for (int x = i; x < textRTB.Length; x++)
            {
                Char caracter = Convert.ToChar(this.richTextBox.Text.Substring(x, 1));
                if ((caracter == ' ') || isLineBreak(caracter) || (Char.IsLetter(caracter) == false && Char.IsDigit(caracter) == false))
                {
                    break;
                }
                else
                {
                    i++;
                    word += caracter;
                }
            }
            if (reserverdWords.Contains(word))
            {
                //MessageBox.Show(word + " Es una palabra reservada");
                paintReservedWords(word, i, Color.Green);
            }
            else if (reserverdBoolean.Contains(word))
            {
                paintReservedWords(word, i, Color.Orange);
            }
            else if (word.Length == 1)
            {
                paintReservedWords(word, i, Color.Brown);
            }
            else
            {
                paintReservedWords(word, i, Color.Black);
                //MessageBox.Show(word + " Es un identificador");
            }
            i--;
        }

        private void reviewSymbols(ref int i, Char character)
        {
            int countSymbols = 0;
            String word = "";
            if (character == '/')
            {
                word += character;
                if ((i + 2) <= this.richTextBox.Text.Length)
                {
                    character = Convert.ToChar(this.richTextBox.Text.Substring((i + 1), 1));
                    if (character == '/')
                    {
                        reviewComment1(ref i);
                    }
                    else if (character == '*')
                    {
                        reviewComment2(ref i);
                    }
                    else if (character == ' ' || isLineBreak(character))
                    {
                        paintReservedWords(word, i + 1, Color.Blue);
                    }
                    else
                    {
                        // error quizas quisiste poner un comentario
                        //MessageBox.Show("Falta / para comentario");
                        addError("Falta / para hacer un comentario");
                    }
                }
                else
                {
                    paintReservedWords(word, i + 1, Color.Blue);
                }
            }
            else if (character == '"')
            {
                reviewTextString(ref i);
            }
            else if (character == ';')
            {
                word += character;
                paintReservedWords(word, i + 1, Color.DeepPink);
            }
            else if (character == '(' || character == ')')
            {
                word += character;
                paintReservedWords(word, i + 1, Color.Blue);
            }
            else if (character == '=')
            {
                word += character;
                if (((i + 2) <= this.richTextBox.Text.Length) || (character == ' ') || (isLineBreak(character)))
                {
                    character = Convert.ToChar(this.richTextBox.Text.Substring((i + 1), 1));
                    word += character;
                    if (character == '=')
                    {
                        i++;
                        paintReservedWords(word, i + 1, Color.Blue);
                    }
                    else
                    {
                        paintReservedWords(word, i + 1, Color.Pink);
                    }
                }
                else
                {
                    paintReservedWords(word, i + 1, Color.Pink);
                }
            }
            else if (character == '-')
            {
                word += character;
                if (((i + 2) <= this.richTextBox.Text.Length) || (character == ' ') || (isLineBreak(character)))
                {
                    character = Convert.ToChar(this.richTextBox.Text.Substring((i + 1), 1));
                    word += character;
                    if (Char.IsDigit(character))
                    {
                        reviewDigits(ref i);
                        // MessageBox.Show("Es un numero negativo");
                    }
                    else if (character == '-')
                    {
                        i++;
                        paintReservedWords(word, i + 1, Color.Blue);
                    }
                    else
                    {
                        paintReservedWords(word, i + 1, Color.Blue);
                    }
                }
                else
                {
                    paintReservedWords(word, i + 1, Color.Blue);
                }
            }
            else if (this.relationalOperators.Contains(character.ToString()))
            {
                for (int x = i; x < this.richTextBox.Text.Length; x++)
                {
                    character = Convert.ToChar(this.richTextBox.Text.Substring(x, 1));
                    if (character == ' ' || isLineBreak(character) || character == ';')
                    {
                        break;
                    }
                    else
                    {
                        i++;
                        word += character;
                        countSymbols++;
                    }
                }
                if (countSymbols <= 2)
                {
                    if (this.relationalOperators.Contains(word))
                    {
                        paintReservedWords(word, i, Color.Blue);
                    }
                }
                else
                {
                    //Error El operador realacional no existe
                    //MessageBox.Show("El operador realacional no es correcto");
                    addError("Error el operador relacional no existe");
                }
                i--;
            }
            else if (character == '&')
            {
                if ((i + 2) <= this.richTextBox.Text.Length)
                {
                    character = Convert.ToChar(this.richTextBox.Text.Substring(i + 1, 1));
                    if (character == '&')
                    {
                        i++;
                        paintReservedWords("&&", i + 1, Color.Blue);
                    }
                    else
                    {
                        //Error el operador logico esta mal escrito
                        addError("Error el operador logico esta mal escrito");
                    }
                }
                else
                {
                    //Error el operador logico esta mal escrito
                    addError("Error el operador logico esta mal escrito");
                }
            }
            else if (character == '!')
            {
                paintReservedWords("!", i + 1, Color.Blue);
            }
            else if (this.arithmeticOperators.Contains(character.ToString()))
            {
                for (int x = i; x < this.richTextBox.Text.Length; x++)
                {
                    character = Convert.ToChar(this.richTextBox.Text.Substring(x, 1));
                    if (character == ' ' || isLineBreak(character) || character == ';')
                    {
                        break;
                    }
                    else
                    {

                        i++;
                        word += character;
                        countSymbols++;
                    }
                }
                if (countSymbols <= 2)
                {
                    if (this.arithmeticOperators.Contains(word))
                    {
                        paintReservedWords(word, i, Color.Blue);
                    }
                }
                else
                {
                    //Error El operador aritmetico no existe
                    //MessageBox.Show("El operador aritmetico no es correcto");
                    addError("Error el operador aritmetico no es correcto");
                }
                i--;
            }
        }
        private void reviewTextString(ref int i)
        {
            String word = "";
            Boolean isClosed = false;
            int initString = i;
            for (int x = i; x < this.richTextBox.Text.Length; x++)
            {
                Char character = Convert.ToChar(this.richTextBox.Text.Substring(x, 1));
                if (isLineBreak(character) || (character == '"' && initString != x))
                {
                    if (character == '"')
                    {
                        i++;
                        word += character;
                        isClosed = true;
                    }
                    break;
                }
                else
                {
                    i++;
                    word += character;
                }
            }
            if (isClosed == false)
            {
                // MessageBox.Show("Error la cadena de texto no esta cerrada");
                addError("La cadena de texto no esta cerrada");
            }
            paintReservedWords(word, i, Color.Gray);
            i--;
        }

        private void reviewComment1(ref int i)
        {
            String word = "";
            for (int x = i; x < this.richTextBox.Text.Length; x++)
            {
                Char character = Convert.ToChar(this.richTextBox.Text.Substring(x, 1));
                if (isLineBreak(character))
                {
                    break;
                }
                else
                {
                    i++;
                    word += character;
                }
            }
            paintReservedWords(word, i, Color.Red);
        }
        private void reviewComment2(ref int i)
        {
            Boolean commentClosed = false;
            String word = "";
            for (int x = i; x < this.richTextBox.Text.Length; x++)
            {
                Char character = Convert.ToChar(this.richTextBox.Text.Substring(x, 1));
                if (character == '*')
                {
                    if ((x + 2) <= this.richTextBox.Text.Length)
                    {
                        Char character2 = Convert.ToChar(this.richTextBox.Text.Substring((x + 1), 1));
                        if (character2 == '/')
                        {
                            i = i + 2;
                            word += character;
                            word += character2;
                            commentClosed = true;
                            break;
                        }
                        else
                        {
                            i++;
                            word += character;
                        }
                    }
                }
                else
                {
                    i++;
                    word += character;
                }
            }
            paintReservedWords(word, i, Color.Red);
            if (commentClosed == false)
            {
                // error falta cierre de comentario
                //MessageBox.Show("Error falta cierre de comentario");
                addError("Error falta cierre de comentario");
            }
        }

        private void reviewDigits(ref int i)
        {
            String word = "";
            int quantityPoints = 0;
            int initCount = i;
            for (int x = i; x < this.richTextBox.Text.Length; x++)
            {
                Char character = Convert.ToChar(this.richTextBox.Text.Substring(x, 1));
                Boolean charIsDigit = Char.IsDigit(character);
                if (charIsDigit || (character == '.' && quantityPoints <= 1) || (x == initCount && character == '-'))
                {
                    if (character == '.')
                    {
                        quantityPoints++;
                    }
                    i++;
                    word += character;
                }
                else
                {
                    break;
                }
            }
            if (quantityPoints == 0)
            {
                paintReservedWords(word, i, Color.Purple);
            }
            else if (quantityPoints == 1)
            {
                try
                {
                    double number = Double.Parse(word);
                    String[] numberSeparated = word.Split('.');
                    if (numberSeparated[1].Equals(""))
                    {
                        addError("El numero decimal esta mal escrito");
                    }
                    else
                    {
                        paintReservedWords(word, i, Color.LightBlue);
                    }
                }
                catch (Exception e)
                {
                    addError("El numero decimal esta mal escrito");
                }
            }
            i--;
        }

        public void reviewOr(ref int i)
        {
            if ((i + 2) <= this.richTextBox.Text.Length)
            {
                Char character = Convert.ToChar(this.richTextBox.Text.Substring(i + 1, 1));
                if (character == '|')
                {
                    i++;
                    paintReservedWords("||", i + 1, Color.Blue);
                }
                else
                {
                    //Error el operador logico esta mal escrito
                    addError("Error el operador logico esta mal escrito");
                }
            }
        }

        public void paintReservedWords(String word, int start, Color color)
        {
            this.richTextBox.Select(start - word.Length, word.Length);
            this.richTextBox.SelectionColor = color;
            this.richTextBox.SelectionStart = this.richTextBox.Text.Length;
            this.richTextBox.SelectionColor = Color.Black;
            this.richTextBox.SelectionStart = this.richTextBox.Text.Length;
        }

        public Boolean isLineBreak(Char caracter)
        {
            if (caracter.Equals('\n'))
            {
                return true;
            }
            return false;
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
