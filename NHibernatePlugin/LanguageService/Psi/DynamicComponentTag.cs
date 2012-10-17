using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class DynamicComponentTag : XmlTag
    {
        public DynamicComponentTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("DynamicComponentTag ctor");
        }
    }
}