using JetBrains.Application;
using JetBrains.ComponentModel;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Caches2;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService
{
    [LanguageService, ShellComponentInterface(ProgramConfigurations.ALL), ShellComponentImplementation]
    public class MappingFileLanguageService : XmlLanguageServiceBase, IShellComponent
    {
        public static PsiLanguageType MAPPING_FILE = new PsiLanguageType("MappingFile");
        public const string MAPPING_FILE_LANGUAGEID = "MappingFile";

        public MappingFileLanguageService()
            : base(MAPPING_FILE) {
            Logger.LogMessage("NHibernatePlugin: MappingFileLanguageService ctor");
        }

        public override IParser CreateParser(ILexer lexer, ISolution solution, IProject project, CheckForInterrupt checkForInterrupt) {
            return new MappingFileParser(lexer, checkForInterrupt);
        }

        public override bool ShouldInvalidatePsiCache(IElement element, PsiChangedElementType elementType) {
            return true;
        }
        public override ILanguageSpecificSearcherFactory LanguageSpecificSearcherFactory {
            get { return MappingFileSearcherFactory.Instance; }
        }

        public override ILanguageCacheProvider CacheProvider {
            get { return new MappingFileLanguageCacheProvider(MAPPING_FILE); }
        }

        public override ITypePresenter TypePresenter {
            get { return MappingFileTypePresenter.Instance; }
        }

        public override PsiLanguageType LanguageType {
            get { return MAPPING_FILE; }
        }

        public void Init() {
        }

        public void Dispose() {
        }
    }
}