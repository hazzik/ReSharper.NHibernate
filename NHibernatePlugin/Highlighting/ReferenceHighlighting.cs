using JetBrains.ReSharper.Daemon;

namespace NHibernatePlugin.Highlighting
{
    [ConfigurableSeverityHighlighting(Id, OverlapResolve = OverlapResolveKind.ERROR, ShowToolTipInStatusBar = true)]
    public class ReferenceHighlighting : Highlighting
    {
        public const string Id = "NHibernatePlugin.Reference";

        public ReferenceHighlighting(string tooltip)
            : base(tooltip) {
        }
    }
}