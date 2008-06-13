using JetBrains.ReSharper.Psi.Xml.Tree;

namespace NHibernatePlugin.LanguageService.Parser
{
    public abstract class MappingFileCompositeNodeType : XmlElementTypes.XmlCompositeNodeType
    {
        protected MappingFileCompositeNodeType(string s)
            : base(s, MappingFileLanguageService.MAPPING_FILE) {
        }
    }
}