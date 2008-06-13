using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class MiscellaneousTag : XmlTag
    {
        public MiscellaneousTag()
            : base(MappingFileElementType.MISCELLANEOUS) {
            Logger.LogMessage("MiscellaneousTag ctor");
        }
    }
}