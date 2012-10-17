using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class PropertyTag : XmlTag
    {
        public PropertyTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("PropertyTag ctor");
        }
    }
}