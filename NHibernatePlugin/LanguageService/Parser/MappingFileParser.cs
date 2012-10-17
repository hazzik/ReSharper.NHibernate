using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Parsing;

namespace NHibernatePlugin.LanguageService.Parser
{
    public class MappingFileParser : IParser
    {
        private readonly ILexer m_Lexer;
        private readonly IXmlElementFactory m_MappingFileElementFactory;

        public MappingFileParser(ILexer lexer, IXmlElementFactory mappingFileElementFactory) {
            m_Lexer = lexer;
            m_MappingFileElementFactory = mappingFileElementFactory;
        }

        public virtual IFile ParseFile() {
            return new MappingFileTreeBuilder(m_MappingFileElementFactory, DefaultXmlElementFactoryContext.Instance).BuildXml(m_Lexer);
        }
    }
}