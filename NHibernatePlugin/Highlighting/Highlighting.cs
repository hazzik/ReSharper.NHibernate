using JetBrains.ReSharper.Daemon;

namespace NHibernatePlugin.Highlighting
{
    public abstract class Highlighting : ICustomAttributeHighlighting
    {
        private readonly string m_Tooltip;

        protected Highlighting(string tooltip) {
            m_Tooltip = tooltip;
        }

        public string ToolTip {
            get { return m_Tooltip; }
        }

        public string ErrorStripeToolTip {
            get { return m_Tooltip; }
        }

        public int NavigationOffsetPatch {
            get { return 0; }
        }

        public string AttributeId {
            get { return HighlightingAttributeIds.ERROR_ATTRIBUTE; }
        }
    }
}