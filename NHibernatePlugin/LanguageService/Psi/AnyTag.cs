using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class AnyTag : XmlTag
    {
        public AnyTag()
            : base(MappingFileElementType.ANY) {
            Logger.LogMessage("AnyTag ctor");
        }
    }
}