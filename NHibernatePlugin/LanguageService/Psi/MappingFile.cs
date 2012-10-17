using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class MappingFile : XmlFile, IMappingFile
    {
        public MappingFile(XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("MappingFile ctor");
        }
    }

    public interface IMappingFile : IXmlFile
    {
    }
}