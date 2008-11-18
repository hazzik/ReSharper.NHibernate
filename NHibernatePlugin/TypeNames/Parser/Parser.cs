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
            token = scanner.NextToken();
            IParsedType parsedType = null;
            if (token.TokenType == Scanner.TokenType.Name) {
                parsedType = ParseType(token.Text);
            }
            if (scanner.EOF) {
                return parsedType;
            }
            throw new Exception();
        }

        private IParsedType ParseType(string typeName) {
            ParsedType parsedType = new ParsedType(typeName);
            token = scanner.NextToken();
            if(token.TokenType == Scanner.TokenType.Accent) {
                token = scanner.NextToken();
                if (token.TokenType == Scanner.TokenType.LeftBracket) {
                    token = scanner.NextToken();
                    if (token.TokenType == Scanner.TokenType.Name) {
                        parsedType.AddTypeParameter(ParseType(token.Text));
                        while (token.TokenType == Scanner.TokenType.Comma) {
                            token = scanner.NextToken();
                            if (token.TokenType == Scanner.TokenType.Name) {
                                parsedType.AddTypeParameter(ParseType(token.Text));
                            }
                        }
                    }
                }
            }
            return parsedType;
        }
    }
}