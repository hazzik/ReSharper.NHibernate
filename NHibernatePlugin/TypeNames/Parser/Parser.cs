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

        public IParsedType Parse(string input, out IParserError error) {
            scanner = new Scanner(input);
            return ParseTypeAndAssemblyName(out error);
        }

        private IParsedType ParseTypeAndAssemblyName(out IParserError error) {
            IParsedType parsedType = ParseType(out error);
            if (error != ParserError.None) {
                return null;
            }
            ParseAssemblyName(parsedType, out error);
            if (!scanner.EOF || token.TokenType != Scanner.TokenType.EOF) {
                error = ParserError.Error(string.Format(UnexpectedToken, token.Text), scanner.CurrentIndex - token.Text.Length);
                return null;
            }
            return parsedType;
        }

        private void ParseAssemblyName(IParsedType parsedType, out IParserError error) {
            if (token.TokenType != Scanner.TokenType.Comma) {
                error = ParserError.None;
                return;
            }
            token = scanner.NextToken();
            if (token.TokenType != Scanner.TokenType.Name) {
                error = ParserError.Error(NameExpected, scanner.CurrentIndex);
                return;
            }
            parsedType.AssemblyName = token.Text;
            token = scanner.NextToken();
            error = ParserError.None;
        }

        private IParsedType ParseType(out IParserError error) {
            token = scanner.NextToken();
            if (token.TokenType != Scanner.TokenType.Name) {
                error = ParserError.Error(NameExpected, scanner.CurrentIndex);
                return null;
            }
            string typeName = token.Text;
            ParsedType parsedType;

            token = scanner.NextToken();
            if (token.TokenType != Scanner.TokenType.Accent) {
                parsedType = new ParsedType(typeName);
            }
            else {
                token = scanner.NextToken();
                if (token.TokenType != Scanner.TokenType.Number) {
                    error = ParserError.Error(NumberExpected, scanner.CurrentIndex);
                    return null;
                }
                parsedType = new ParsedType(typeName + "`" + token.Text);

                token = scanner.NextToken();
                if (token.TokenType != Scanner.TokenType.LeftBracket) {
                    error = ParserError.Error(LeftBracketExpected, scanner.CurrentIndex);
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
                    error = ParserError.Error(RightBracketExpected, scanner.CurrentIndex);
                    return null;
                }
                token = scanner.NextToken();
            }

            error = ParserError.None;
            return parsedType;
        }
    }
}