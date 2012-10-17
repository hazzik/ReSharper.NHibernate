using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class CompositeElementTag : XmlTag
    {
        public CompositeElementTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("CompositeElementTag ctor");
        }
    }
}