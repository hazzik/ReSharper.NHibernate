using JetBrains.Application;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Parsing;
using NHibernatePlugin.LanguageService.Psi;

namespace NHibernatePlugin.LanguageService.Parser
{
    public class MappingFileParser : IParser
    {
        private readonly CheckForInterrupt m_CheckForInterrupt;
        private readonly ILexer m_Lexer;

        public MappingFileParser(ILexer lexer, CheckForInterrupt checkForInterrupt) {
            m_Lexer = lexer;
            m_CheckForInterrupt = checkForInterrupt;
        }

        public virtual IFile ParseFile() {
            XmlTreeBuilder builder = new MappingFileTreeBuilder(MappingFileElementFactory.Instance, m_CheckForInterrupt);
            IMappingFile file = (IMappingFile)builder.BuildXml(m_Lexer);
            if (file == null) {
                return null;
            }
            return file.ToTreeNode();
        }
    }
}