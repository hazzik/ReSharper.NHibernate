using JetBrains.ReSharper.Daemon;

namespace NHibernatePlugin.Highlighting
{
    [ConfigurableSeverityHighlighting(Id, OverlapResolve = OverlapResolveKind.ERROR, ShowToolTipInStatusBar = true)]
    public class PropertyHighlighting : Highlighting
    {
        public const string Id = "NHibernatePlugin.Property";

        public PropertyHighlighting(string tooltip)
            : base(tooltip) {
        }
    }
}