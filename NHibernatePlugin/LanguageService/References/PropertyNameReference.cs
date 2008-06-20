using System.Collections.Generic;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Xml.Impl.Tree.References;
using JetBrains.ReSharper.Psi.Xml.Impl.Util;
using JetBrains.ReSharper.Psi.Xml.Parsing;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;
using NHibernatePlugin.LanguageService.Psi;

namespace NHibernatePlugin.LanguageService.References
{
    public class PropertyNameReference : XmlReferenceWithTokenBase<PropertyNameAttribute>
    {
        public PropertyNameReference(PropertyNameAttribute owner, IXmlTokenNode token, TextRange rangeWithin)
            : base(owner, token, rangeWithin) {
        }

        public override bool HasMultipleNames {
            get { return true; }
        }

        protected override XmlElementFactory ElementFactory {
            get { return MappingFileElementFactory.Instance; }
        }

        public override string[] GetAllNames() {
            Logger.LogMessage("GetAllNames for {0}/{1}", GetElement().GetType(), GetElement().GetText());
            PropertyNameAttribute propertyNameAttribute = GetElement() as PropertyNameAttribute;
            if ((propertyNameAttribute == null) || (propertyNameAttribute.UnquotedValue == null)) {
                return base.GetAllNames();
            }
            ComponentTag parentComponent = PsiUtils.ParentComponent(propertyNameAttribute);
            if (parentComponent != null) {
                return GetAllNames(parentComponent, propertyNameAttribute, "class");
            }
            NestedCompositeElementTag parentNestedComposite = PsiUtils.ParentNestedComposite(propertyNameAttribute);
            if (parentNestedComposite != null) {
                return GetAllNames(parentNestedComposite, propertyNameAttribute, "class");
            }
            CompositeElementTag parentComposite = PsiUtils.ParentComposite(propertyNameAttribute);
            if (parentComposite != null) {
                return GetAllNames(parentComposite, propertyNameAttribute, "class");
            }
            SubclassTag subclassTag = PsiUtils.ParentSubclass(propertyNameAttribute);
            if (subclassTag != null) {
                return GetAllNames(subclassTag, propertyNameAttribute, "name");
            }
            ClassTag parentClass = propertyNameAttribute.GetContainingElement<ClassTag>(true);
            return GetAllNames(parentClass, propertyNameAttribute, "name");
        }

        public override ISymbolTable GetReferenceSymbolTable(bool useReferenceName) {
            Logger.LogMessage("GetReferenceSymbolTable {0} for {1}/{2}", useReferenceName, GetElement().GetType(), GetElement().GetText());
            PropertyNameAttribute propertyNameAttribute = GetElement() as PropertyNameAttribute;
            if ((propertyNameAttribute == null) || (propertyNameAttribute.UnquotedValue == null)) {
                return EmptySymbolTable.INSTANCE;
            }
            ComponentTag parentComponent = PsiUtils.ParentComponent(propertyNameAttribute);
            if (parentComponent != null) {
                return GetReferenceSymbolTable(parentComponent, propertyNameAttribute, "class");
            }
            NestedCompositeElementTag parentNestedComposite = PsiUtils.ParentNestedComposite(propertyNameAttribute);
            if (parentNestedComposite != null) {
                return GetReferenceSymbolTable(parentNestedComposite, propertyNameAttribute, "class");
            }
            CompositeElementTag parentComposite = PsiUtils.ParentComposite(propertyNameAttribute);
            if (parentComposite != null) {
                return GetReferenceSymbolTable(parentComposite, propertyNameAttribute, "class");
            }
            SubclassTag subclassTag = PsiUtils.ParentSubclass(propertyNameAttribute);
            if (subclassTag != null) {
                return GetReferenceSymbolTable(subclassTag, propertyNameAttribute, "name");
            }
            ClassTag parentClass = propertyNameAttribute.GetContainingElement<ClassTag>(true);
            return GetReferenceSymbolTable(parentClass, propertyNameAttribute, "name");
        }

        private string[] GetAllNames(IXmlTag containingElement, PropertyNameAttribute propertyNameAttribute, string attributeName) {
            IList<string> result = new List<string>();
            IField field = PsiUtils.GetField(GetProject().GetSolution(), propertyNameAttribute, attributeName, containingElement);
            if (field != null) {
                Logger.LogMessage("  field found: {0}", field.ShortName);
                result.Add(field.ShortName);
            }
            IProperty property = PsiUtils.GetProperty(GetProject().GetSolution(), propertyNameAttribute, attributeName, containingElement);
            if (property != null) {
                Logger.LogMessage("  property found: {0}", property.ShortName);
                result.Add(property.ShortName);
            }
            return result.ToArray();
        }

        private ISymbolTable GetReferenceSymbolTable(IXmlTag containingElement, PropertyNameAttribute propertyNameAttribute, string attributeName) {
            SymbolTable symbolTable = new SymbolTable(true);
            IField field = PsiUtils.GetField(GetProject().GetSolution(), propertyNameAttribute, attributeName, containingElement);
            if (field != null) {
                Logger.LogMessage("  field found: {0}", field.ShortName);
                symbolTable.AddSymbol(propertyNameAttribute.UnquotedValue, field, field.IdSubstitution, null, 1);
            }
            
            IProperty property = PsiUtils.GetProperty(GetProject().GetSolution(), propertyNameAttribute, attributeName, containingElement);
            if (property != null) {
                Logger.LogMessage("  property found: {0}", property.ShortName);
                symbolTable.AddSymbol(property, property.IdSubstitution, null, 1);
            }

            return symbolTable;
        }

        protected override IReference BindToInternal(IDeclaredElement element) {
            string name = element.ShortName
                .TrimFromStart("_")
                .TrimFromStart("m_");

            return ReferenceWithTokenUtil.SetText(this, name, ElementFactory);
        }
    }
}