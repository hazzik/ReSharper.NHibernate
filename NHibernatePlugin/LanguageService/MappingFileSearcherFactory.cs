using System;
using System.Collections.Generic;
using JetBrains.Application;
using JetBrains.ComponentModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.Util;
using NHibernatePlugin.Helper;

namespace NHibernatePlugin.LanguageService
{
    [ShellComponentInterface(ProgramConfigurations.ALL), ShellComponentImplementation]
    public class MappingFileSearcherFactory : ILanguageSpecificSearcherFactory, IShellComponent
    {
        private static readonly ILanguageSpecificSearcherFactory s_Instance = new MappingFileSearcherFactory();

        public void Init() {
            Logger.LogMessage("MappingFileSearcherFactory Init called");
        }

        public void Dispose() {
        }

        public ICollection<string> GetAllPossibleNames(IDeclaredElement element) {
            string elementName = element.ShortName;
            Logger.LogMessage("GetAllPossibleNames {0}/{1}", element.GetType(), elementName);
            Logger.LogMessage("  accessibility domain {0}", element.GetAccessibilityDomain());

            IFieldDeclaration fieldDeclaration = element as IFieldDeclaration;
            if (fieldDeclaration == null) {
                return new string[] { elementName };
            }

            Logger.LogMessage("  field declaration {0}", fieldDeclaration.DeclaredName);
            IList<string> result = new List<string>();
            Naming.AddOtherNames(result, element);
            return result.ToArray();
        }

        public bool IsUnnamed(IDeclaredElement element) {
            return false;
        }

        public ILanguageSpecificSearcher CreateConstructorSpecialReferenceSearcher(ICollection<IConstructor> constructors, FindResultConsumer consumer) {
            return null;
        }

        public ILanguageSpecificSearcher CreateMethodsReferencedByDelegateSearcher(IDelegate @delegate, FindResultConsumer consumer) {
            return null;
        }

        public ILanguageSpecificSearcher CreateReferenceSearcher(ICollection<IDeclaredElement> elements, FindResultConsumer consumer) {
            Logger.LogMessage("CreateReferenceSearcher called");
            return new MappingFileReferenceSearcher(elements, consumer);
        }

        public ILanguageSpecificSearcher CreateTextOccurenceSeacrher(ICollection<IDeclaredElement> elements, FindResultConsumer consumer) {
            return null;
        }

        public ILanguageSpecificSearcher CreateTextOccurenceSeacrher(string subject, FindResultConsumer consumer) {
            return null;
        }

        public ILanguageSpecificSearcher CreateLateBoundReferenceSearcher(ICollection<IDeclaredElement> elements, FindResultConsumer consumer) {
            return null;
        }

        public ILanguageSpecificSearcher CreateAnonymousTypeSearcher(IList<Pair<string, IType>> typeDescription, FindResultConsumer consumer) {
            return null;
        }

        public IDeclaredElement GetAnonymousTypeProperty(FindResultAnonymousType findResult, string propertyName) {
            return null;
        }

        public Pair<ICollection<IDeclaredElement>, Predicate<IReference>>? GetRelatedDeclaredElements(IDeclaredElement element) {
            return null;
        }

        public static ILanguageSpecificSearcherFactory Instance {
            get { return s_Instance; }
        }
    }
}