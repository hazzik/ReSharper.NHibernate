using NHibernatePlugin.TypeNames.Scanners;

namespace NHibernatePlugin.TypeNames.Parser
{
    public class Parser
    {
        private const string NameExpected = "Name expected";
        private const string RightBracketExpected = "']' expected";
        private const string LeftBracketExpected = "'[' expected";
        private const string NumberExpected = "Number expected";
        private const string UnexpectedToken = "Unexpected token '{0}'";

        private Scanner scanner;
        private IToken token;
        private IParserError error;

        public IParsedType Parse(string input, out IParserError anError) {
            scanner = new Scanner(input);
            error = ParserError.None;
            var parsedType = ParseTypeAndAssemblyName();
            anError = error;
            return parsedType;
        }

        private IParsedType ParseTypeAndAssemblyName() {
            IParsedType parsedType = ParseType();
            if (error != ParserError.None) {
                return null;
            }
            
            ParseAssemblyName(parsedType);
            if (error != ParserError.None) {
                return null;
            }

            if (!scanner.EOF || token.TokenType != Scanner.TokenType.EOF) {
                error = ParserError.Error(string.Format(UnexpectedToken, token.Text), scanner.CurrentIndex - token.Text.Length);
                return null;
            }
            return parsedType;
        }

        private IParsedType ParseType() {
            token = scanner.NextToken();
            if (token.TokenType != Scanner.TokenType.Name) {
                error = ParserError.Error(NameExpected, scanner.CurrentIndex);
                return null;
            }
            string typeName = token.Text;

            token = scanner.NextToken();
            if (token.TokenType != Scanner.TokenType.Accent) {
                return ParseSimpleOrArrayType(typeName);
            }
            return ParseGenericType(typeName);
        }

        private IParsedType ParseSimpleOrArrayType(string typeName) {
            if (token.TokenType == Scanner.TokenType.LeftBracket) {
                token = scanner.NextToken();
                if (token.TokenType != Scanner.TokenType.RightBracket) {
                    error = ParserError.Error(RightBracketExpected, scanner.CurrentIndex);
                    return null;
                }
                typeName += "[]";
                token = scanner.NextToken();
            }
            return new ParsedType(typeName);
        }

        private IParsedType ParseGenericType(string typeName) {
            token = scanner.NextToken();
            if (token.TokenType != Scanner.TokenType.Number) {
                error = ParserError.Error(NumberExpected, scanner.CurrentIndex);
                return null;
            }
            var parsedType = new ParsedType(typeName + "`" + token.Text);

            token = scanner.NextToken();
            if (token.TokenType != Scanner.TokenType.LeftBracket) {
                error = ParserError.Error(LeftBracketExpected, scanner.CurrentIndex);
                return null;
            }
            ParseGenericTypeParameter(parsedType);
            if (error != ParserError.None) {
                return null;
            }

            if (token.TokenType != Scanner.TokenType.RightBracket) {
                error = ParserError.Error(RightBracketExpected, scanner.CurrentIndex);
                return null;
            }
            token = scanner.NextToken();
            return parsedType;
        }

        private void ParseGenericTypeParameter(ParsedType parsedType) {
            parsedType.AddTypeParameter(ParseType());
            if (error != ParserError.None) {
                return;
            }
            while (token.TokenType == Scanner.TokenType.Comma) {
                parsedType.AddTypeParameter(ParseType());
                if (error != ParserError.None) {
                    return;
                }
            }
        }

        private void ParseAssemblyName(IParsedType parsedType) {
            if (token.TokenType != Scanner.TokenType.Comma) {
                return;
            }
            token = scanner.NextToken();
            if (token.TokenType != Scanner.TokenType.Name) {
                error = ParserError.Error(NameExpected, scanner.CurrentIndex);
                return;
            }
            parsedType.AssemblyName = token.Text;
            token = scanner.NextToken();
        }
    }
}