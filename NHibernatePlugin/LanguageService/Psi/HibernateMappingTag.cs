using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class HibernateMappingTag : XmlTag
    {
        public HibernateMappingTag() 
            : base(MappingFileElementType.HIBERNATE_MAPPING) {
            Logger.LogMessage("HibernateMappingTag ctor");
        }

        public IXmlAttribute GetAssemblyAttribute() {
            return GetAttribute("assembly");
        }

        public IXmlAttribute GetNamespaceAttribute() {
            return GetAttribute("namespace");
        }

        public IXmlAttribute GetDefaultAccessAttribute() {
            return GetAttribute("default-access");
        }
    }
}