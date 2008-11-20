namespace NHibernatePlugin.TypeNames.Parser
{
    public class ParserError : IParserError
    {
        private readonly string message;
        private readonly int index;

        public static IParserError None = new ParserError("", 0);

        public static IParserError Error(string message, int index) {
            return new ParserError(message, index);
        }

        protected ParserError(string message, int index) {
            this.message = message;
            this.index = index;
        }

        public string Message {
            get { return message; }
        }

        public int Index {
            get { return index; }
        }
    }
}