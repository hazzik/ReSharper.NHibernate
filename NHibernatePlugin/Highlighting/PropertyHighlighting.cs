using JetBrains.ReSharper.Daemon;
using NHibernatePlugin.LanguageService;

namespace NHibernatePlugin.Highlighting
{
    [ConfigurableSeverityHighlighting(Id, HbmXmlLanguage.Name, OverlapResolve = OverlapResolveKind.ERROR, ShowToolTipInStatusBar = true)]
    public class PropertyHighlighting : Highlighting
    {
        public const string Id = "NHibernatePlugin.Property";

        public PropertyHighlighting(string tooltip)
            : base(tooltip) {
        }
    }
}