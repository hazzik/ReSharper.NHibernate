using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class ClassTag : XmlTag, INamedTag
    {
        public ClassTag()
            : base(MappingFileElementType.CLASS) {
            Logger.LogMessage("ClassTag ctor");
        }

        public IXmlAttribute GetNameAttribute() {
            return GetAttribute("name");
        }
    }
}