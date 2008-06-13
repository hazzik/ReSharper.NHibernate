using JetBrains.Application;
using JetBrains.ReSharper.Psi.Xml.Parsing;

namespace NHibernatePlugin.LanguageService.Parser
{
    public class MappingFileTreeBuilder : XmlTreeBuilder
    {
        public MappingFileTreeBuilder(XmlElementFactory elementFactory, CheckForInterrupt checkForInterrupt)
            : base(elementFactory, checkForInterrupt, MappingFileLanguageService.MAPPING_FILE) {
        }
    }
}