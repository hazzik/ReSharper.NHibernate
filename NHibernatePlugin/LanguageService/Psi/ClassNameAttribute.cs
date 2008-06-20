using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;
using NHibernatePlugin.LanguageService.References;

namespace NHibernatePlugin.LanguageService.Psi
{
    public class ClassNameAttribute : NameAttribute
    {
        public ClassNameAttribute(string containerName)
            : base(MappingFileElementType.CLASS_NAME_ATTRIBUTE) {
            Logger.LogMessage("ClassNameAttribute ctor for container {0}", containerName);
            m_ContainerName = containerName;
        }

        protected ClassNameAttribute(CompositeNodeType compositeNodeType)
            : base(compositeNodeType) {
        }

        protected IReferenceImpl[] CreateReferencesInternal(IXmlAttributeValue value) {
            Logger.LogMessage("CreateReferencesInternal for {0}", value.GetText());
            IXmlValueToken valueToken = value.ToTreeNode().ValueToken;
            if (valueToken == null) {
                return null;
            }
            return new IReferenceImpl[] {new ClassNameReference(this, valueToken, valueToken.UnquotedValueRange)};
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