using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class JoinedSubclassTag : XmlTag, INamedTag
    {
        public JoinedSubclassTag()
            : base(MappingFileElementType.JOINEDSUBCLASS) {
            Logger.LogMessage("JoinedSubclassTag ctor");
        }

        public IXmlAttribute GetNameAttribute() {
            return GetAttribute("name");
        }
        
    }
}