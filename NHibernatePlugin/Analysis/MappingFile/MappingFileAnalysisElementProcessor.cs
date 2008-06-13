using System;
using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using NHibernatePlugin.Helper;
using NHibernatePlugin.Highlighting;
using NHibernatePlugin.LanguageService;
using NHibernatePlugin.LanguageService.Psi;

namespace NHibernatePlugin.Analysis.MappingFile
{
    public class MappingFileAnalysisElementProcessor : IRecursiveElementProcessor
    {
        private const string UndefinedType = "UNDEFINED";
        private readonly IDaemonProcess m_Process;
        private readonly List<HighlightingInfo> m_Highlightings = new List<HighlightingInfo>();
        private string m_Assembly;
        private string m_Namespace;
        private string m_DefaultAccess;

        public MappingFileAnalysisElementProcessor(IDaemonProcess myProcess) {
            Logger.LogMessage("NHibernatePlugin: MappingFileAnalysisElementProcessor ctor called");
            m_Process = myProcess;
        }

        public bool InteriorShouldBeProcessed(IElement element) {
            if (element.Language != MappingFileLanguageService.MAPPING_FILE) {
                return false;
            }
            if (element is HibernateMappingTag) {
                return false;
            }
            if (element is ClassTag) {
                return false;
            }
            return true;
        }

        public void ProcessBeforeInterior(IElement element) {
            if (element.Language != MappingFileLanguageService.MAPPING_FILE) {
                return;
            }
            HibernateMappingTag hibernateMappingTag = element as HibernateMappingTag;
            if (hibernateMappingTag != null) {
                ProcessNHibernateMapping(hibernateMappingTag);
            }
        }

        private void ProcessNHibernateMapping(HibernateMappingTag hibernateMappingTag) {
            HighlightIfNotEmbedded(hibernateMappingTag);

            IXmlAttribute assemblyAttribute = hibernateMappingTag.GetAssemblyAttribute();
            if ((assemblyAttribute != null) && (assemblyAttribute.UnquotedValue != null)) {
                m_Assembly = assemblyAttribute.UnquotedValue;
            }

            IXmlAttribute namespaceAttribute = hibernateMappingTag.GetNamespaceAttribute();
            if ((namespaceAttribute != null) && (namespaceAttribute.UnquotedValue != null)) {
                m_Namespace = namespaceAttribute.UnquotedValue;
                INamespace namespaceElement = GetNamespaceElement(m_Namespace);
                if (namespaceElement == null) {
                    AddHighlighting(namespaceAttribute, new NamespaceHighlighting(string.Format("Undefined namespace '{0}'", m_Namespace)));
                }
            }

            IXmlAttribute accessAttribute = hibernateMappingTag.GetDefaultAccessAttribute();
            if ((accessAttribute != null) && (accessAttribute.UnquotedValue != null)) {
                m_DefaultAccess = accessAttribute.UnquotedValue;
                HighlightUndefinedAccessAttribute(accessAttribute, m_DefaultAccess);
            }

            ITreeNode nextNode = hibernateMappingTag;
            while ((nextNode = nextNode.FindNextNode(IsOfType(hibernateMappingTag, typeof(ClassTag)))) != null) {
                ProcessClassElement((ClassTag)nextNode);
            }

            nextNode = hibernateMappingTag;
            while ((nextNode = nextNode.FindNextNode(IsOfType(hibernateMappingTag, typeof(JoinedSubclassTag)))) != null) {
                ProcessJoinedSubclassElement((JoinedSubclassTag)nextNode);
            }
        }

        private void HighlightIfNotEmbedded(ITreeNode hibernateMappingTag) {
            var projectFile = hibernateMappingTag.GetProjectFile();
            if (projectFile != null) {
                var buildAction = projectFile.GetBuildActionProperty();
                Logger.LogMessage("File {0}, Build action {1}", projectFile.Name, buildAction);
                if (!buildAction.Equals(BuildAction.EMBEDDED_RESOURCE)) {
                    var firstChild = hibernateMappingTag.FirstChild;
                    AddHighlighting(firstChild, new EmbeddedResourceHighlighting(string.Format("Build action of file '{0}' is not set to 'Embedded Resource'", projectFile.Name)));
                }
            }
        }

        private void ProcessClassElement(ClassTag classTag) {
            ITypeElement typeElement = HighlightUndefinedType(classTag, "name");
            VerifyProjectReference(classTag, typeElement);
            ProcessId(classTag, typeElement);
            ProcessProperties(classTag, typeElement);
            ProcessAssociations(classTag, typeElement);
            ProcessSubclasses(classTag);
            ProcessComponents(classTag, typeElement);
        }

        private void ProcessJoinedSubclassElement(INamedTag joinedSubclassTag) {
            ITypeElement typeElement = HighlightUndefinedType(joinedSubclassTag, "name");
            VerifyProjectReference(joinedSubclassTag, typeElement);
        }

        private void VerifyProjectReference(INamedTag classTag, IDeclaredElement typeElement) {
            Logger.LogMessage("VerifyProjectReference");
            // TODO: verify if this logic works if the mapped class is references as an assembly instead of a project
            if ((classTag == null) || (typeElement == null)) {
                Logger.LogMessage("classtag {0}, typeElement {1}", classTag, typeElement);
                return;
            }

            IProject projectOfMappings = classTag.GetProject();
            if (projectOfMappings == null) {
                Logger.LogMessage("Project of mapping not found");
                return;
            }

            Logger.LogMessage("ProjectOfMappings {0}", projectOfMappings.Name);
            foreach (IProjectFile projectFile in typeElement.GetProjectFiles()) {
                Logger.LogMessage("Project {0}", projectFile.GetProject().Name);
                if (projectFile.GetProject() == projectOfMappings) {
                    Logger.LogMessage("Type is defined in same project");
                    return;
                }
                foreach (IProjectReference projectReference in classTag.GetProject().GetProjectReferences()) {
                    Logger.LogMessage("Project reference {0}", projectReference.ResolveReferencedProject().Name);
                    if (projectReference.ResolveReferencedProject() == projectFile.GetProject()) {
                        return;
                    }
                }
            }
            
            IElement typeElementTag = classTag.GetNameAttribute();
            AddHighlighting(typeElementTag, new ReferenceHighlighting(string.Format("Mapping project '{0}' should reference project '{1}'",
                projectOfMappings.Name, typeElement.GetProjectFiles()[0].GetProject().Name)));
        }

        private void ProcessComponents(ITreeNode tagHeaderNode, ITypeElement typeElement) {
            ITreeNode nextNode = tagHeaderNode;
            while ((nextNode = nextNode.FindNextNode(IsOfType(tagHeaderNode, typeof(ComponentTag)))) != null) {
                Logger.LogMessage("Component: {0}", nextNode.GetText());
                HighlightUndefinedProperty((IXmlTag)nextNode, typeElement, "class");
                ITypeElement componentPropertyType = HighlightUndefinedType((IXmlTag)nextNode, "class");
                ITypeElement componentType = PsiUtils.GetTypeElement(m_Process.Solution, GetPropertyType((IXmlTag)nextNode, typeElement));

                if ((componentType != null) && (componentPropertyType != null) && !componentType.Equals(componentPropertyType)) {
                    AddHighlighting(nextNode, new TypeHighlighting(string.Format("Class name '{0}' should be '{1}'", componentPropertyType.ShortName, componentType.ShortName)));
                }
                
                ProcessParent(nextNode, componentType);
                ProcessProperties(nextNode, componentType);
                ProcessAssociations(nextNode, componentType);
                ProcessComponents(nextNode, componentType);
            }
        }

        private void ProcessParent(ITreeNode nextNode, ITypeElement componentType) {
            ParentTag parentNode = nextNode.FindNextNode(IsOfType(nextNode, typeof(ParentTag))) as ParentTag;
            if (parentNode != null) {
                Logger.LogMessage("Parent {0}", parentNode.GetText());
                IXmlAttribute nameAttribute = parentNode.GetNameAttribute();
                if ((nameAttribute != null) && (nameAttribute.UnquotedValue != null)) {
                    HighlightUndefinedProperty(parentNode, componentType, "class");
                }
            }
        }

        private void ProcessSubclasses(ITreeNode tagHeaderNode) {
            ITreeNode nextNode = tagHeaderNode;
            while ((nextNode = nextNode.FindNextNode(IsOfType(tagHeaderNode, typeof(SubclassTag)))) != null) {
                Logger.LogMessage("Subclass: {0}", nextNode.GetText());
                ITypeElement subClassType = HighlightUndefinedType((IXmlTag)nextNode, "name");
                ProcessProperties(nextNode, subClassType);
                ProcessAssociations(nextNode, subClassType);
            }
        }

        private static string GetPropertyType(IXmlTag node, ITypeElement typeElement) {
            if (typeElement == null) {
                return null;
            }
            IXmlAttribute attribute = node.GetAttribute("name");
            if ((attribute != null) && (attribute.Value != null)) {
                string propertyName = attribute.UnquotedValue;
                IProperty property = PsiUtils.GetProperty(typeElement, propertyName);
                if (property != null) {
                    return property.Type.ToString();
                }
            }
            return null;
        }

        private void ProcessProperties(ITreeNode tagHeaderNode, ITypeElement typeElement) {
            ITreeNode nextNode = tagHeaderNode;
            while ((nextNode = nextNode.FindNextNode(IsOfType(tagHeaderNode, typeof(PropertyTag)))) != null) {
                HighlightUndefinedProperty((IXmlTag)nextNode, typeElement, "type");
            }
        }

        private void ProcessAssociationToOne(ITreeNode tagHeaderNode, ITypeElement typeElement, Type referenceType, string typeAttributeName) {
            ITreeNode nextNode = tagHeaderNode;
            while ((nextNode = nextNode.FindNextNode(IsOfType(tagHeaderNode, referenceType))) != null) {
                HighlightUndefinedProperty((IXmlTag)nextNode, typeElement, typeAttributeName);
                HighlightUndefinedType((IXmlTag)nextNode, typeAttributeName);
            }
        }

        private void ProcessAssociationToMany(ITreeNode tagHeaderNode, ITypeElement typeElement, Type referenceType, string typeAttributeName) {
            ITreeNode nextNode = tagHeaderNode;
            while ((nextNode = nextNode.FindNextNode(IsOfType(tagHeaderNode, referenceType))) != null) {
                HighlightUndefinedProperty((IXmlTag)nextNode, typeElement, typeAttributeName);
                ProcessOneToMany(nextNode, typeElement);
                ProcessManyToMany(nextNode, typeElement);
                ProcessManyToAny(nextNode, typeElement);
                ProcessCompositeElement(nextNode);
            }
        }

        private void ProcessId(ITreeNode tagHeaderNode, ITypeElement typeElement) {
            IdTag idTag = (IdTag)tagHeaderNode.FindNextNode(IsOfType(tagHeaderNode, typeof(IdTag)));
            if (idTag != null) {
                HighlightUndefinedProperty(idTag, typeElement, "type");
            }
        }

        private void ProcessAssociations(ITreeNode tagHeaderNode, ITypeElement typeElement) {
            ProcessManyToOne(tagHeaderNode, typeElement);
            ProcessOneToOne(tagHeaderNode, typeElement);
            ProcessAny(tagHeaderNode, typeElement);
            ProcessMap(tagHeaderNode, typeElement);
            ProcessSet(tagHeaderNode, typeElement);
            ProcessList(tagHeaderNode, typeElement);
            ProcessBag(tagHeaderNode, typeElement);
            ProcessIdBag(tagHeaderNode, typeElement);
            ProcessArray(tagHeaderNode, typeElement);
            ProcessPrimitiveArray(tagHeaderNode, typeElement);
        }

        private static TreeNodePredicate IsOfType(ITreeNode node, Type type) {
            return delegate(ITreeNode treeNode) { return (treeNode.GetType() == type) && (treeNode.Parent == node) ? TreeNodeActionType.ACCEPT : TreeNodeActionType.CONTINUE; };
        }

        private void ProcessOneToOne(ITreeNode node, ITypeElement typeElement) {
            ProcessAssociationToOne(node, typeElement, typeof(OneToOneTag), "class");
        }

        private void ProcessOneToMany(ITreeNode node, ITypeElement typeElement) {
            ProcessAssociationToMany(node, typeElement, typeof(OneToManyTag), "class");
        }

        private void ProcessManyToMany(ITreeNode node, ITypeElement typeElement) {
            ProcessAssociationToMany(node, typeElement, typeof(ManyToManyTag), "class");
        }

        private void ProcessManyToAny(ITreeNode node, ITypeElement typeElement) {
            ProcessAssociationToMany(node, typeElement, typeof(ManyToAnyTag), "class");
        }

        private void ProcessManyToOne(ITreeNode node, ITypeElement typeElement) {
            ProcessAssociationToOne(node, typeElement, typeof(ManyToOneTag), "class");
        }

        private void ProcessAny(ITreeNode node, ITypeElement typeElement) {
            ProcessAssociationToMany(node, typeElement, typeof(AnyTag), "class");
        }

        private void ProcessMap(ITreeNode node, ITypeElement typeElement) {
            ProcessAssociationToMany(node, typeElement, typeof(MapTag), "class");
        }

        private void ProcessSet(ITreeNode node, ITypeElement typeElement) {
            ProcessAssociationToMany(node, typeElement, typeof(SetTag), "class");
        }

        private void ProcessList(ITreeNode node, ITypeElement typeElement) {
            ProcessAssociationToMany(node, typeElement, typeof(ListTag), "class");
        }

        private void ProcessBag(ITreeNode node, ITypeElement typeElement) {
            ProcessAssociationToMany(node, typeElement, typeof(BagTag), "");
        }

        private void ProcessIdBag(ITreeNode node, ITypeElement typeElement) {
            ProcessAssociationToMany(node, typeElement, typeof(IdBagTag), "");
        }

        private void ProcessArray(ITreeNode node, ITypeElement typeElement) {
            ProcessAssociationToMany(node, typeElement, typeof(ArrayTag), "class");
        }

        private void ProcessPrimitiveArray(ITreeNode node, ITypeElement typeElement) {
            ProcessAssociationToMany(node, typeElement, typeof(PrimitiveArrayTag), "class");
        }

        private void ProcessCompositeElement(ITreeNode node) {
            ITreeNode compositeElementNode = node.FindNextNode(IsOfType(node, typeof(CompositeElementTag)));
            if (compositeElementNode == null) {
                return;
            }
            ITypeElement compositeType = HighlightUndefinedType((IXmlTag)compositeElementNode, "class");

            ProcessAssociationToOne(compositeElementNode, compositeType, typeof(CompositeElementTag), "class");
            ProcessProperties(compositeElementNode, compositeType);
            ProcessParent(compositeElementNode, compositeType);
            ProcessManyToOne(compositeElementNode, compositeType);
            ProcessNestedCompositeElement(compositeElementNode, compositeType);
        }

        private void ProcessNestedCompositeElement(ITreeNode node, ITypeElement typeElement) {
            ITreeNode nestedCompositeElementNode = node.FindNextNode(IsOfType(node, typeof(NestedCompositeElementTag)));
            if (nestedCompositeElementNode == null) {
                return;
            }
            HighlightUndefinedProperty((IXmlTag)nestedCompositeElementNode, typeElement, "class");
            ITypeElement compositeType = HighlightUndefinedType((IXmlTag)nestedCompositeElementNode, "class");

            ProcessAssociationToOne(nestedCompositeElementNode, compositeType, typeof(CompositeElementTag), "class");
            ProcessProperties(nestedCompositeElementNode, compositeType);
            ProcessParent(nestedCompositeElementNode, compositeType);
            ProcessManyToOne(nestedCompositeElementNode, compositeType);
            ProcessNestedCompositeElement(nestedCompositeElementNode, compositeType);
        }

        private ITypeElement HighlightUndefinedType(IXmlTag tag, string attributeName) {
            IXmlAttribute attribute = tag.GetAttribute(attributeName);
            if ((attribute == null) || (attribute.XmlName != attributeName) || (attribute.UnquotedValue == null)) {
                return null;
            }

            string fullQualifiedTypeName = attribute.UnquotedValue;
            Logger.LogMessage("Type is {0}", fullQualifiedTypeName);
            TypeNameParser typeNameParser = new TypeNameParser(fullQualifiedTypeName, m_Assembly, m_Namespace);
            string className = typeNameParser.QualifiedTypeName;
            if (string.IsNullOrEmpty(className)) {
                return null;
            }
            
            ITypeElement typeElement = PsiUtils.GetTypeElement(m_Process.Solution, className, m_Assembly, m_Namespace);
            if ((typeElement == null) || (typeElement.Module == null)) {
                AddHighlighting(attribute, new TypeHighlighting(string.Format("Type '{0}' could not be resolved", fullQualifiedTypeName)));
            }
            else {
                string assemblyName = typeNameParser.AssemblyName;
                if ((typeElement.Module.Name != "mscorlib") && (typeElement.Module.Name != assemblyName)) {
                    AddHighlighting(attribute, new TypeHighlighting(string.Format("Assembly name '{0}' should be '{1}'", assemblyName, typeElement.Module.Name)));
                }
            }
            return typeElement;
        }

        private INamespace GetNamespaceElement(string @namespace) {
            if (string.IsNullOrEmpty(@namespace)) {
                return null;
            }
            PsiManager psiManager = PsiManager.GetInstance(m_Process.Solution);
            IDeclarationsCache declarationsCache = psiManager.GetDeclarationsCache(DeclarationsCacheScope.SolutionScope(m_Process.Solution, true), true);
            return declarationsCache.GetNamespace(@namespace);
        }

        private void HighlightUndefinedProperty(IXmlTag xmlTag, ITypeElement typeElement, string typeAttributeName) {
            string propertyName = null;
            IXmlAttribute nameAttribute = null;
            IXmlAttribute typeAttribute = null;
            string accessMethod = null;
            IXmlAttribute accessAttribute = null;
            foreach (IXmlAttribute attribute in xmlTag.Attributes) {
                if ((attribute.XmlName == "name") && (attribute.UnquotedValue != null)) {
                    nameAttribute = attribute;
                    propertyName = attribute.UnquotedValue;
                    Logger.LogMessage("Mapped property is {0}", propertyName);
                }
                if ((attribute.XmlName == typeAttributeName) && (attribute.UnquotedValue != null)) {
                    typeAttribute = attribute;
                    string propertyType = attribute.UnquotedValue;
                    Logger.LogMessage("Property type is {0}", propertyType);
                }
                if ((attribute.XmlName == "access") && (attribute.UnquotedValue != null)) {
                    accessAttribute = attribute;
                    accessMethod = attribute.UnquotedValue;
                    Logger.LogMessage("Access method is {0}", accessMethod);
                }
            }
            if (propertyName != null) {
                AccessMethod access = HighlightUndefinedAccessAttribute(accessAttribute, accessMethod);
                HighlightUndefinedMappedProperty(xmlTag, propertyName, nameAttribute, typeAttribute, typeElement, access, typeAttributeName);
            }
        }

        private AccessMethod HighlightUndefinedAccessAttribute(IElement accessAttribute, string accessMethod) {
            Logger.LogMessage("HighlightUndefinedAccessAttribute {0}/{1}", accessAttribute, accessMethod);
            if (string.IsNullOrEmpty(accessMethod)) {
                Logger.LogMessage("  default: {0}", m_DefaultAccess);
                return new AccessMethod(m_DefaultAccess);
            }
            AccessMethod access = new AccessMethod(accessMethod);
            if ((access.Unknown) && (accessAttribute != null)) {
                AddHighlighting(accessAttribute, new AccessHighlighting(string.Format("Access method '{0}' unknown", accessMethod)));
            }
            return access;
        }

        private void HighlightUndefinedMappedProperty(IXmlTag xmlTag, string propertyName, IElement nameAttribute, IElement typeAttribute, ITypeElement typeElement, AccessMethod access, string attributeName) {
            IAccessor accessor = null;
            IField field = null;
            string className = (typeElement == null) ? UndefinedType : typeElement.ShortName;
            if ((access.Method == AccessMethod.AccessMethodProperty) || (access.Method == AccessMethod.AccessMethodNosetter)) {
                accessor = PsiUtils.GetPropertyGetter(typeElement, propertyName);
                HighlightPropertyGetter(propertyName, accessor, nameAttribute, className);
            }
            if ((access.Method == AccessMethod.AccessMethodProperty) || (access.Method == AccessMethod.AccessMethodNosetter)) {
                accessor = PsiUtils.GetPropertySetter(typeElement, propertyName);
                HighlightPropertySetter(propertyName, accessor, nameAttribute, className);
            }
            if ((access.Method == AccessMethod.AccessMethodField) || (access.Method == AccessMethod.AccessMethodNosetter)) {
                field = PsiUtils.GetField(typeElement, propertyName, access);
                HighlightField(access, propertyName, nameAttribute, className, field);
            }

            if (typeAttribute != null) {
                HighlightPropertyType(accessor, nameAttribute, field, typeAttribute, xmlTag, attributeName);
            }
        }

        private void HighlightPropertyType(IAccessor accessor, IElement nameAttribute, ITypeOwner field, IElement typeAttribute, IXmlTag xmlTag, string attributeName) {
            ITypeElement propertyClass = HighlightUndefinedType(xmlTag, attributeName);
            if (accessor != null) {
                ITypeElement propertyTypeElement = PsiUtils.GetTypeElement(m_Process.Solution, accessor.Parameters[0].Type.ToString());
                if ((propertyTypeElement != null) && (propertyClass != null) && (!propertyClass.IsDescendantOf(propertyTypeElement))) {
                    AddHighlighting(typeAttribute, new TypeHighlighting(string.Format("Class name '{0}' should be '{1}' or a descendant", propertyClass.ShortName, propertyTypeElement.ShortName)));
                }
            }
            if (field != null) {
                ITypeElement fieldTypeElement = PsiUtils.GetTypeElement(m_Process.Solution, field.Type.ToString());
                if ((fieldTypeElement != null) && (propertyClass != null) && (!propertyClass.IsDescendantOf(fieldTypeElement))) {
                    AddHighlighting(typeAttribute, new TypeHighlighting(string.Format("Class name '{0}' should be '{1}' or a descendant", propertyClass.ShortName, fieldTypeElement.ShortName)));
                }
            }

            if ((accessor != null) && (field != null)) {
                ITypeElement propertyTypeElement = PsiUtils.GetTypeElement(m_Process.Solution, accessor.Parameters[0].Type.ToString());
                ITypeElement fieldTypeElement = PsiUtils.GetTypeElement(m_Process.Solution, field.Type.ToString());
                if ((propertyTypeElement != null) && (fieldTypeElement != null) && (!propertyTypeElement.Equals(fieldTypeElement))) {
                    AddHighlighting(nameAttribute, new TypeHighlighting(string.Format("Property and field type differ: '{0}' vs. '{1}'", propertyTypeElement.ShortName, fieldTypeElement.ShortName)));
                }
            }
        }

        private void HighlightField(AccessMethod access, string propertyName, IElement nameAttribute, string className, IField field) {
            if (field == null) {
                AddHighlighting(nameAttribute, new PropertyHighlighting(string.Format("Field '{0}' not found in class '{1}'", access.Name(propertyName), className)));
            }
        }

        private void HighlightPropertySetter(string propertyName, IAccessor accessor, IElement nameAttribute, string className) {
            if (accessor == null) {
                AddHighlighting(nameAttribute, new PropertyHighlighting(string.Format("Setter for property '{0}' not found in class '{1}'", propertyName, className)));
            }
        }

        private void HighlightPropertyGetter(string propertyName, IAccessor accessor, IElement nameAttribute, string className) {
            if (accessor == null) {
                AddHighlighting(nameAttribute, new PropertyHighlighting(string.Format("Getter for property '{0}' not found in class '{1}'", propertyName, className)));
            }
        }

        private void AddHighlighting(IElement elementToHighlight, IHighlighting highlighting) {
            m_Highlightings.Add(new HighlightingInfo(elementToHighlight.GetDocumentRange(), highlighting));
        }

        public void ProcessAfterInterior(IElement element) {
        }

        public bool ProcessingIsFinished {
            get { return m_Process.InterruptFlag; }
        }

        public List<HighlightingInfo> Highlightings {
            get { return m_Highlightings; }
        }
    }
}