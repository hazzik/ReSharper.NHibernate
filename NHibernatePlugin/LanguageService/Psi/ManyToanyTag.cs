using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class ManyToAnyTag : XmlTag
    {
        public ManyToAnyTag()
            : base(MappingFileElementType.MANYTOANY) {
            Logger.LogMessage("ManyToAnyTag ctor");
        }
    }
}