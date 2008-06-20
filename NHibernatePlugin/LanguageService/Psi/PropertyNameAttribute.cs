using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;
using NHibernatePlugin.LanguageService.References;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class PropertyNameAttribute : NameAttribute
    {
        public PropertyNameAttribute(string containerName)
            : base(MappingFileElementType.PROPERTY_NAME_ATTRIBUTE) {
            Logger.LogMessage("PropertyNameAttribute ctor for container {0}", containerName);
            m_ContainerName = containerName;
        }

        protected PropertyNameAttribute(CompositeNodeType compositeNodeType)
            : base(compositeNodeType) {
        }

        protected IReferenceImpl[] CreateReferencesInternal(IXmlAttributeValue value) {
            Logger.LogMessage("CreateReferencesInternal for {0}", value.GetText());
            IXmlValueToken valueToken = value.ToTreeNode().ValueToken;
            if (valueToken == null) {
                return null;
            }
            return new IReferenceImpl[] {new PropertyNameReference(this, valueToken, valueToken.UnquotedValueRange)};
        }

        protected override IReferenceImpl[] CreateReferences() {
            IXmlAttributeValue value = Value;
            if (value == null) {
                return EmptyArray<IReferenceImpl>.Instance;
            }
            return CreateReferencesInternal(value);
        }
    }
}