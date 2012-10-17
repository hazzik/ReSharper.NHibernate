using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class OneToOneTag : XmlTag
    {
        public OneToOneTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("OneToOneTag ctor");
        }
    }
}