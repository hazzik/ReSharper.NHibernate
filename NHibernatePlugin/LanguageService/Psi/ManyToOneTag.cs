using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class ManyToOneTag : XmlTag
    {
        public ManyToOneTag()
            : base(MappingFileElementType.MANYTOONE) {
            Logger.LogMessage("ManyToOneTag ctor");
        }
    }
}