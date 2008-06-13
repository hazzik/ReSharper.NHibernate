using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class FileTag : XmlTag
    {
        public FileTag()
            : base(MappingFileElementType.FILE) {
            Logger.LogMessage("FileTag ctor");
        }
    }
}