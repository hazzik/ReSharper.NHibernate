using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Xml.Parsing;

namespace NHibernatePlugin.LanguageService.Parser
{
    public class MappingFileTreeBuilder : XmlTreeBuilder
    {
        public MappingFileTreeBuilder(IXmlElementFactory elementFactory, IXmlElementFactoryContext factoryContext)
            : base(elementFactory, factoryContext) {
        }
    }
}