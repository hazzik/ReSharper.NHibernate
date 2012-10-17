using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Xml.Parsing;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Psi;

namespace NHibernatePlugin.LanguageService.Parser
{
    public class MappingFileElementFactory : XmlElementFactory
    {
        private readonly MappingFileElementTypes m_ElementTypes;
        public static readonly MappingFileElementFactory Instance = new MappingFileElementFactory();

        public MappingFileElementFactory(XmlTokenTypes tokenTypes, MappingFileElementTypes elementTypes)
            : base(HbmXmlLanguage.Instance, tokenTypes, elementTypes) {
            m_ElementTypes = elementTypes;
        }

        public override IXmlFile CreateFile() {
            Logger.LogMessage("CreateFile");
            return new MappingFile(m_ElementTypes.FILE);
        }

        public override IXmlTag CreateRootTag(IXmlTagHeader header, IXmlElementFactoryContext context) {
            Logger.LogMessage("CreateRootTag {0}: {1}", header.Name.XmlName, header.GetText());
            if (header.Name.XmlName == Keyword.HibernateMapping) {
                return new HibernateMappingTag(m_ElementTypes.HIBERNATE_MAPPING);
            }

            return new MiscellaneousTag(m_ElementTypes.MISCELLANEOUS);
        }

        public override IXmlTag CreateTag(IXmlTagHeader header, IXmlTag parentTag, IXmlElementFactoryContext context) {
            Logger.LogMessage("CreateTag {0}: {1}", header.Name.XmlName, header.GetText());

            string name = header.Name.XmlName;

            switch (name) {
                case Keyword.HibernateMapping:
                    return new HibernateMappingTag(m_ElementTypes.HIBERNATE_MAPPING);
                case Keyword.Class:
                    return new ClassTag(m_ElementTypes.CLASS);
                case Keyword.Id:
                    return new IdTag(m_ElementTypes.ID);
                case Keyword.Property:
                    return new PropertyTag(m_ElementTypes.PROPERTY);
                case Keyword.Bag:
                    return new BagTag(m_ElementTypes.BAG);
                case Keyword.IdBag:
                    return new IdBagTag(m_ElementTypes.IDBAG);
                case Keyword.Set:
                    return new SetTag(m_ElementTypes.SET);
                case Keyword.Array:
                    return new ArrayTag(m_ElementTypes.ARRAY);
                case Keyword.PrimitiveArray:
                    return new PrimitiveArrayTag(m_ElementTypes.PRIMITIVEARRAY);
                case Keyword.List:
                    return new ListTag(m_ElementTypes.LIST);
                case Keyword.Any:
                    return new AnyTag(m_ElementTypes.ANY);
                case Keyword.Map:
                    return new MapTag(m_ElementTypes.MAP);
                case Keyword.Component:
                    return new ComponentTag(m_ElementTypes.COMPONENT);
                case Keyword.DynamicComponent:
                    return new DynamicComponentTag(m_ElementTypes.DYNAMICCOMPONENT);
                case Keyword.Subclass:
                    return new SubclassTag(m_ElementTypes.SUBCLASS);
                case Keyword.JoinedSubclass:
                    return new JoinedSubclassTag(m_ElementTypes.JOINEDSUBCLASS);
                case Keyword.OneToOne:
                    return new OneToOneTag(m_ElementTypes.ONETOONE);
                case Keyword.OneToMany:
                    return new OneToManyTag(m_ElementTypes.ONETOMANY);
                case Keyword.ManyToMany:
                    return new ManyToManyTag(m_ElementTypes.MANYTOMANY);
                case Keyword.ManyToAny:
                    return new ManyToAnyTag(m_ElementTypes.MANYTOANY);
                case Keyword.ManyToOne:
                    return new ManyToOneTag(m_ElementTypes.MANYTOONE);
                case Keyword.CompositeElement:
                    return new CompositeElementTag(m_ElementTypes.COMPOSITE_ELEMENT);
                case Keyword.NestedCompositeElement:
                    return new NestedCompositeElementTag(m_ElementTypes.NESTED_COMPOSITE_ELEMENT);
                case Keyword.Parent:
                    return new ParentTag(m_ElementTypes.PARENT);
            }

            return base.CreateTag(header, parentTag, context);
        }

        public override IXmlAttribute CreateAttribute(IXmlIdentifier nameIdentifier, IXmlAttributeContainer attributeContainer, IXmlTagContainer parentTag, IXmlElementFactoryContext context) {
            string attributeName = nameIdentifier.GetText();
            string containerName = attributeContainer.ContainerName;
            if (attributeName == Keyword.Name) {
                switch (containerName) {
                    case Keyword.Id:
                        return new PropertyNameAttribute(containerName, m_ElementTypes.PROPERTY_NAME_ATTRIBUTE);
                    case Keyword.Property:
                        return new PropertyNameAttribute(containerName, m_ElementTypes.PROPERTY_NAME_ATTRIBUTE);
                    case Keyword.Component:
                        return new PropertyNameAttribute(containerName, m_ElementTypes.PROPERTY_NAME_ATTRIBUTE);
                    case Keyword.DynamicComponent:
                        return new PropertyNameAttribute(containerName, m_ElementTypes.PROPERTY_NAME_ATTRIBUTE);
                    case Keyword.Bag:
                        return new PropertyNameAttribute(containerName, m_ElementTypes.PROPERTY_NAME_ATTRIBUTE);
                    case Keyword.Set:
                        return new PropertyNameAttribute(containerName, m_ElementTypes.PROPERTY_NAME_ATTRIBUTE);
                    case Keyword.IdBag:
                        return new PropertyNameAttribute(containerName, m_ElementTypes.PROPERTY_NAME_ATTRIBUTE);
                    case Keyword.Array:
                        return new PropertyNameAttribute(containerName, m_ElementTypes.PROPERTY_NAME_ATTRIBUTE);
                    case Keyword.PrimitiveArray:
                        return new PropertyNameAttribute(containerName, m_ElementTypes.PROPERTY_NAME_ATTRIBUTE);
                    case Keyword.List:
                        return new PropertyNameAttribute(containerName, m_ElementTypes.PROPERTY_NAME_ATTRIBUTE);
                    case Keyword.Any:
                        return new PropertyNameAttribute(containerName, m_ElementTypes.PROPERTY_NAME_ATTRIBUTE);
                    case Keyword.ManyToOne:
                        return new PropertyNameAttribute(containerName, m_ElementTypes.PROPERTY_NAME_ATTRIBUTE);
                    case Keyword.OneToOne:
                        return new PropertyNameAttribute(containerName, m_ElementTypes.PROPERTY_NAME_ATTRIBUTE);
                    case Keyword.Parent:
                        return new PropertyNameAttribute(containerName, m_ElementTypes.PROPERTY_NAME_ATTRIBUTE);
                    case Keyword.NestedCompositeElement:
                        return new PropertyNameAttribute(containerName, m_ElementTypes.PROPERTY_NAME_ATTRIBUTE);
                    case Keyword.Class:
                        return new ClassNameAttribute(containerName, m_ElementTypes.CLASS_NAME_ATTRIBUTE);
                }
            }
            if ((attributeName == Keyword.Type) || (attributeName == Keyword.Class)) {
                return new ClassNameAttribute(containerName, m_ElementTypes.CLASS_NAME_ATTRIBUTE);
            }
            return base.CreateAttribute(nameIdentifier, attributeContainer, parentTag, context);
        }
    }
}