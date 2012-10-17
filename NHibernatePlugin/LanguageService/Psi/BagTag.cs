using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class BagTag : XmlTag
    {
        public BagTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("BagTag ctor");
        }
    }
}