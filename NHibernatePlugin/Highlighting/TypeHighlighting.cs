using JetBrains.ReSharper.Daemon;

namespace NHibernatePlugin.Highlighting
{
    [ConfigurableSeverityHighlighting(Id, OverlapResolve = OverlapResolveKind.ERROR, ShowToolTipInStatusBar = true)]
    public class TypeHighlighting : Highlighting
    {
        public const string Id = "NHibernatePlugin.Type";

        public TypeHighlighting(string tooltip)
            : base(tooltip) {
        }
    }
}