using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Finder;
using JetBrains.ReSharper.Psi.Search;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;
using NHibernatePlugin.Helper;

namespace NHibernatePlugin.LanguageService
{
    public class MappingFileReferenceSearcher : IReferenceSearchContext, ILanguageSpecificSearcher
    {
        private readonly FindResultConsumer m_Consumer;
        private readonly HashSet<string> m_ElementNames = new HashSet<string>();
        private readonly string[] m_ElementNamesArray;
        private readonly HashSet<IDeclaredElement> m_Elements = new HashSet<IDeclaredElement>();

        public MappingFileReferenceSearcher(IEnumerable<IDeclaredElement> elements, FindResultConsumer consumer) {
            Logger.LogMessage("MappingFileReferenceSearcher ctor");
            m_Consumer = consumer;
            foreach (IDeclaredElement element in elements) {
                m_Elements.Add(element);
                m_ElementNames.Add(element.ShortName);
                Logger.LogMessage("  Accessability domain {0}", element.GetAccessibilityDomain());
                Logger.LogMessage("  ShortName {0}/{1}", element.GetType(), element.ShortName);

                Naming.AddOtherNames(m_ElementNames, element);
            }
            m_ElementNamesArray = m_ElementNames.ToArray();
        }

        public IList<string> GetNamesToSearchForInProjectFile(IProjectFile item) {
            return m_ElementNamesArray;
        }

        public bool ProcessElement(IElement element) {
            return (new ReferenceSearchSourceFileProcessor(this, element.ToTreeNode()).Run() == FindExecution.Stop);
        }

        public bool ProcessProjectItem(IProjectFile projectItem) {
            IFile psiFile = PsiManager.GetInstance(projectItem.GetSolution()).GetPsiFile(projectItem);
            return ((psiFile != null) && ProcessElement(psiFile));
        }

        public HashSet<IDeclaredElement> Elements {
            get { return m_Elements; }
        }

        public bool HasUnnamedElement {
            get { return false; }
        }

        public FindResultConsumer ResultConsumer {
            get { return m_Consumer; }
        }
    }
}