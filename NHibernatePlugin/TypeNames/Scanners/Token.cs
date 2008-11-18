namespace NHibernatePlugin.TypeNames.Scanners
{
    public class Token : IToken
    {
        private readonly Scanner.TokenType tokenType;
        private readonly string text;

        public Token(Scanner.TokenType tokenType, string text) {
            this.tokenType = tokenType;
            this.text = text;
        }

        public Scanner.TokenType TokenType {
            get { return tokenType; }
        }

        public string Text {
            get { return text; }
        }
    }
}