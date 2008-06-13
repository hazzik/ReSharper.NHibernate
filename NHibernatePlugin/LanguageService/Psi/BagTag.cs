using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class BagTag : XmlTag
    {
        public BagTag()
            : base(MappingFileElementType.BAG) {
            Logger.LogMessage("BagTag ctor");
        }
    }
}