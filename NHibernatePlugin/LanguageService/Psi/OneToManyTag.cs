using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class OneToManyTag : XmlTag
    {
        public OneToManyTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("OneToManyTag ctor");
        }
    }
}