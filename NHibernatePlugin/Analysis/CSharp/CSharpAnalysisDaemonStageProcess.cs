using JetBrains.Application.Progress;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace NHibernatePlugin.Analysis.CSharp
{
    public class CSharpAnalysisDaemonStageProcess : IDaemonStageProcess
    {
        private readonly IDaemonProcess m_DaemonProcess;
        
        public CSharpAnalysisDaemonStageProcess(IDaemonProcess daemonProcess) {
            m_DaemonProcess = daemonProcess;
        }

        public DaemonStageProcessResult Execute() {
            // Creating container to put highlightings to
            DaemonStageProcessResult result = new DaemonStageProcessResult();

            // Getting PSI (AST) for the file being highlighted
            PsiManager manager = PsiManager.GetInstance(m_DaemonProcess.Solution);
            IFile file = manager.GetPsiFile(m_DaemonProcess.ProjectFile);
            if (file == null) {
                return result;
            }

            // Running visitor against the PSI
            CSharpAnalysisElementProcessor elementProcessor = new CSharpAnalysisElementProcessor(m_DaemonProcess);
            file.ProcessDescendants(elementProcessor);

            // Checking if the daemon is interrupted by user activity
            if (m_DaemonProcess.InterruptFlag) {
                throw new ProcessCancelledException();
            }

            // Fill in the result
            result.FullyRehighlighted = true;
            result.Highlightings = elementProcessor.Highlightings.ToArray();

            return result;
        }
    }
}