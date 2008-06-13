using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class ManyToManyTag : XmlTag
    {
        public ManyToManyTag()
            : base(MappingFileElementType.MANYTOMANY) {
            Logger.LogMessage("ManyToManyTag ctor");
        }
    }
}