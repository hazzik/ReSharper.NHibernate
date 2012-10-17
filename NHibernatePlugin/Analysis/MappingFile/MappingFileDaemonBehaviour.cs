using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Xaml;

namespace NHibernatePlugin.Analysis.MappingFile
{
    [Language(typeof(HbmXmlLanguage))]
    public class MappingFileDaemonBehaviour : ILanguageSpecificDaemonBehavior
    {
        public ErrorStripeRequest InitialErrorStripe(IPsiSourceFile file) {
            return !file.Properties.ShouldBuildPsi || (!file.PrimaryPsiLanguage.Is<HbmXmlLanguage>())
                ? ErrorStripeRequest.NONE
                : ErrorStripeRequest.STRIPE_AND_ERRORS;
        }

        public bool CanShowErrorBox {
            get { return true; }
        }

        public bool RunInSolutionAnalysis {
            get { return true; }
        }
    }
}