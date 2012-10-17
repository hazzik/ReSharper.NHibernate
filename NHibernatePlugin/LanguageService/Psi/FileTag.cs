using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class FileTag : XmlTag
    {
        public FileTag(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("FileTag ctor");
        }
    }
}