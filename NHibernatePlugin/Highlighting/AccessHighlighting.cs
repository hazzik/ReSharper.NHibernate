using JetBrains.ReSharper.Daemon;

namespace NHibernatePlugin.Highlighting
{
    [ConfigurableSeverityHighlighting(Id, OverlapResolve = OverlapResolveKind.ERROR, ShowToolTipInStatusBar = true)]
    public class AccessHighlighting : Highlighting
    {
        public const string Id = "NHibernatePlugin.Access";

        public AccessHighlighting(string tooltip)
            : base(tooltip) {
        }
    }
}