using System;
using NHibernatePlugin.TypeNames.Scanners;

namespace NHibernatePlugin.TypeNames.Parser
{
    public class Parser
    {
        private Scanner scanner;
        private IToken token;

        public IParsedType Parse(string input) {
            scanner = new Scanner(input);
            IParsedType parsedType = ParseType();
            if (scanner.EOF) {
                return parsedType;
            }
            throw new Exception();
        }

        private IParsedType ParseType() {
            ParsedType parsedType = null;

            token = scanner.NextToken();
            if (token.TokenType == Scanner.TokenType.Name) {
                string typeName = token.Text;
                token = scanner.NextToken();
                if (token.TokenType != Scanner.TokenType.Accent) {
                    return new ParsedType(typeName);
                }
                token = scanner.NextToken();
                if (token.TokenType == Scanner.TokenType.Number) {
                    parsedType = new ParsedType(typeName + "`" + token.Text);
                    token = scanner.NextToken();
                    if (token.TokenType == Scanner.TokenType.LeftBracket) {
                        parsedType.AddTypeParameter(ParseType());
                        while (token.TokenType == Scanner.TokenType.Comma) {
                            parsedType.AddTypeParameter(ParseType());
                        }
                        if (token.TokenType == Scanner.TokenType.RightBracket) {
                            token = scanner.NextToken();
                        }
                    }
                }
            }
            return parsedType;
        }
    }
}