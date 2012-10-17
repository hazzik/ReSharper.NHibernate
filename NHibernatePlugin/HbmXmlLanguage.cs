using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Xml;

namespace NHibernatePlugin
{
    [LanguageDefinition(Name)]
    public class HbmXmlLanguage : XmlLanguage
    {
        public new const string Name = "MappingFile";

        [CanBeNull]
        public static readonly HbmXmlLanguage Instance = new HbmXmlLanguage();

        private HbmXmlLanguage()
            : base(Name, "MappingFile") {
        }

        protected HbmXmlLanguage([NotNull] string name)
            : base(name) {
        }

        protected HbmXmlLanguage([NotNull] string name, [NotNull] string presentableName)
            : base(name, presentableName) {
        }
    }
}