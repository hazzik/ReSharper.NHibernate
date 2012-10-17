using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class JoinedSubclassTag : XmlTag, INamedTag
    {
        public JoinedSubclassTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("JoinedSubclassTag ctor");
        }

        public IXmlAttribute GetNameAttribute() {
            return this.GetAttribute("name");
        }
    }
}