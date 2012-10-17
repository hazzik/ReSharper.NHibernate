using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class ComponentTag : XmlTag
    {
        public ComponentTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("ComponentTag ctor");
        }
    }
}