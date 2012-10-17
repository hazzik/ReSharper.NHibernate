using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class ParentTag : XmlTag
    {
        public ParentTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("ParentTag ctor");
        }

        public IXmlAttribute GetNameAttribute() {
            return this.GetAttribute("name");
        }
    }
}