using System;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace NHibernatePlugin.Analysis.CSharp
{
    /// <summary>
    /// Daemon stage for analysing C# files. This class is automatically loaded by ReSharper daemon 
    /// because it's marked with the [DaemonStage] attribute.
    /// </summary>
    [DaemonStage(StagesBefore = new[] { typeof(LanguageSpecificDaemonStage) })]
    public class CSharpAnalysisDaemonStage : CSharpDaemonStageBase
    {
        /// <summary>
        /// This method provides a <see cref="IDaemonStageProcess"/> instance which is assigned to highlighting a single document
        /// </summary>

        protected override IDaemonStageProcess CreateProcess(IDaemonProcess process, IContextBoundSettingsStore settings, DaemonProcessKind processKind, ICSharpFile file) {
            if (process == null)
                throw new ArgumentNullException("process");

            return new CSharpAnalysisDaemonStageProcess(process);

        }

        public override ErrorStripeRequest NeedsErrorStripe(IPsiSourceFile sourceFile, IContextBoundSettingsStore settingsStore) {
            // We want to add markers to the right-side stripe as well as contribute to document errors
            return ErrorStripeRequest.STRIPE_AND_ERRORS;
        }
    }
}