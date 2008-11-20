namespace NHibernatePlugin.TypeNames.Scanners
{
    public class Scanner
    {
        public enum TokenType
        {
            Name,
            Accent,
            LeftBracket,
            RightBracket,
            Comma,
            Number,
            Unknown,
            EOF
        };

        private readonly string input;
        private int nextIndex;

        public Scanner(string input) {
            this.input = input;
        }

        public bool EOF {
            get { return IndexIsEOF(); }
        }

        public int CurrentIndex {
            get { return nextIndex; }
        }

        public IToken NextToken() {
            if (IndexIsEOF()) {
                return new Token(TokenType.EOF, "");
            }
            string tokenText = "";

            if (PeekChar().IsNameStartCharacter()) {
                do {
                    tokenText += GetNextChar();
                } while (!IndexIsEOF() && PeekChar().IsNameCharacter());
                return new Token(TokenType.Name, tokenText);
            }
            if (char.IsDigit(PeekChar())) {
                do {
                    tokenText += GetNextChar();
                } while (!IndexIsEOF() && char.IsDigit(PeekChar()));
                return new Token(TokenType.Number, tokenText);
            }
            if ('`' == PeekChar()) {
                tokenText += GetNextChar();
                return new Token(TokenType.Accent, tokenText);
            }
            if ('[' == PeekChar()) {
                tokenText += GetNextChar();
                return new Token(TokenType.LeftBracket, tokenText);
            }
            if (']' == PeekChar()) {
                tokenText += GetNextChar();
                return new Token(TokenType.RightBracket, tokenText);
            }
            if (',' == PeekChar()) {
                tokenText += GetNextChar();
                return new Token(TokenType.Comma, tokenText);
            }

            return new Token(TokenType.Unknown, PeekChar().ToString());
        }

        private char PeekChar() {
            return input[nextIndex];
        }

        private bool IndexIsEOF() {
            return nextIndex >= input.Length;
        }

        private char GetNextChar() {
            return input[nextIndex++];
        }
    }
}