using System;

using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;

namespace NHibernatePlugin.Analysis.CSharp
{
    /// <summary>
    /// Daemon stage for analysing C# files. This class is automatically loaded by ReSharper daemon 
    /// because it's marked with the [DaemonStage] attribute.
    /// </summary>
    [DaemonStage(StagesBefore = new Type[] { typeof(LanguageSpecificDaemonStage) }, RunForInvisibleDocument = false)]
    public class CSharpAnalysisDaemonStage : IDaemonStage
    {
        /// <summary>
        /// This method provides a <see cref="IDaemonStageProcess"/> instance which is assigned to highlighting a single document
        /// </summary>
        public IDaemonStageProcess CreateProcess(IDaemonProcess process) {
            if (process == null)
                throw new ArgumentNullException("process");

            return new CSharpAnalysisDaemonStageProcess(process);
        }

        public ErrorStripeRequest NeedsErrorStripe(IProjectFile projectFile) {
            // We want to add markers to the right-side stripe as well as contribute to document errors
            return ErrorStripeRequest.STRIPE_AND_ERRORS;
        }
    }
}