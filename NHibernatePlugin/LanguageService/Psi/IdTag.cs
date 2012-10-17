using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class IdTag : XmlTag
    {
        public IdTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("IdTag ctor");
        }
    }
}