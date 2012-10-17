using JetBrains.ReSharper.Daemon;
using NHibernatePlugin.LanguageService;

namespace NHibernatePlugin.Highlighting
{
    [ConfigurableSeverityHighlighting(Id, HbmXmlLanguage.Name, OverlapResolve = OverlapResolveKind.ERROR, ShowToolTipInStatusBar = true)]
    public class NamespaceHighlighting : Highlighting
    {
        public const string Id = "NHibernatePlugin.Namespace";

        public NamespaceHighlighting(string tooltip)
            : base(tooltip) {
        }
    }
}