using JetBrains.ReSharper.Daemon;
using NHibernatePlugin.LanguageService;

namespace NHibernatePlugin.Highlighting
{
    [ConfigurableSeverityHighlighting(Id, HbmXmlLanguage.Name, OverlapResolve = OverlapResolveKind.ERROR, ShowToolTipInStatusBar = true)]
    public class AccessHighlighting : Highlighting
    {
        public const string Id = "NHibernatePlugin.Access";

        public AccessHighlighting(string tooltip)
            : base(tooltip) {
        }
    }
}