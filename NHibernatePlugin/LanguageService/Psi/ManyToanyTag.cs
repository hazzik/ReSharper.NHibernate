using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class ManyToAnyTag : XmlTag
    {
        public ManyToAnyTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("ManyToAnyTag ctor");
        }
    }
}