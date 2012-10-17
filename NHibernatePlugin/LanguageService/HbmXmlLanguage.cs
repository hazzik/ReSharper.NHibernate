using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Xml;

namespace NHibernatePlugin.LanguageService
{
    [LanguageDefinition(Name)]
    public class HbmXmlLanguage : XmlLanguage
    {
        public new const string Name = "HBM_XML";

        [CanBeNull]
        public static readonly HbmXmlLanguage Instance;

        private HbmXmlLanguage()
            : base(Name, "HbmXml") {
        }

        protected HbmXmlLanguage([NotNull] string name)
            : base(name) {
        }

        protected HbmXmlLanguage([NotNull] string name, [NotNull] string presentableName)
            : base(name, presentableName) {
        }
    }
}