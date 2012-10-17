using JetBrains.ReSharper.Daemon;
using NHibernatePlugin.LanguageService;

namespace NHibernatePlugin.Highlighting
{
    [ConfigurableSeverityHighlighting(Id, HbmXmlLanguage.Name, OverlapResolve = OverlapResolveKind.ERROR, ShowToolTipInStatusBar = true)]
    public class ReferenceHighlighting : Highlighting
    {
        public const string Id = "NHibernatePlugin.Reference";

        public ReferenceHighlighting(string tooltip)
            : base(tooltip) {
        }
    }
}