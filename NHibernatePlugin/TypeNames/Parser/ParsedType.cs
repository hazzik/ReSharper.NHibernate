using System.Collections.Generic;

namespace NHibernatePlugin.TypeNames.Parser
{
    public class ParsedType : IParsedType
    {
        private readonly string typeName;
        private readonly IList<IParsedType> typeParameters = new List<IParsedType>();

        public ParsedType(string typeName) {
            this.typeName = typeName;
        }

        public string TypeName {
            get { return typeName; }
        }

        public IList<IParsedType> TypeParameters {
            get { return typeParameters; }
        }

        public void AddTypeParameter(IParsedType parsedType) {
            typeParameters.Add(parsedType);
        }
    }
}