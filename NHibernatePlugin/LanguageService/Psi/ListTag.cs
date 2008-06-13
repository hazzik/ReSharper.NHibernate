using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class ListTag : XmlTag
    {
        public ListTag()
            : base(MappingFileElementType.LIST) {
            Logger.LogMessage("ListTag ctor");
        }
    }
}