using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class SubclassTag : XmlTag
    {
        public SubclassTag()
            : base(MappingFileElementType.SUBCLASS) {
            Logger.LogMessage("SubclassTag ctor");
        }
    }
}