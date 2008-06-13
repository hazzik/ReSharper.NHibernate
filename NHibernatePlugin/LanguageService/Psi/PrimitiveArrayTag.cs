using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class PrimitiveArrayTag : XmlTag
    {
        public PrimitiveArrayTag()
            : base(MappingFileElementType.PRIMITIVEARRAY) {
            Logger.LogMessage("PrimitiveArrayTag ctor");
        }
    }
}