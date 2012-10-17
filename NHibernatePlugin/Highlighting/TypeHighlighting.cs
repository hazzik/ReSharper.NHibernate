using JetBrains.ReSharper.Daemon;
using NHibernatePlugin.LanguageService;

namespace NHibernatePlugin.Highlighting
{
    [ConfigurableSeverityHighlighting(Id, HbmXmlLanguage.Name, OverlapResolve = OverlapResolveKind.ERROR, ShowToolTipInStatusBar = true)]
    public class TypeHighlighting : Highlighting
    {
        public const string Id = "NHibernatePlugin.Type";

        public TypeHighlighting(string tooltip)
            : base(tooltip) {
        }
    }
}