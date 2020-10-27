using proyecto2Lenguajes.ObjectsCompile;
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
        private List<token> pileTokens;

        public PileTokens() {
            tokens = new List<string>();
            pileTokens = new List<token>();
    }

        public List<token> getPileTokens()
        {
            return pileTokens;
        }

        public void setNewToken(String token, int row)
        {
            token = getTypeToken(token);
            if (verifyComments(token) == false)
            {
                token tok = new token(token,row);
                pileTokens.Add(tok);
            }            
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

        public Boolean verifyComments(String token)
        {
            Boolean isComment = false;
            if (token.Length >= 2)
            {
                String first2Char = token.Substring(0, 2);
                if (first2Char == "//" || first2Char == "/*")
                {
                    isComment = true;
                }
            }
            return isComment;
        }

    }
}
