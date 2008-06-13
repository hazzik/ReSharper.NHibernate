using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class IdTag : XmlTag
    {
        public IdTag()
            : base(MappingFileElementType.ID) {
            Logger.LogMessage("IdTag ctor");
        }
    }
}