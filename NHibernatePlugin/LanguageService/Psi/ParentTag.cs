using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class ParentTag : XmlTag
    {
        public ParentTag()
            : base(MappingFileElementType.PARENT) {
            Logger.LogMessage("ParentTag ctor");
        }

        public IXmlAttribute GetNameAttribute() {
            return GetAttribute("name");
        }
    }
}