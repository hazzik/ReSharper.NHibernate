using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.References;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class ClassNameAttribute : NameAttribute
    {
        public ClassNameAttribute(string containerName, XmlCompositeNodeType type)
            : base(type) {
            Logger.LogMessage("ClassNameAttribute ctor for container {0}", containerName);
            m_ContainerName = containerName;
        }

        protected ClassNameAttribute(XmlCompositeNodeType compositeNodeType)
            : base(compositeNodeType) {
        }

        protected IReference[] CreateReferencesInternal(IXmlAttributeValue value) {
            Logger.LogMessage("CreateReferencesInternal for {0}", value.GetText());
            IXmlValueToken valueToken = value.ToTreeNode().ValueToken;
            if (valueToken == null) {
                return null;
            }
            return new IReference[] {new ClassNameReference(this, valueToken, valueToken.UnquotedValueRange)};
        }

        protected override IReference[] CreateReferences() {
            IXmlAttributeValue value = Value;
            if (value == null) {
                return EmptyArray<IReference>.Instance;
            }
            return CreateReferencesInternal(value);
        }
    }
}