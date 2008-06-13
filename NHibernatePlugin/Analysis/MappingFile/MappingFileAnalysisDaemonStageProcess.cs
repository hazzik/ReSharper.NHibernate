using JetBrains.Application.Progress;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.Analysis.MappingFile
{
    public class MappingFileAnalysisDaemonStageProcess : IDaemonStageProcess
    {
        private readonly IDaemonProcess m_DaemonProcess;

        public MappingFileAnalysisDaemonStageProcess(IDaemonProcess daemonProcess) {
            m_DaemonProcess = daemonProcess;
        }

        public DaemonStageProcessResult Execute() {
            Logger.LogMessage("NHibernatePlugin: MappingFileAnalysisDaemonStageProcess.Execute called");
            DaemonStageProcessResult result = new DaemonStageProcessResult();

            IFile file = PsiManager.PsiFile(m_DaemonProcess.ProjectFile);
            if (file == null) {
                Logger.LogMessage("   NO PSI FILE !!! {0}", m_DaemonProcess.ProjectFile.Name);
                return result;
            }

            MappingFileAnalysisElementProcessor elementProcessor = new MappingFileAnalysisElementProcessor(m_DaemonProcess);
            file.ProcessDescendants(elementProcessor);

            if (m_DaemonProcess.InterruptFlag) {
                throw new ProcessCancelledException();
            }

            result.FullyRehighlighted = true;
            result.Highlightings = elementProcessor.Highlightings.ToArray();

            return result;
        }
    }
}