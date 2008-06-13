using JetBrains.ReSharper.Daemon;

namespace NHibernatePlugin.Highlighting
{
    [ConfigurableSeverityHighlighting(Id, OverlapResolve = OverlapResolveKind.ERROR, ShowToolTipInStatusBar = true)]
    public class EmbeddedResourceHighlighting : Highlighting
    {
        public const string Id = "NHibernatePlugin.EmbeddedResource";

        public EmbeddedResourceHighlighting(string tooltip)
            : base(tooltip) {
        }
    }
}