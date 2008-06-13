using JetBrains.ReSharper.Daemon;

namespace NHibernatePlugin.Highlighting
{
    [ConfigurableSeverityHighlighting(Id, OverlapResolve = OverlapResolveKind.ERROR, ShowToolTipInStatusBar = true)]
    public class NamespaceHighlighting : Highlighting
    {
        public const string Id = "NHibernatePlugin.Namespace";

        public NamespaceHighlighting(string tooltip)
            : base(tooltip) {
        }
    }
}