using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Refactorings.Conflicts;
using JetBrains.ReSharper.Refactorings.Rename;
using JetBrains.ReSharper.Refactorings.RenameModel;
using JetBrains.ReSharper.Refactorings.Workflow;
using JetBrains.Util;

namespace NHibernatePlugin.Refactorings.Rename
{
    public class MappingFileRename : RenameBase
    {
        public MappingFileRename(RenameWorkflow workflow, ISolution solution, IRefactoringDriver driver)
            : base(workflow, solution, driver) {
            Logger.LogMessage("NHibernatePlugin: MappingFileRename ctor called");
        }

        public override IList<IConflictSearcher> AdditionalConflictsSearchers(IDeclaredElement element, string newName) {
            return EmptyArray<IConflictSearcher>.Instance;
        }

        public override void AdditionalReferenceProcessing(IDeclaredElement newTarget, IReference reference, ICollection<IReference> newReferences) {
        }

        public override bool DoNotProcess(IDeclaredElement element) {
            return false;
        }

        public override string[] GetPossibleReferenceNames(IDeclaredElement element, string newName) {
            Logger.LogMessage("GetPossibleReferenceNames element {0}, newName {1}", element.ShortName, newName);
            return new string[] { newName };
        }

        public override IReference TransformAnonymousType(IReference reference) {
            return reference;
        }
    }
}