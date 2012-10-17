using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class PrimitiveArrayTag : XmlTag
    {
        public PrimitiveArrayTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("PrimitiveArrayTag ctor");
        }
    }
}