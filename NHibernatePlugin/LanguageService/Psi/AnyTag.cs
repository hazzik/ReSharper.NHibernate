using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class AnyTag : XmlTag
    {
        public AnyTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("AnyTag ctor");
        }
    }
}