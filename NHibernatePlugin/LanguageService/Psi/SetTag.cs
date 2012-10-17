using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class SetTag : XmlTag
    {
        public SetTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("SetTag ctor");
        }
    }
}