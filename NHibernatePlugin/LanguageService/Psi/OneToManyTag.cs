using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class OneToManyTag : XmlTag
    {
        public OneToManyTag()
            : base(MappingFileElementType.ONETOMANY) {
            Logger.LogMessage("OneToManyTag ctor");
        }
    }
}