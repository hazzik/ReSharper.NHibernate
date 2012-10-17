using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class HibernateMappingTag : XmlTag
    {
        public HibernateMappingTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("HibernateMappingTag ctor");
        }

        public IXmlAttribute GetAssemblyAttribute() {
            return this.GetAttribute("assembly");
        }

        public IXmlAttribute GetNamespaceAttribute() {
            return this.GetAttribute("namespace");
        }

        public IXmlAttribute GetDefaultAccessAttribute() {
            return this.GetAttribute("default-access");
        }
    }
}