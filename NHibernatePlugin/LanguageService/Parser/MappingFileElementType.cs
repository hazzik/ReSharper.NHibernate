using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Psi;

namespace NHibernatePlugin.LanguageService.Parser
{
    public static class MappingFileElementType
    {
        public static readonly XmlCompositeNodeType FILE = new FILE_TYPE();
        public static readonly XmlCompositeNodeType HIBERNATE_MAPPING = new HIBERNATE_MAPPING_TYPE();
        public static readonly XmlCompositeNodeType CLASS = new CLASS_TYPE();
        public static readonly XmlCompositeNodeType ID = new ID_TYPE();
        public static readonly XmlCompositeNodeType PROPERTY = new PROPERTY_TYPE();
        public static readonly XmlCompositeNodeType BAG = new BAG_TYPE();
        public static readonly XmlCompositeNodeType IDBAG = new IDBAG_TYPE();
        public static readonly XmlCompositeNodeType SET = new SET_TYPE();
        public static readonly XmlCompositeNodeType LIST = new LIST_TYPE();
        public static readonly XmlCompositeNodeType ANY = new ANY_TYPE();
        public static readonly XmlCompositeNodeType MAP = new MAP_TYPE();
        public static readonly XmlCompositeNodeType ARRAY = new ARRAY_TYPE();
        public static readonly XmlCompositeNodeType PRIMITIVEARRAY = new PRIMITIVEARRAY_TYPE();
        public static readonly XmlCompositeNodeType COMPONENT = new COMPONENT_TYPE();
        public static readonly XmlCompositeNodeType DYNAMICCOMPONENT = new DYNAMICCOMPONENT_TYPE();
        public static readonly XmlCompositeNodeType SUBCLASS = new SUBCLASS_TYPE();
        public static readonly XmlCompositeNodeType JOINEDSUBCLASS = new JOINEDSUBCLASS_TYPE();
        public static readonly XmlCompositeNodeType ONETOONE = new ONETOONE_TYPE();
        public static readonly XmlCompositeNodeType ONETOMANY = new ONETOMANY_TYPE();
        public static readonly XmlCompositeNodeType MANYTOMANY = new MANYTOMANY_TYPE();
        public static readonly XmlCompositeNodeType MANYTOANY = new MANYTOANY_TYPE();
        public static readonly XmlCompositeNodeType MANYTOONE = new MANYTOONE_TYPE();
        public static readonly XmlCompositeNodeType COMPOSITE_ELEMENT = new COMPOSITE_ELEMENT_TYPE();
        public static readonly XmlCompositeNodeType NESTED_COMPOSITE_ELEMENT = new NESTED_COMPOSITE_ELEMENT_TYPE();
        public static readonly XmlCompositeNodeType PARENT = new PARENT_TYPE();
        
        public static readonly XmlCompositeNodeType MISCELLANEOUS = new MISCELLANEOUS_TYPE();

        public static readonly XmlCompositeNodeType PROPERTY_NAME_ATTRIBUTE = new PROPERTY_NAME_ATTRIBUTE_TYPE();
        public static readonly XmlCompositeNodeType CLASS_NAME_ATTRIBUTE = new CLASS_NAME_ATTRIBUTE_TYPE();

        private class FILE_TYPE : MappingFileCompositeNodeType
        {
            public FILE_TYPE()
                : base("FILE") {
            }

            public override CompositeElement Create() {
                return new FileTag();
            }
        }

        private class CLASS_TYPE : MappingFileCompositeNodeType
        {
            public CLASS_TYPE()
                : base("CLASS") {
            }

            public override CompositeElement Create() {
                return new ClassTag();
            }
        }

        private class ID_TYPE : MappingFileCompositeNodeType
        {
            public ID_TYPE()
                : base("ID") {
            }

            public override CompositeElement Create() {
                return new IdTag();
            }
        }

        private class HIBERNATE_MAPPING_TYPE : MappingFileCompositeNodeType
        {
            public HIBERNATE_MAPPING_TYPE()
                : base("HIBERNATE_MAPPING") {
            }

            public override CompositeElement Create() {
                return new HibernateMappingTag();
            }
        }

        private class PROPERTY_TYPE : MappingFileCompositeNodeType
        {
            public PROPERTY_TYPE()
                : base("PROPERTY") {
            }

            public override CompositeElement Create() {
                return new PropertyTag();
            }
        }

        private class BAG_TYPE : MappingFileCompositeNodeType
        {
            public BAG_TYPE()
                : base("BAG") {
            }

            public override CompositeElement Create() {
                return new BagTag();
            }
        }

        private class IDBAG_TYPE : MappingFileCompositeNodeType
        {
            public IDBAG_TYPE()
                : base("IDBAG") {
            }

            public override CompositeElement Create() {
                return new IdBagTag();
            }
        }

        private class LIST_TYPE : MappingFileCompositeNodeType
        {
            public LIST_TYPE()
                : base("LIST") {
            }

            public override CompositeElement Create() {
                return new ListTag();
            }
        }

        private class ANY_TYPE : MappingFileCompositeNodeType
        {
            public ANY_TYPE()
                : base("ANY") {
            }

            public override CompositeElement Create() {
                return new AnyTag();
            }
        }

        private class MAP_TYPE : MappingFileCompositeNodeType
        {
            public MAP_TYPE()
                : base("MAP") {
            }

            public override CompositeElement Create() {
                return new MapTag();
            }
        }

        private class ARRAY_TYPE : MappingFileCompositeNodeType
        {
            public ARRAY_TYPE()
                : base("ARRAY") {
            }

            public override CompositeElement Create() {
                return new ArrayTag();
            }
        }

        private class PRIMITIVEARRAY_TYPE : MappingFileCompositeNodeType
        {
            public PRIMITIVEARRAY_TYPE()
                : base("PRIMITIVEARRAY") {
            }

            public override CompositeElement Create() {
                return new PrimitiveArrayTag();
            }
        }

        private class SET_TYPE : MappingFileCompositeNodeType
        {
            public SET_TYPE()
                : base("SET") {
            }

            public override CompositeElement Create() {
                return new SetTag();
            }
        }

        private class COMPONENT_TYPE : MappingFileCompositeNodeType
        {
            public COMPONENT_TYPE()
                : base("COMPONENT") {
            }

            public override CompositeElement Create() {
                return new ComponentTag();
            }
        }

        private class DYNAMICCOMPONENT_TYPE : MappingFileCompositeNodeType
        {
            public DYNAMICCOMPONENT_TYPE()
                : base("DYNAMICCOMPONENT") {
            }

            public override CompositeElement Create() {
                return new DynamicComponentTag();
            }
        }

        private class SUBCLASS_TYPE : MappingFileCompositeNodeType
        {
            public SUBCLASS_TYPE()
                : base("SUBCLASS") {
            }

            public override CompositeElement Create() {
                return new SubclassTag();
            }
        }

        private class JOINEDSUBCLASS_TYPE : MappingFileCompositeNodeType
        {
            public JOINEDSUBCLASS_TYPE()
                : base("JOINEDSUBCLASS") {
            }

            public override CompositeElement Create() {
                return new JoinedSubclassTag();
            }
        }

        private class ONETOONE_TYPE : MappingFileCompositeNodeType
        {
            public ONETOONE_TYPE()
                : base("ONETOONE") {
            }

            public override CompositeElement Create() {
                return new OneToOneTag();
            }
        }

        private class ONETOMANY_TYPE : MappingFileCompositeNodeType
        {
            public ONETOMANY_TYPE()
                : base("ONETOMANY") {
            }

            public override CompositeElement Create() {
                return new OneToManyTag();
            }
        }

        private class MANYTOMANY_TYPE : MappingFileCompositeNodeType
        {
            public MANYTOMANY_TYPE()
                : base("MANYTOMANY") {
            }

            public override CompositeElement Create() {
                return new ManyToManyTag();
            }
        }

        private class MANYTOANY_TYPE : MappingFileCompositeNodeType
        {
            public MANYTOANY_TYPE()
                : base("MANYTOANY") {
            }

            public override CompositeElement Create() {
                return new ManyToAnyTag();
            }
        }

        private class MANYTOONE_TYPE : MappingFileCompositeNodeType
        {
            public MANYTOONE_TYPE()
                : base("MANYTOONE") {
            }

            public override CompositeElement Create() {
                return new ManyToOneTag();
            }
        }

        private class COMPOSITE_ELEMENT_TYPE : MappingFileCompositeNodeType
        {
            public COMPOSITE_ELEMENT_TYPE()
                : base("COMPOSITE_ELEMENT") {
            }

            public override CompositeElement Create() {
                return new CompositeElementTag();
            }
        }

        private class NESTED_COMPOSITE_ELEMENT_TYPE : MappingFileCompositeNodeType
        {
            public NESTED_COMPOSITE_ELEMENT_TYPE()
                : base("NESTED_COMPOSITE_ELEMENT") {
            }

            public override CompositeElement Create() {
                return new NestedCompositeElementTag();
            }
        }

        private class PARENT_TYPE : MappingFileCompositeNodeType
        {
            public PARENT_TYPE()
                : base("PARENT") {
            }

            public override CompositeElement Create() {
                return new ParentTag();
            }
        }

        private class MISCELLANEOUS_TYPE : MappingFileCompositeNodeType
        {
            public MISCELLANEOUS_TYPE()
                : base("MISCELLANEOUS") {
            }

            public override CompositeElement Create() {
                return new MiscellaneousTag();
            }
        }

        private class PROPERTY_NAME_ATTRIBUTE_TYPE : MappingFileCompositeNodeType
        {
            public PROPERTY_NAME_ATTRIBUTE_TYPE()
                : base("PROPERTY_NAME_ATTRIBUTE") {
            }

            public override CompositeElement Create() {
                Logger.LogMessage("**** PROPERTY_NAME_ATTRIBUTE_TYPE.Create called");
                return new PropertyNameAttribute("");
            }
        }

        private class CLASS_NAME_ATTRIBUTE_TYPE : MappingFileCompositeNodeType
        {
            public CLASS_NAME_ATTRIBUTE_TYPE()
                : base("CLASS_NAME_ATTRIBUTE") {
            }

            public override CompositeElement Create() {
                Logger.LogMessage("**** CLASS_NAME_ATTRIBUTE_TYPE.Create called");
                return new ClassNameAttribute("");
            }
        }
    }
}