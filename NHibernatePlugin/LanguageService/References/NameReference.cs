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
    public class NameReference : XmlReferenceWithTokenBase<NameAttribute>
    {
        public NameReference(NameAttribute owner, IXmlTokenNode token, TextRange rangeWithin)
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
            NameAttribute nameAttribute = GetElement() as NameAttribute;
            if ((nameAttribute == null) || (nameAttribute.UnquotedValue == null)) {
                return base.GetAllNames();
            }
            ComponentTag parentComponent = PsiUtils.ParentComponent(nameAttribute);
            if (parentComponent != null) {
                return GetAllNames(parentComponent, nameAttribute, "class");
            }
            NestedCompositeElementTag parentNestedComposite = PsiUtils.ParentNestedComposite(nameAttribute);
            if (parentNestedComposite != null) {
                return GetAllNames(parentNestedComposite, nameAttribute, "class");
            }
            CompositeElementTag parentComposite = PsiUtils.ParentComposite(nameAttribute);
            if (parentComposite != null) {
                return GetAllNames(parentComposite, nameAttribute, "class");
            }
            SubclassTag subclassTag = PsiUtils.ParentSubclass(nameAttribute);
            if (subclassTag != null) {
                return GetAllNames(subclassTag, nameAttribute, "name");
            }
            ClassTag parentClass = nameAttribute.GetContainingElement<ClassTag>(true);
            return GetAllNames(parentClass, nameAttribute, "name");
        }

        public override ISymbolTable GetReferenceSymbolTable(bool useReferenceName) {
            Logger.LogMessage("GetReferenceSymbolTable {0} for {1}/{2}", useReferenceName, GetElement().GetType(), GetElement().GetText());
            NameAttribute nameAttribute = GetElement() as NameAttribute;
            if ((nameAttribute == null) || (nameAttribute.UnquotedValue == null)) {
                return EmptySymbolTable.INSTANCE;
            }
            ComponentTag parentComponent = PsiUtils.ParentComponent(nameAttribute);
            if (parentComponent != null) {
                return GetReferenceSymbolTable(parentComponent, nameAttribute, "class");
            }
            NestedCompositeElementTag parentNestedComposite = PsiUtils.ParentNestedComposite(nameAttribute);
            if (parentNestedComposite != null) {
                return GetReferenceSymbolTable(parentNestedComposite, nameAttribute, "class");
            }
            CompositeElementTag parentComposite = PsiUtils.ParentComposite(nameAttribute);
            if (parentComposite != null) {
                return GetReferenceSymbolTable(parentComposite, nameAttribute, "class");
            }
            SubclassTag subclassTag = PsiUtils.ParentSubclass(nameAttribute);
            if (subclassTag != null) {
                return GetReferenceSymbolTable(subclassTag, nameAttribute, "name");
            }
            ClassTag parentClass = nameAttribute.GetContainingElement<ClassTag>(true);
            return GetReferenceSymbolTable(parentClass, nameAttribute, "name");
        }

        private string[] GetAllNames(IXmlTag containingElement, NameAttribute nameAttribute, string attributeName) {
            IList<string> result = new List<string>();
            IField field = PsiUtils.GetField(GetProject().GetSolution(), nameAttribute, attributeName, containingElement);
            if (field != null) {
                Logger.LogMessage("  field found: {0}", field.ShortName);
                result.Add(field.ShortName);
            }
            IProperty property = PsiUtils.GetProperty(GetProject().GetSolution(), nameAttribute, attributeName, containingElement);
            if (property != null) {
                Logger.LogMessage("  property found: {0}", property.ShortName);
                result.Add(property.ShortName);
            }
            ITypeElement typeElement = PsiUtils.GetTypeElement(GetProject().GetSolution(), nameAttribute.UnquotedValue, "", "");
            if (typeElement != null) {
                Logger.LogMessage("  type found: {0}", typeElement.ShortName);
                result.Add(typeElement.ShortName);
            }
            return result.ToArray();
        }

        private ISymbolTable GetReferenceSymbolTable(IXmlTag containingElement, NameAttribute nameAttribute, string attributeName) {
            SymbolTable symbolTable = new SymbolTable(true);
            IField field = PsiUtils.GetField(GetProject().GetSolution(), nameAttribute, attributeName, containingElement);
            if (field != null) {
                Logger.LogMessage("  field found: {0}", field.ShortName);
                symbolTable.AddSymbol(nameAttribute.UnquotedValue, field, field.IdSubstitution, null, 1);
            }
            
            IProperty property = PsiUtils.GetProperty(GetProject().GetSolution(), nameAttribute, attributeName, containingElement);
            if (property != null) {
                Logger.LogMessage("  property found: {0}", property.ShortName);
                symbolTable.AddSymbol(property, property.IdSubstitution, null, 1);
            }
            ITypeElement typeElement = PsiUtils.GetTypeElement(GetProject().GetSolution(), nameAttribute.UnquotedValue, "", "");
            if (typeElement != null) {
                Logger.LogMessage("  type found: {0}", typeElement.ShortName);
                symbolTable.AddSymbol(nameAttribute.UnquotedValue, typeElement, typeElement.IdSubstitution, null, 0);
            }

            return symbolTable;
        }

        protected override IReference BindToInternal(IDeclaredElement element) {
            string name;
            IClass klass = element as IClass;
            if (klass != null) {
                name = klass.CLRName;
                if (klass.Module != null) {
                    name += ", " + klass.Module.Name;
                }
            }
            else {
                name = element.ShortName
                    .TrimFromStart("_")
                    .TrimFromStart("m_");
            }
 
            return ReferenceWithTokenUtil.SetText(this, name, ElementFactory);
        }
    }
}