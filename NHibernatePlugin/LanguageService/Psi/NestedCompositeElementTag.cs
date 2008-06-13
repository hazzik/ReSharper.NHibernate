using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class NestedCompositeElementTag : XmlTag
    {
        public NestedCompositeElementTag()
            : base(MappingFileElementType.NESTED_COMPOSITE_ELEMENT) {
            Logger.LogMessage("NestedCompositeElementTag ctor");
        }
    }
}