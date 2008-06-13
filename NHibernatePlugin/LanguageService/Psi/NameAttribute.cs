using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;
using NHibernatePlugin.LanguageService.References;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class NameAttribute : XmlAttribute
    {
        private readonly string m_ContainerName;

        public NameAttribute(string containerName)
            : base(MappingFileElementType.NAME_ATTRIBUTE) {
            Logger.LogMessage("NameAttribute ctor for container {0}", containerName);
            m_ContainerName = containerName;
        }

        protected IReferenceImpl[] CreateReferencesInternal(IXmlAttributeValue value) {
            Logger.LogMessage("CreateReferencesInternal for {0}", value.GetText());
            IXmlValueToken valueToken = value.ToTreeNode().ValueToken;
            if (valueToken == null) {
                return null;
            }
            return new IReferenceImpl[] {new NameReference(this, valueToken, valueToken.UnquotedValueRange)};
        }

        protected NameAttribute(CompositeNodeType compositeNodeType)
            : base(compositeNodeType) {
        }

        protected override void ClearCachedData() {
            ResetReferences();
            base.ClearCachedData();
        }

        protected override IReferenceImpl[] CreateReferences() {
            IXmlAttributeValue value = Value;
            if (value == null) {
                return EmptyArray<IReferenceImpl>.Instance;
            }
            return CreateReferencesInternal(value);
        }

        public string ContainerName {
            get { return m_ContainerName; }
        }
    }
}