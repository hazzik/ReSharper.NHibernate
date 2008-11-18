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

        public IToken NextToken() {
            if (IndexIsEOF()) {
                return new Token(TokenType.EOF, "");
            }
            string tokenText = "";

            if (char.IsLetter(PeekChar())) {
                do {
                    tokenText += GetNextChar();
                } while (!IndexIsEOF() && char.IsLetter(PeekChar()));
                return new Token(TokenType.Name, tokenText);
            }
            if ('´' == PeekChar()) {
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

            throw new ScannerException(string.Format("Unknown character {0}", PeekChar()));
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