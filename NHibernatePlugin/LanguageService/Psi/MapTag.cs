using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class MapTag : XmlTag
    {
        public MapTag()
            : base(MappingFileElementType.MAP) {
            Logger.LogMessage("MapTag ctor");
        }
    }
}