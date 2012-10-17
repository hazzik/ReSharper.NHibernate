using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class ManyToManyTag : XmlTag
    {
        public ManyToManyTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("ManyToManyTag ctor");
        }
    }
}