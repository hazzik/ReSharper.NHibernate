using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class ArrayTag : XmlTag
    {
        public ArrayTag()
            : base(MappingFileElementType.ARRAY) {
            Logger.LogMessage("ArrayTag ctor");
        }
    }
}