using JetBrains.ReSharper.Psi.Xml.Tree;

namespace NHibernatePlugin.LanguageService.Parser
{
    public abstract class MappingFileCompositeNodeType : XmlCompositeNodeType
    {
        protected MappingFileCompositeNodeType(string s, MappingFileElementTypes elementTypes)
            : base(s, elementTypes) {
        }
    }
}