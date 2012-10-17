using System;
using JetBrains.Application.Progress;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace NHibernatePlugin.Analysis.CSharp
{
    public class CSharpAnalysisDaemonStageProcess : IDaemonStageProcess
    {
        private readonly IDaemonProcess m_DaemonProcess;
        
        public CSharpAnalysisDaemonStageProcess(IDaemonProcess daemonProcess) {
            m_DaemonProcess = daemonProcess;
        }

        public void Execute(Action<DaemonStageResult> commiter) {
            // Getting PSI (AST) for the file being highlighted
            PsiManager manager = PsiManager.GetInstance(m_DaemonProcess.Solution);
            IFile file = manager.GetPsiFile(m_DaemonProcess.SourceFile, CSharpLanguage.Instance,
                new DocumentRange(m_DaemonProcess.SourceFile.Document, m_DaemonProcess.SourceFile.Document.DocumentRange)) as ICSharpFile;
            if (file == null)
            {
                return;
            }

            // Running visitor against the PSI
            CSharpAnalysisElementProcessor elementProcessor = new CSharpAnalysisElementProcessor(m_DaemonProcess);
            file.ProcessDescendants(elementProcessor);

            // Checking if the daemon is interrupted by user activity
            if (m_DaemonProcess.InterruptFlag)
            {
                throw new ProcessCancelledException();
            }

            commiter(new DaemonStageResult(elementProcessor.Highlightings));
        }

        public IDaemonProcess DaemonProcess {
            get { return m_DaemonProcess; }
        }
    }
}