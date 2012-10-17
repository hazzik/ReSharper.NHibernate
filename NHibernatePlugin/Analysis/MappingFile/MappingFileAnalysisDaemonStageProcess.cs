using System;
using JetBrains.Application.Progress;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;
using NHibernatePlugin.LanguageService;

namespace NHibernatePlugin.Analysis.MappingFile
{
    public class MappingFileAnalysisDaemonStageProcess : IDaemonStageProcess
    {
        private readonly IDaemonProcess m_DaemonProcess;

        public MappingFileAnalysisDaemonStageProcess(IDaemonProcess daemonProcess) {
            m_DaemonProcess = daemonProcess;
        }

        public void Execute(Action<DaemonStageResult> commiter ) {
            Logger.LogMessage("NHibernatePlugin: MappingFileAnalysisDaemonStageProcess.Execute called");

            var psiManager = PsiManager.GetInstance(m_DaemonProcess.Solution);
            IFile file = psiManager.GetPsiFile(m_DaemonProcess.SourceFile, HbmXmlLanguage.Instance,
                new DocumentRange(m_DaemonProcess.SourceFile.Document, m_DaemonProcess.SourceFile.Document.DocumentRange)) as IXmlFile;

            if (file == null) {
                Logger.LogMessage("   NO PSI FILE !!! {0}", m_DaemonProcess.SourceFile.Name);
                return ;
            }

            var elementProcessor = new MappingFileAnalysisElementProcessor(m_DaemonProcess);
            file.ProcessDescendants(elementProcessor);

            if (m_DaemonProcess.InterruptFlag) {
                throw new ProcessCancelledException();
            }

            commiter(new DaemonStageResult(elementProcessor.Highlightings));
        }

        public IDaemonProcess DaemonProcess {
            get { return m_DaemonProcess; }
        }
    }
}