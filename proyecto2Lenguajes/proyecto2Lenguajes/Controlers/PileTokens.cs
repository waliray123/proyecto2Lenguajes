using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto2Lenguajes.Controlers
{
    class PileTokens
    {

        private List<string> tokens;

        public PileTokens() {
            tokens = new List<string>();
        }

        public List<string> getPileTokens()
        {
            return tokens;
        }

        public void setNewToken(String token)
        {
            token = getTypeToken(token);
            tokens.Add(token);
        }

        public String getTypeToken(String token) {
            String tokenR = token;
            int result;
            double result2;
            if (token == "= ")
            {
                tokenR = "=";
            }
            else if (token.Substring(0, 1) == "_")
            {
                tokenR = "id";
            }
            else if (int.TryParse(token, out result))
            {
                tokenR = "numE";
            }
            else if (double.TryParse(token, out result2))
            {
                tokenR = "numD";
            }
            else if (token.Length == 1 )
            {
                Char charT = Char.Parse(token);
                if (Char.IsLetter(charT))
                {
                    tokenR = "char";
                }                
            }
            else if (token.Substring(token.Length - 1, 1) == token.Substring(0, 1))
            {
                Char last = Convert.ToChar(token.Substring(0, 1));
                if (last == '"')
                {
                    tokenR = "cad";
                }
            }
            else if (token == "verdadero" || token == "falso")
            {
                tokenR = "bool";
            }
            return tokenR;
        }

       

    }
}
