namespace NHibernatePlugin.TypeNames.Scanners
{
    public interface IToken
    {
        Scanner.TokenType TokenType { get; }
        string Text { get; }
    }
}