namespace NHibernatePlugin.TypeNames.Parser
{
    public interface IParserError
    {
        string Message { get; }
        int Index { get; }
    }
}