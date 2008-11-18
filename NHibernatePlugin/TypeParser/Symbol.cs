namespace NHibernatePlugin.TypeParser
{
    public class Symbol : ISymbol
    {
        private readonly Scanner.SymbolType symbolType;
        private readonly string text;

        public Symbol(Scanner.SymbolType symbolType, string text) {
            this.symbolType = symbolType;
            this.text = text;
        }

        public Scanner.SymbolType SymbolType {
            get { return symbolType; }
        }

        public string Text {
            get { return text; }
        }
    }
}