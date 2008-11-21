using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.Util;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using NHibernatePlugin.Helper;
using NHibernatePlugin.LanguageService.Parser;
using NHibernatePlugin.LanguageService.Psi;
using NHibernatePlugin.TypeNames.Parser;

namespace NHibernatePlugin.LanguageService
{
    public static class PsiUtils
    {
        public static IAccessor GetPropertySetter(ITypeElement typeElement, string propertyName) {
            if (typeElement == null) {
                return null;
            }
            foreach (IProperty property in typeElement.Properties) {
                if ((property.ShortName == propertyName) && (property.Setter != null)) {
                    return property.Setter;
                }
            }
            IList<IDeclaredType> superTypes = typeElement.GetSuperTypes();
            foreach (IDeclaredType superType in superTypes) {
                IAccessor accessor = GetPropertySetter(superType.GetTypeElement(), propertyName);
                if (accessor != null) {
                    return accessor;
                }
            }
            return null;
        }
        
        public static IAccessor GetPropertyGetter(ITypeElement typeElement, string propertyName) {
            if (typeElement == null) {
                return null;
            }
            foreach (IProperty property in typeElement.Properties) {
                if ((property.ShortName == propertyName) && (property.Getter != null)) {
                    return property.Getter;
                }
            }
            IList<IDeclaredType> superTypes = typeElement.GetSuperTypes();
            foreach (IDeclaredType superType in superTypes) {
                IAccessor accessor = GetPropertyGetter(superType.GetTypeElement(), propertyName);
                if (accessor != null) {
                    return accessor;
                }
            }
            return null;
        }
        
        public static IProperty GetProperty(ITypeElement typeElement, string propertyName) {
            if (typeElement == null) {
                return null;
            }
            foreach (IProperty property in typeElement.Properties) {
                if (property.ShortName == propertyName) {
                    return property;
                }
            }
            IList<IDeclaredType> superTypes = typeElement.GetSuperTypes();
            foreach (IDeclaredType superType in superTypes) {
                IProperty property = GetProperty(superType.GetTypeElement(), propertyName);
                if (property != null) {
                    return property;
                }
            }
            return null;
        }

        public static IProperty GetProperty(ISolution solution, PropertyNameAttribute propertyNameAttribute, string attributeName, IXmlTag containingElement) {
            string propertyName = propertyNameAttribute.UnquotedValue;
            Logger.LogMessage("GetProperty {0} ({1})", propertyName, attributeName);

            ITypeElement typeElement = GetTypeElement(solution, containingElement, attributeName);
            if (typeElement == null) {
                Logger.LogMessage("  type not found");
                return null;
            }
            Logger.LogMessage("  in type {0}", typeElement.ShortName);
            return GetProperty(typeElement, propertyName);
        }

        private static ITypeElement GetTypeElement(ISolution solution, IXmlTag classTag, string attributeName) {
            IXmlAttribute nameAttribute = classTag.GetAttribute(attributeName);
            if ((nameAttribute == null) || (nameAttribute.UnquotedValue == null)) {
                return null;
            }
            return GetTypeElement(classTag, solution, nameAttribute.UnquotedValue);
        }

        public static ITypeElement GetTypeElement(IXmlTag classTag, ISolution solution, string className) {
            string @namespace = "";
            string assembly = "";
            HibernateMappingTag hibernateMapping = classTag.GetContainingElement<HibernateMappingTag>(false);
            if (hibernateMapping != null) {
                IXmlAttribute namespaceAttribute = hibernateMapping.GetNamespaceAttribute();
                @namespace = namespaceAttribute == null ? "" : namespaceAttribute.UnquotedValue;

                IXmlAttribute assemblyAttribute = hibernateMapping.GetAssemblyAttribute();
                assembly = assemblyAttribute == null ? "" : assemblyAttribute.UnquotedValue;
            }

            return GetTypeElement(solution, className, assembly, @namespace);
        }

        public static IField GetField(ITypeElement typeElement, string fieldName, AccessMethod access) {
            if (typeElement == null) {
                return null;
            }
            foreach (IField field in GetFields(typeElement)) {
                if (field.ShortName == access.Name(fieldName)) {
                    return field;
                }
            }
            IList<IDeclaredType> superTypes = typeElement.GetSuperTypes();
            foreach (IDeclaredType superType in superTypes) {
                IField field = GetField(superType.GetTypeElement(), fieldName, access);
                if (field != null) {
                    return field;
                }
            }
            return null;
        }

        private static IEnumerable<IField> GetFields(ITypeElement typeElement) {
            IList<IField> result = new List<IField>();
            IEnumerable<TypeMemberInstance> members = MiscUtil.GetAllClassMembers(typeElement);
            foreach (TypeMemberInstance member in members) {
                IField field = member.Element as IField;
                if (field != null) {
                    result.Add(field);
                }
            }
            return result;
        }

        private static ITypeElement GetTypeElement(ISolution solution, string fullQualifiedTypeName, string assembly, string @namespace) {
            fullQualifiedTypeName = ClrNameFromNHibernateSpecialName(fullQualifiedTypeName);
            TypeNameParser typeNameParser = new TypeNameParser(fullQualifiedTypeName, assembly, @namespace);
            if (string.IsNullOrEmpty(typeNameParser.TypeName)) {
                return null;
            }

            PsiManager psiManager = PsiManager.GetInstance(solution);
            IDeclarationsCache declarationsCache = psiManager.GetDeclarationsCache(DeclarationsCacheScope.SolutionScope(solution, true), true);

            TypeNames.Parser.Parser parser = new TypeNames.Parser.Parser();
            IParserError error;
            var parsedType = parser.Parse(fullQualifiedTypeName, out error);
            if (parsedType != null) {
                var theTypeElement = GetTypeElement(parsedType.TypeName, declarationsCache);
                // TODO: parsedType.TypeParameters berücksichtigen
            }

            ITypeElement typeElement = GetTypeElement(typeNameParser.QualifiedTypeName, declarationsCache);
            if (typeElement != null) {
                return typeElement;
            }
            if (!fullQualifiedTypeName.Contains(".") && !fullQualifiedTypeName.Contains(",")) {
                return GetTypeElement(typeNameParser.TypeName, declarationsCache);
            }
            return null;
        }

        private static string ClrNameFromNHibernateSpecialName(string fullQualifiedTypeName) {
            switch(fullQualifiedTypeName) {
                case "AnsiChar":
                    return "System.Char";
                case "TrueFalse":
                    return "System.Boolean";
                case "YesNo":
                    return "System.Boolean";
                case "AnsiString":
                    return "System.String";
                case "StringClob":
                    return "System.String";
                case "CultureInfo":
                    return "System.Globalization.CultureInfo";
                case "Binary":
                    return "System.Byte[]";
                case "BinaryBlob":
                    return "System.Byte[]";
            }
            return fullQualifiedTypeName;
        }

        private static ITypeElement GetTypeElement(string className, IDeclarationsCache declarationsCache) {
            // TODO: array type (System.Byte[])
            ITypeElement typeElement = declarationsCache.GetTypeElementByCLRName(className);
            if (typeElement == null) {
                IEnumerable<IDeclaredElement> declaredElements = declarationsCache.GetElementsByShortName(className);
                if ((declaredElements == null) || (declaredElements.IsEmpty())) {
                    declaredElements = declarationsCache.GetElementsAtQualifiedName(className);
                }
                if (declaredElements.Length() == 1) {
                    foreach (var element in declaredElements) { // return first element
                        return element as ITypeElement;
                    }
                }
            }
            return typeElement;
        }

        public static IField GetField(ISolution solution, NameAttribute propertyNameAttribute, string attributeName, IXmlTag containingElement) {
            string fieldName = propertyNameAttribute.UnquotedValue;
            Logger.LogMessage("GetField {0} ({1})", fieldName, attributeName);

            AccessMethod access = null;
            XmlTag xmlTag = propertyNameAttribute.GetContainingElement<XmlTag>(true);
            if (xmlTag != null) {
                IXmlAttribute accessAttribute = xmlTag.GetAttribute("access");
                if (accessAttribute != null) {
                    Logger.LogMessage("  access attribute found: {0}", accessAttribute.UnquotedValue);
                    access = new AccessMethod(accessAttribute.UnquotedValue);
                }
            }
            if (access == null) {
                HibernateMappingTag hibernateMappingTag = Parent<HibernateMappingTag>(propertyNameAttribute, Keyword.HibernateMapping);
                if (hibernateMappingTag != null) {
                    IXmlAttribute accessAttribute = hibernateMappingTag.GetDefaultAccessAttribute();
                    if (accessAttribute != null) {
                        Logger.LogMessage("  default access attribute found: {0}", accessAttribute.UnquotedValue);
                        access = new AccessMethod(accessAttribute.UnquotedValue);
                    }
                }
            }
            if (access == null) {
                access = new AccessMethod("");
            }

            ITypeElement typeElement = GetTypeElement(solution, containingElement, attributeName);
            if (typeElement == null) {
                return null;
            }
            IField field = GetField(typeElement, fieldName, access);
            if (field == null) {
                return null;
            }
            Logger.LogMessage("  field found: {0}", field.ShortName);
            return field;
        }

        public static ComponentTag ParentComponent(NameAttribute nameAttribute) {
            return Parent<ComponentTag>(nameAttribute, Keyword.Component);
        }

        public static CompositeElementTag ParentComposite(NameAttribute nameAttribute) {
            return Parent<CompositeElementTag>(nameAttribute, Keyword.CompositeElement);
        }

        public static NestedCompositeElementTag ParentNestedComposite(NameAttribute nameAttribute) {
            return Parent<NestedCompositeElementTag>(nameAttribute, Keyword.NestedCompositeElement);
        }

        public static SubclassTag ParentSubclass(NameAttribute nameAttribute) {
            return Parent<SubclassTag>(nameAttribute, Keyword.Subclass);
        }

        private static T Parent<T>(NameAttribute nameAttribute, string keyword) where T : IXmlTag {
            T subclassTag = nameAttribute.GetContainingElement<T>(false);
            if (subclassTag != null) {
                if (nameAttribute.ContainerName == keyword) {
                    subclassTag = subclassTag.GetContainingElement<T>(false);
                }
            }
            return subclassTag;
        }
    }
}