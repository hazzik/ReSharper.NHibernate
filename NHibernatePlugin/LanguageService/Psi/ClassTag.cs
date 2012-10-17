using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class ClassTag : XmlTag, INamedTag
    {
        public ClassTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("ClassTag ctor");
        }

        public IXmlAttribute GetNameAttribute() {
            return this.GetAttribute("name");
        }
    }
}