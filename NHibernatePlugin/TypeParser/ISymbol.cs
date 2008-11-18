namespace NHibernatePlugin.TypeParser
{
    public interface ISymbol
    {
        Scanner.SymbolType SymbolType { get; }
        string Text { get; }
    }
}