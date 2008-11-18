using System.Collections.Generic;

namespace NHibernatePlugin.TypeNames.Parser
{
    public interface IParsedType
    {
        string TypeName { get; }
        IList<IParsedType> TypeParameters { get; }
    }
}