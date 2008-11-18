namespace NHibernatePlugin.TypeParser
{
    public class Scanner
    {
        public enum SymbolType
        {
            Name,
            Accent,
            LeftBracket,
            RightBracket,
            EOF
        };

        private readonly string input;
        private int nextIndex;

        public Scanner(string input) {
            this.input = input;
        }

        public ISymbol NextSymbol() {
            if (IndexIsEOF()) {
                return new Symbol(SymbolType.EOF, "");
            }
            string symbolText = "";

            if (char.IsLetter(PeekChar())) {
                do {
                    symbolText += GetNextChar();
                } while (!IndexIsEOF() && char.IsLetter(PeekChar()));
                return new Symbol(SymbolType.Name, symbolText);
            }
            if ('´' == PeekChar()) {
                symbolText += GetNextChar();
                return new Symbol(SymbolType.Accent, symbolText);
            }
            if ('[' == PeekChar()) {
                symbolText += GetNextChar();
                return new Symbol(SymbolType.LeftBracket, symbolText);
            }
            if (']' == PeekChar()) {
                symbolText += GetNextChar();
                return new Symbol(SymbolType.RightBracket, symbolText);
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