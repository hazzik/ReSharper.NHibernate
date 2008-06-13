using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class PropertyTag : XmlTag 
    {
        public PropertyTag()
            : base(MappingFileElementType.PROPERTY) {
            Logger.LogMessage("PropertyTag ctor");
        }
    }
}