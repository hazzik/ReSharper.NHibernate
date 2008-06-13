using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class ComponentTag : XmlTag
    {
        public ComponentTag()
            : base(MappingFileElementType.COMPONENT) {
            Logger.LogMessage("ComponentTag ctor");
        }
    }
}