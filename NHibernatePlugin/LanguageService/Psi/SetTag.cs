using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class SetTag : XmlTag
    {
        public SetTag()
            : base(MappingFileElementType.SET) {
            Logger.LogMessage("SetTag ctor");
        }
    }
}