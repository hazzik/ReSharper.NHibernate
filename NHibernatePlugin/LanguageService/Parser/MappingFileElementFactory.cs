using JetBrains.ReSharper.Psi.Xml.Parsing;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Psi;

namespace NHibernatePlugin.LanguageService.Parser
{
    public class MappingFileElementFactory : XmlElementFactory
    {
        public static readonly MappingFileElementFactory Instance = new MappingFileElementFactory();

        public MappingFileElementFactory()
            : base(MappingFileLanguageService.MAPPING_FILE) {
        }

        public override IXmlFile CreateFile() {
            Logger.LogMessage("CreateFile");
            return new MappingFile();
        }

        public override IXmlTag CreateRootTag(IXmlTagHeaderNode header) {
            Logger.LogMessage("CreateRootTag {0}: {1}", header.Name.XmlName, header.GetText());
            if (header.Name.XmlName == Keyword.HibernateMapping) {
                return new HibernateMappingTag();
            }

            return new MiscellaneousTag();
        }

        public override IXmlTag CreateTag(IXmlTagHeaderNode header, IXmlTag parentTag) {
            Logger.LogMessage("CreateTag {0}: {1}", header.Name.XmlName, header.GetText());

            string name = header.Name.XmlName;

            switch (name) {
                case Keyword.HibernateMapping:
                    return new HibernateMappingTag();
                case Keyword.Class:
                    return new ClassTag();
                case Keyword.Id:
                    return new IdTag();
                case Keyword.Property:
                    return new PropertyTag();
                case Keyword.Bag:
                    return new BagTag();
                case Keyword.IdBag:
                    return new IdBagTag();
                case Keyword.Set:
                    return new SetTag();
                case Keyword.Array:
                    return new ArrayTag();
                case Keyword.PrimitiveArray:
                    return new PrimitiveArrayTag();
                case Keyword.List:
                    return new ListTag();
                case Keyword.Any:
                    return new AnyTag();
                case Keyword.Map:
                    return new MapTag();
                case Keyword.Component:
                    return new ComponentTag();
                case Keyword.DynamicComponent:
                    return new DynamicComponentTag();
                case Keyword.Subclass:
                    return new SubclassTag();
                case Keyword.JoinedSubclass:
                    return new JoinedSubclassTag();
                case Keyword.OneToOne:
                    return new OneToOneTag();
                case Keyword.OneToMany:
                    return new OneToManyTag();
                case Keyword.ManyToMany:
                    return new ManyToManyTag();
                case Keyword.ManyToAny:
                    return new ManyToAnyTag();
                case Keyword.ManyToOne:
                    return new ManyToOneTag();
                case Keyword.CompositeElement:
                    return new CompositeElementTag();
                case Keyword.NestedCompositeElement:
                    return new NestedCompositeElementTag();
                case Keyword.Parent:
                    return new ParentTag();
            }

            return base.CreateTag(header, parentTag);
        }

        public override IXmlAttribute CreateAttribute(IXmlIdentifierNode nameIdentifier, IXmlAttributeContainer attributeContainer, IXmlTagContainer parentTag) {
            string attributeName = nameIdentifier.GetText();
            string containerName = attributeContainer.ContainerName;
            if (attributeName != Keyword.Name) {
                return base.CreateAttribute(nameIdentifier, attributeContainer, parentTag);
            }
            switch (containerName) {
                case Keyword.Id:
                    return new NameAttribute(containerName);
                case Keyword.Property:
                    return new NameAttribute(containerName);
                case Keyword.Component:
                    return new NameAttribute(containerName);
                case Keyword.DynamicComponent:
                    return new NameAttribute(containerName);
                case Keyword.Bag:
                    return new NameAttribute(containerName);
                case Keyword.Set:
                    return new NameAttribute(containerName);
                case Keyword.IdBag:
                    return new NameAttribute(containerName);
                case Keyword.Array:
                    return new NameAttribute(containerName);
                case Keyword.PrimitiveArray:
                    return new NameAttribute(containerName);
                case Keyword.List:
                    return new NameAttribute(containerName);
                case Keyword.Any:
                    return new NameAttribute(containerName);
                case Keyword.ManyToOne:
                    return new NameAttribute(containerName);
                case Keyword.OneToOne:
                    return new NameAttribute(containerName);
                case Keyword.Parent:
                    return new NameAttribute(containerName);
                case Keyword.NestedCompositeElement:
                    return new NameAttribute(containerName);
            }
            return base.CreateAttribute(nameIdentifier, attributeContainer, parentTag);
        }
    }
}