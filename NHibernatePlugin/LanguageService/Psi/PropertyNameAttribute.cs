using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree.References;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.References;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class PropertyNameAttribute : NameAttribute
    {
        public PropertyNameAttribute(string containerName, XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("PropertyNameAttribute ctor for container {0}", containerName);
            m_ContainerName = containerName;
        }

        protected PropertyNameAttribute(XmlCompositeNodeType compositeNodeType)
            : base(compositeNodeType) {
        }

        protected IXmlReference[] CreateReferencesInternal(IXmlAttributeValue value) {
            Logger.LogMessage("CreateReferencesInternal for {0}", value.GetText());
            IXmlValueToken valueToken = value.ValueToken;
            if (valueToken == null) {
                return null;
            }
            return new IXmlReference[] {new PropertyNameReference(this, valueToken, valueToken.UnquotedValueRange)};
        }

        protected override IXmlReference[] CreateReferences() {
            IXmlAttributeValue value = Value;
            if (value == null) {
                return EmptyArray<IXmlReference>.Instance;
            }
            return CreateReferencesInternal(value);
        }
    }
}