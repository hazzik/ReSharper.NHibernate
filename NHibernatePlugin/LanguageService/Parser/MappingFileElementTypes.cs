using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Psi;

namespace NHibernatePlugin.LanguageService.Parser
{
    [Language(typeof(HbmXmlLanguage))]
    public class MappingFileElementTypes : XmlElementTypes
    {
        public readonly XmlCompositeNodeType FILE;
        public readonly XmlCompositeNodeType HIBERNATE_MAPPING;
        public readonly XmlCompositeNodeType CLASS;
        public readonly XmlCompositeNodeType ID;
        public readonly XmlCompositeNodeType PROPERTY;
        public readonly XmlCompositeNodeType BAG;
        public readonly XmlCompositeNodeType IDBAG;
        public readonly XmlCompositeNodeType SET;
        public readonly XmlCompositeNodeType LIST;
        public readonly XmlCompositeNodeType ANY;
        public readonly XmlCompositeNodeType MAP;
        public readonly XmlCompositeNodeType ARRAY;
        public readonly XmlCompositeNodeType PRIMITIVEARRAY;
        public readonly XmlCompositeNodeType COMPONENT;
        public readonly XmlCompositeNodeType DYNAMICCOMPONENT;
        public readonly XmlCompositeNodeType SUBCLASS;
        public readonly XmlCompositeNodeType JOINEDSUBCLASS;
        public readonly XmlCompositeNodeType ONETOONE;
        public readonly XmlCompositeNodeType ONETOMANY;
        public readonly XmlCompositeNodeType MANYTOMANY;
        public readonly XmlCompositeNodeType MANYTOANY;
        public readonly XmlCompositeNodeType MANYTOONE;
        public readonly XmlCompositeNodeType COMPOSITE_ELEMENT;
        public readonly XmlCompositeNodeType NESTED_COMPOSITE_ELEMENT;
        public readonly XmlCompositeNodeType PARENT;

        public readonly XmlCompositeNodeType MISCELLANEOUS;

        public readonly XmlCompositeNodeType PROPERTY_NAME_ATTRIBUTE;
        public readonly XmlCompositeNodeType CLASS_NAME_ATTRIBUTE;

        private class FILE_TYPE : MappingFileCompositeNodeType
        {
            public FILE_TYPE(MappingFileElementTypes elementTypes)
                : base("FILE", elementTypes) {
            }

            public override CompositeElement Create() {
                return new FileTag(this);
            }
        }

        private class CLASS_TYPE : MappingFileCompositeNodeType
        {
            public CLASS_TYPE(MappingFileElementTypes elementTypes)
                : base("CLASS", elementTypes) {
            }

            public override CompositeElement Create() {
                return new ClassTag(this);
            }
        }

        private class ID_TYPE : MappingFileCompositeNodeType
        {
            public ID_TYPE(MappingFileElementTypes elementTypes)
                : base("ID", elementTypes) {
            }

            public override CompositeElement Create() {
                return new IdTag(this);
            }
        }

        private class HIBERNATE_MAPPING_TYPE : MappingFileCompositeNodeType
        {
            public HIBERNATE_MAPPING_TYPE(MappingFileElementTypes elementTypes)
                : base("HIBERNATE_MAPPING", elementTypes) {
            }

            public override CompositeElement Create() {
                return new HibernateMappingTag(this);
            }
        }

        private class PROPERTY_TYPE : MappingFileCompositeNodeType
        {
            public PROPERTY_TYPE(MappingFileElementTypes elementTypes)
                : base("PROPERTY", elementTypes)
            {
            }

            public override CompositeElement Create() {
                return new PropertyTag(this);
            }
        }

        private class BAG_TYPE : MappingFileCompositeNodeType
        {
            public BAG_TYPE(MappingFileElementTypes elementTypes)
                : base("BAG", elementTypes)
            {
            }

            public override CompositeElement Create() {
                return new BagTag(this);
            }
        }

        private class IDBAG_TYPE : MappingFileCompositeNodeType
        {
            public IDBAG_TYPE(MappingFileElementTypes elementTypes)
                : base("IDBAG", elementTypes)
            {
            }

            public override CompositeElement Create() {
                return new IdBagTag(this);
            }
        }

        private class LIST_TYPE : MappingFileCompositeNodeType
        {
            public LIST_TYPE(MappingFileElementTypes elementTypes)
                : base("LIST", elementTypes) {
            }

            public override CompositeElement Create() {
                return new ListTag(this);
            }
        }

        private class ANY_TYPE : MappingFileCompositeNodeType
        {
            public ANY_TYPE(MappingFileElementTypes elementTypes)
                : base("ANY", elementTypes) {
            }

            public override CompositeElement Create() {
                return new AnyTag(this);
            }
        }

        private class MAP_TYPE : MappingFileCompositeNodeType
        {
            public MAP_TYPE(MappingFileElementTypes elementTypes)
                : base("MAP", elementTypes) {
            }

            public override CompositeElement Create() {
                return new MapTag(this);
            }
        }

        private class ARRAY_TYPE : MappingFileCompositeNodeType
        {
            public ARRAY_TYPE(MappingFileElementTypes elementTypes)
                : base("ARRAY", elementTypes) {
            }

            public override CompositeElement Create() {
                return new ArrayTag(this);
            }
        }

        private class PRIMITIVEARRAY_TYPE : MappingFileCompositeNodeType
        {
            public PRIMITIVEARRAY_TYPE(MappingFileElementTypes elementTypes)
                : base("PRIMITIVEARRAY", elementTypes) {
            }

            public override CompositeElement Create() {
                return new PrimitiveArrayTag(this);
            }
        }

        private class SET_TYPE : MappingFileCompositeNodeType
        {
            public SET_TYPE(MappingFileElementTypes elementTypes)
                : base("SET", elementTypes) {
            }

            public override CompositeElement Create() {
                return new SetTag(this);
            }
        }

        private class COMPONENT_TYPE : MappingFileCompositeNodeType
        {
            public COMPONENT_TYPE(MappingFileElementTypes elementTypes)
                : base("COMPONENT", elementTypes) {
            }

            public override CompositeElement Create() {
                return new ComponentTag(this);
            }
        }

        private class DYNAMICCOMPONENT_TYPE : MappingFileCompositeNodeType
        {
            public DYNAMICCOMPONENT_TYPE(MappingFileElementTypes elementTypes)
                : base("DYNAMICCOMPONENT", elementTypes) {
            }

            public override CompositeElement Create() {
                return new DynamicComponentTag(this);
            }
        }

        private class SUBCLASS_TYPE : MappingFileCompositeNodeType
        {
            public SUBCLASS_TYPE(MappingFileElementTypes elementTypes)
                : base("SUBCLASS", elementTypes) {
            }

            public override CompositeElement Create() {
                return new SubclassTag(this);
            }
        }

        private class JOINEDSUBCLASS_TYPE : MappingFileCompositeNodeType
        {
            public JOINEDSUBCLASS_TYPE(MappingFileElementTypes elementTypes)
                : base("JOINEDSUBCLASS", elementTypes) {
            }

            public override CompositeElement Create() {
                return new JoinedSubclassTag(this);
            }
        }

        private class ONETOONE_TYPE : MappingFileCompositeNodeType
        {
            public ONETOONE_TYPE(MappingFileElementTypes elementTypes)
                : base("ONETOONE", elementTypes) {
            }

            public override CompositeElement Create() {
                return new OneToOneTag(this);
            }
        }

        private class ONETOMANY_TYPE : MappingFileCompositeNodeType
        {
            public ONETOMANY_TYPE(MappingFileElementTypes elementTypes)
                : base("ONETOMANY", elementTypes) {
            }

            public override CompositeElement Create() {
                return new OneToManyTag(this);
            }
        }

        private class MANYTOMANY_TYPE : MappingFileCompositeNodeType
        {
            public MANYTOMANY_TYPE(MappingFileElementTypes elementTypes)
                : base("MANYTOMANY", elementTypes) {
            }

            public override CompositeElement Create() {
                return new ManyToManyTag(this);
            }
        }

        private class MANYTOANY_TYPE : MappingFileCompositeNodeType
        {
            public MANYTOANY_TYPE(MappingFileElementTypes elementTypes)
                : base("MANYTOANY", elementTypes) {
            }

            public override CompositeElement Create() {
                return new ManyToAnyTag(this);
            }
        }

        private class MANYTOONE_TYPE : MappingFileCompositeNodeType
        {
            public MANYTOONE_TYPE(MappingFileElementTypes elementTypes)
                : base("MANYTOONE", elementTypes) {
            }

            public override CompositeElement Create() {
                return new ManyToOneTag(this);
            }
        }

        private class COMPOSITE_ELEMENT_TYPE : MappingFileCompositeNodeType
        {
            public COMPOSITE_ELEMENT_TYPE(MappingFileElementTypes elementTypes)
                : base("COMPOSITE_ELEMENT", elementTypes) {
            }

            public override CompositeElement Create() {
                return new CompositeElementTag(this);
            }
        }

        private class NESTED_COMPOSITE_ELEMENT_TYPE : MappingFileCompositeNodeType
        {
            public NESTED_COMPOSITE_ELEMENT_TYPE(MappingFileElementTypes elementTypes)
                : base("NESTED_COMPOSITE_ELEMENT", elementTypes) {
            }

            public override CompositeElement Create() {
                return new NestedCompositeElementTag(this);
            }
        }

        private class PARENT_TYPE : MappingFileCompositeNodeType
        {
            public PARENT_TYPE(MappingFileElementTypes elementTypes)
                : base("PARENT", elementTypes) {
            }

            public override CompositeElement Create() {
                return new ParentTag(this);
            }
        }

        private class MISCELLANEOUS_TYPE : MappingFileCompositeNodeType
        {
            public MISCELLANEOUS_TYPE(MappingFileElementTypes elementTypes)
                : base("MISCELLANEOUS", elementTypes) {
            }

            public override CompositeElement Create() {
                return new MiscellaneousTag(this);
            }
        }

        private class PROPERTY_NAME_ATTRIBUTE_TYPE : MappingFileCompositeNodeType
        {
            public PROPERTY_NAME_ATTRIBUTE_TYPE(MappingFileElementTypes elementTypes)
                : base("PROPERTY_NAME_ATTRIBUTE", elementTypes) {
            }

            public override CompositeElement Create() {
                Logger.LogMessage("**** PROPERTY_NAME_ATTRIBUTE_TYPE.Create called");
                return new PropertyNameAttribute("", this);
            }
        }

        private class CLASS_NAME_ATTRIBUTE_TYPE : MappingFileCompositeNodeType
        {
            public CLASS_NAME_ATTRIBUTE_TYPE(MappingFileElementTypes elementTypes)
                : base("CLASS_NAME_ATTRIBUTE", elementTypes) {
            }

            public override CompositeElement Create() {
                Logger.LogMessage("**** CLASS_NAME_ATTRIBUTE_TYPE.Create called");
                return new ClassNameAttribute("", this);
            }
        }

        public MappingFileElementTypes(XmlTokenTypes xmlTokenTypes)
            : base(xmlTokenTypes) {
            CLASS_NAME_ATTRIBUTE = new CLASS_NAME_ATTRIBUTE_TYPE(this);
            PROPERTY_NAME_ATTRIBUTE = new PROPERTY_NAME_ATTRIBUTE_TYPE(this);
            MISCELLANEOUS = new MISCELLANEOUS_TYPE(this);
            PARENT = new PARENT_TYPE(this);
            NESTED_COMPOSITE_ELEMENT = new NESTED_COMPOSITE_ELEMENT_TYPE(this);
            COMPOSITE_ELEMENT = new COMPOSITE_ELEMENT_TYPE(this);
            MANYTOONE = new MANYTOONE_TYPE(this);
            MANYTOANY = new MANYTOANY_TYPE(this);
            MANYTOMANY = new MANYTOMANY_TYPE(this);
            ONETOMANY = new ONETOMANY_TYPE(this);
            ONETOONE = new ONETOONE_TYPE(this);
            JOINEDSUBCLASS = new JOINEDSUBCLASS_TYPE(this);
            SUBCLASS = new SUBCLASS_TYPE(this);
            DYNAMICCOMPONENT = new DYNAMICCOMPONENT_TYPE(this);
            COMPONENT = new COMPONENT_TYPE(this);
            PRIMITIVEARRAY = new PRIMITIVEARRAY_TYPE(this);
            ARRAY = new ARRAY_TYPE(this);
            MAP = new MAP_TYPE(this);
            ANY = new ANY_TYPE(this);
            LIST = new LIST_TYPE(this);
            SET = new SET_TYPE(this);
            IDBAG = new IDBAG_TYPE(this);
            BAG = new BAG_TYPE(this);
            PROPERTY = new PROPERTY_TYPE(this);
            ID = new ID_TYPE(this);
            CLASS = new CLASS_TYPE(this);
            HIBERNATE_MAPPING = new HIBERNATE_MAPPING_TYPE(this);
            FILE = new FILE_TYPE(this);
        }
    }
}