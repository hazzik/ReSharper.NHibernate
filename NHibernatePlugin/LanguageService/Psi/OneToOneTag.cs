using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class OneToOneTag : XmlTag
    {
        public OneToOneTag()
            : base(MappingFileElementType.ONETOONE) {
            Logger.LogMessage("OneToOneTag ctor");
        }
    }
}