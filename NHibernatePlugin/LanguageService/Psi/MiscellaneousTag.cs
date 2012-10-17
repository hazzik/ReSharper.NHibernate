using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class MiscellaneousTag : XmlTag
    {
        public MiscellaneousTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("MiscellaneousTag ctor");
        }
    }
}