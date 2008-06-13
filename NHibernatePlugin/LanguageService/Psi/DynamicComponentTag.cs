using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class DynamicComponentTag : XmlTag
    {
        public DynamicComponentTag()
            : base(MappingFileElementType.DYNAMICCOMPONENT) {
            Logger.LogMessage("DynamicComponentTag ctor");
        }
    }
}