using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;

namespace NHibernatePlugin.Highlighting
{
    [ConfigurableSeverityHighlighting(Id, KnownLanguage.ANY_LANGUAGEID, OverlapResolve = OverlapResolveKind.ERROR, ShowToolTipInStatusBar = true)]
    public class EmbeddedResourceHighlighting : Highlighting
    {
        public const string Id = "NHibernatePlugin.EmbeddedResource";

        public EmbeddedResourceHighlighting(string tooltip)
            : base(tooltip) {
        }
    }
}