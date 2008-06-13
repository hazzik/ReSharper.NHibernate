using System.Collections.Generic;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.Analysis.CSharp
{
    public class CSharpAnalysisElementProcessor : IRecursiveElementProcessor
    {
        private readonly IDaemonProcess m_Process;
        private readonly List<HighlightingInfo> m_Highlightings = new List<HighlightingInfo>();

        public CSharpAnalysisElementProcessor(IDaemonProcess myProcess) {
            m_Process = myProcess;
        }

        public bool InteriorShouldBeProcessed(IElement element) {
            return false;   // TODO: set to true if anything should processed !!
        }

        public void ProcessBeforeInterior(IElement element) {
            IPropertyDeclaration propertyDeclaration = element as IPropertyDeclaration;
            if (propertyDeclaration != null) {
                Logger.LogMessage("C# ProcessBeforeInterior Property {0}", propertyDeclaration.DeclaredName);
            }

            IFieldDeclaration fieldDeclaration = element as IFieldDeclaration;
            if (fieldDeclaration != null) {
                Logger.LogMessage("C# ProcessBeforeInterior Field {0}", fieldDeclaration.DeclaredName);
            }
        }

        public void ProcessAfterInterior(IElement element) {
        }

        public bool ProcessingIsFinished {
            get { return m_Process.InterruptFlag; }
        }

        public List<HighlightingInfo> Highlightings {
            get { return m_Highlightings; }
        }
    }
}