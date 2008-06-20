using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;

namespace NHibernatePlugin.LanguageService.Psi
{
    public abstract class NameAttribute : XmlAttribute
    {
        protected string m_ContainerName;

        protected NameAttribute(CompositeNodeType compositeNodeType)
            : base(compositeNodeType) {
        }

        public string ContainerName {
            get { return m_ContainerName; }
        }

        protected override void ClearCachedData() {
            ResetReferences();
            base.ClearCachedData();
        }
    }
}