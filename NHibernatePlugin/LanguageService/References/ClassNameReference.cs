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
    public class ClassNameReference : XmlReferenceWithTokenBase<ClassNameAttribute>
    {
        public ClassNameReference(ClassNameAttribute owner, IXmlTokenNode token, TextRange rangeWithin)
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
            ClassNameAttribute classNameAttribute = GetElement() as ClassNameAttribute;
            if ((classNameAttribute == null) || (classNameAttribute.UnquotedValue == null)) {
                return base.GetAllNames();
            }
            return GetAllNames(classNameAttribute);
        }

        public override ISymbolTable GetReferenceSymbolTable(bool useReferenceName) {
            Logger.LogMessage("GetReferenceSymbolTable {0} for {1}/{2}", useReferenceName, GetElement().GetType(), GetElement().GetText());
            ClassNameAttribute classNameAttribute = GetElement() as ClassNameAttribute;
            if ((classNameAttribute == null) || (classNameAttribute.UnquotedValue == null)) {
                return EmptySymbolTable.INSTANCE;
            }
            ComponentTag parentComponent = PsiUtils.ParentComponent(classNameAttribute);
            if (parentComponent != null) {
                return GetReferenceSymbolTable(parentComponent, classNameAttribute);
            }
            NestedCompositeElementTag parentNestedComposite = PsiUtils.ParentNestedComposite(classNameAttribute);
            if (parentNestedComposite != null) {
                return GetReferenceSymbolTable(parentNestedComposite, classNameAttribute);
            }
            CompositeElementTag parentComposite = PsiUtils.ParentComposite(classNameAttribute);
            if (parentComposite != null) {
                return GetReferenceSymbolTable(parentComposite, classNameAttribute);
            }
            SubclassTag subclassTag = PsiUtils.ParentSubclass(classNameAttribute);
            if (subclassTag != null) {
                return GetReferenceSymbolTable(subclassTag, classNameAttribute);
            }
            ClassTag parentClass = classNameAttribute.GetContainingElement<ClassTag>(true);
            return GetReferenceSymbolTable(parentClass, classNameAttribute);
        }

        private string[] GetAllNames(IXmlAttribute classNameAttribute) {
            IList<string> result = new List<string>();
            IXmlTag containingElement = classNameAttribute.GetContainingElement<IXmlTag>(true);
            ITypeElement typeElement = PsiUtils.GetTypeElement(containingElement, GetProject().GetSolution(), classNameAttribute.UnquotedValue);
            if (typeElement != null) {
                Logger.LogMessage("  type found: {0}", typeElement.ShortName);
                result.Add(typeElement.ShortName);
            }
            return result.ToArray();
        }

        private ISymbolTable GetReferenceSymbolTable(IXmlTag containingElement, IXmlAttribute classNameAttribute) {
            SymbolTable symbolTable = new SymbolTable(true);
            ITypeElement typeElement = PsiUtils.GetTypeElement(containingElement, GetProject().GetSolution(), classNameAttribute.UnquotedValue);
            if (typeElement != null) {
                Logger.LogMessage("  type found: {0}", typeElement.ShortName);
                symbolTable.AddSymbol(classNameAttribute.UnquotedValue, typeElement, typeElement.IdSubstitution, null, 0);
            }

            return symbolTable;
        }

        protected override IReference BindToInternal(IDeclaredElement element) {
            string name;
            IClass klass = element as IClass;
            if (klass != null) {
                ClassNameAttribute classNameAttribute = GetElement() as ClassNameAttribute;
                if ((classNameAttribute != null) && (classNameAttribute.UnquotedValue != null) &&
                        (classNameAttribute.UnquotedValue.Contains(".") || classNameAttribute.UnquotedValue.Contains(","))) {
                    name = klass.CLRName;
                    if (klass.Module != null) {
                        name += ", " + klass.Module.Name;
                    }
                }
                else {
                    name = klass.ShortName;
                }
            }
            else {
                name = element.ShortName;
            }
 
            return ReferenceWithTokenUtil.SetText(this, name, ElementFactory);
        }
        
    }
}