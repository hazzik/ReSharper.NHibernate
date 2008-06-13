using System;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.Xml.Stages;
using JetBrains.Util;

namespace NHibernatePlugin.Analysis.MappingFile
{
    /// <summary>
    /// Daemon stage for analysing NHibernate mapping files (*.hbm.xml). The class is automatically
    /// loaded by ReSharper daemon because it's marked with the [DaemonStage] attribute.
    /// </summary>
    [DaemonStage(StagesAfter = new Type[] { typeof(XmlSyntaxErrorsStage) },  RunForInvisibleDocument = true)]
    public class MappingFileAnalysisDaemonStage : IDaemonStage
    {
        /// <summary>
        /// Provides an <see cref="IDaemonStageProcess"/> instance which is assigned to
        /// highlighting a single document
        /// </summary>
        public IDaemonStageProcess CreateProcess(IDaemonProcess process) {
            if (process == null) {
                throw new ArgumentNullException("process");
            }
            Logger.LogMessage("NHibernatePlugin: MappingFileAnalysisDaemonStage.CreateProcess called");
            return new MappingFileAnalysisDaemonStageProcess(process);
        }

        /// <summary>
        /// Returns <see cref="ErrorStripeRequest.STRIPE_AND_ERRORS"/> if the name of <paramref name="projectFile"/> 
        /// ends with ".hbm.xml", <see cref="ErrorStripeRequest.NONE"/> else.
        /// </summary>
        /// <param name="projectFile"></param>
        /// <returns></returns>
        public ErrorStripeRequest NeedsErrorStripe(IProjectFile projectFile) {
            // We want to add markers to the right-side stripe as well as contribute to document errors
            return projectFile.Name.EndsWith(Constants.MappingFileExtension, StringComparison.Ordinal) ? ErrorStripeRequest.STRIPE_AND_ERRORS : ErrorStripeRequest.NONE;
        }
    }
}