using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class NestedCompositeElementTag : XmlTag
    {
        public NestedCompositeElementTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("NestedCompositeElementTag ctor");
        }
    }
}