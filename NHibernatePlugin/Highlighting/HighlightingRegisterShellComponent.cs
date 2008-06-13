using JetBrains.Application;
using JetBrains.ComponentModel;
using JetBrains.ReSharper.Daemon;
using NHibernatePlugin.Highlighting;

namespace NHibernatePlugin.Highlighting
{
    [ShellComponentImplementation, ShellComponentInterface(ProgramConfigurations.ALL)]
    public class HighlightingRegisterShellComponent : IShellComponent
    {
        private const string GroupNHibernateMappings = "NHibernate Mappings";

        public void Init() {
            HighlightingSettingsManager.Instance.RegisterConfigurableSeverity(PropertyHighlighting.Id, GroupNHibernateMappings,
                "Property/field error in mapping file", "Undefined property or field in .hbm.xml file.", Severity.ERROR);
            HighlightingSettingsManager.Instance.RegisterConfigurableSeverity(TypeHighlighting.Id, GroupNHibernateMappings,
                "Type error in mapping file", "Undefined type in .hbm.xml mapping files.", Severity.ERROR);
            HighlightingSettingsManager.Instance.RegisterConfigurableSeverity(AccessHighlighting.Id, GroupNHibernateMappings,
                "Access attribute error in mapping file", "Undefined 'access' attribute values in .hbm.xml mapping files.", Severity.ERROR);
            HighlightingSettingsManager.Instance.RegisterConfigurableSeverity(NamespaceHighlighting.Id, GroupNHibernateMappings,
                "Namespace error in mapping file", "Undefined 'namespace' attribute values in .hbm.xml mapping files.", Severity.ERROR);
            HighlightingSettingsManager.Instance.RegisterConfigurableSeverity(EmbeddedResourceHighlighting.Id, GroupNHibernateMappings,
                "Mapping files build action is not 'embedded resource'", ".hbm.xml mapping files should be embedded as resource", Severity.ERROR);
            HighlightingSettingsManager.Instance.RegisterConfigurableSeverity(ReferenceHighlighting.Id, GroupNHibernateMappings,
                "Project containing mapped class is not referenced in project containing mapping", "Project should be referenced for ReSharper to be able to find the references", Severity.ERROR);
        }

        public void Dispose() {
        }
    }
}