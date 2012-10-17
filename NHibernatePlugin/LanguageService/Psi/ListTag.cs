using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class ListTag : XmlTag
    {
        public ListTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("ListTag ctor");
        }
    }
}