using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class IdBagTag : XmlTag
    {
        public IdBagTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("IdBagTag ctor");
        }
    }
}