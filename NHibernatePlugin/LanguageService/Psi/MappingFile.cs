using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class MappingFile : XmlFile, IMappingFile
    {
        public MappingFile()
            : base(MappingFileElementType.FILE) {
            Logger.LogMessage("MappingFile ctor");
        }
    }

    public interface IMappingFile : IXmlFile
    {
    }
}