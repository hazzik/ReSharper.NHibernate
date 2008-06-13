using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class CompositeElementTag : XmlTag
    {
        public CompositeElementTag()
            : base(MappingFileElementType.COMPOSITE_ELEMENT) {
            Logger.LogMessage("CompositeElementTag ctor");
        }
    }
}