using NHibernatePlugin.TypeNames.Scanners;

namespace NHibernatePlugin.TypeNames.Parser
{
    public class Parser
    {
        private Scanner scanner;
        private IToken token;

        public IParsedType Parse(string input, out IParserError error) {
            scanner = new Scanner(input);
            IParsedType parsedType = ParseType(out error);
            if (error != ParserError.None) {
                return null;
            }
            if (!scanner.EOF || token.TokenType != Scanner.TokenType.EOF) {
                error = ParserError.Error(string.Format("Unexpected token '{0}'", token.Text), scanner.CurrentIndex - token.Text.Length);
                return null;
            }
            return parsedType;
        }

        private IParsedType ParseType(out IParserError error) {
            token = scanner.NextToken();
            if (token.TokenType != Scanner.TokenType.Name) {
                error = ParserError.Error("Name expected", scanner.CurrentIndex);
                return null;
            }
            string typeName = token.Text;

            token = scanner.NextToken();
            if (token.TokenType != Scanner.TokenType.Accent) {
                error = ParserError.None;
                return new ParsedType(typeName);
            }

            token = scanner.NextToken();
            if (token.TokenType != Scanner.TokenType.Number) {
                error = ParserError.Error("Number expected", scanner.CurrentIndex);
                return null;
            }
            ParsedType parsedType = new ParsedType(typeName + "`" + token.Text);

            token = scanner.NextToken();
            if (token.TokenType != Scanner.TokenType.LeftBracket) {
                error = ParserError.Error("'[' expected", scanner.CurrentIndex);
                return null;
            }
            parsedType.AddTypeParameter(ParseType(out error));
            if (error != ParserError.None) {
                return null;
            }
            while (token.TokenType == Scanner.TokenType.Comma) {
                parsedType.AddTypeParameter(ParseType(out error));
                if (error != ParserError.None) {
                    return null;
                }
            }

            if (token.TokenType != Scanner.TokenType.RightBracket) {
                error = ParserError.Error("']' expected", scanner.CurrentIndex);
                return null;
            }
            token = scanner.NextToken();

            error = ParserError.None;
            return parsedType;
        }
    }
}