using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Refactorings;
using JetBrains.ReSharper.Refactorings.Rename;
using JetBrains.ReSharper.Refactorings.RenameModel;
using JetBrains.ReSharper.Refactorings.Workflow;
using JetBrains.Util;
using NHibernatePlugin.Refactorings.Rename;

namespace NHibernatePlugin.Refactorings
{
    [Language(typeof(InternalRefactoringLanguageService))]
    public class MappingFileRefactoringLanguageService : InternalRefactoringLanguageService
    {
        public override RenameBase CreateRename(RenameWorkflow workflow, ISolution solution, IRefactoringDriver driver) {
            Logger.LogMessage("NHibernatePlugin: CreateRename called");
            return new MappingFileRename(workflow, solution, driver);
        }
    }
}