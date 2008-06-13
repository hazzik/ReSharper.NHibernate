using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class IdBagTag : XmlTag
    {
        public IdBagTag()
            : base(MappingFileElementType.IDBAG) {
            Logger.LogMessage("IdBagTag ctor");
        }
    }
}