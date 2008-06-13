using JetBrains.ComponentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;

namespace NHibernatePlugin.LanguageService
{
    [BuildPsiProvider(ProgramConfigurations.ALL)]
    public class MappingFileBuildProviderChecker : ISupportedByPSIChecker
    {
        public BuildPsiResult Check(IProjectFile projectFile) {
            return projectFile.Name.EndsWith(Constants.MappingFileExtension) ? BuildPsiResult.DO_BUILD : BuildPsiResult.UNDEF;
        }
    }
}