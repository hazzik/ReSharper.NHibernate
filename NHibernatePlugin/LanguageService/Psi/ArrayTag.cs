using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class ArrayTag : XmlTag
    {
        public ArrayTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("ArrayTag ctor");
        }
    }
}