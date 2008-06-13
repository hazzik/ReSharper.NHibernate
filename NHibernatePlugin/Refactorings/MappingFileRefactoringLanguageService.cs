using JetBrains.ProjectModel;
using JetBrains.ReSharper.Refactorings;
using JetBrains.ReSharper.Refactorings.Rename;
using JetBrains.ReSharper.Refactorings.RenameModel;
using JetBrains.ReSharper.Refactorings.Workflow;
using JetBrains.Util;
using NHibernatePlugin.LanguageService;
using NHibernatePlugin.Refactorings.Rename;

namespace NHibernatePlugin.Refactorings
{
    [LanguageSpecificImplementation(MappingFileLanguageService.MAPPING_FILE_LANGUAGEID, typeof(RefactoringLanguageService))]
    public class MappingFileRefactoringLanguageService : RefactoringLanguageService
    {
        public override RenameBase CreateRename(RenameWorkflow workflow, ISolution solution, IRefactoringDriver driver) {
            Logger.LogMessage("NHibernatePlugin: CreateRename called");
            return new MappingFileRename(workflow, solution, driver);
        }
    }
}