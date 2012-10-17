using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class MapTag : XmlTag
    {
        public MapTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("MapTag ctor");
        }
    }
}