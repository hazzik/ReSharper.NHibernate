using System.Collections.Generic;
using System.Drawing;
using JetBrains.Application;
using JetBrains.Application.Components;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Caches2;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Xml;
using JetBrains.Text;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService
{
    [ProjectFileLanguageService(new string[] { ".xml" }), ShellComponent(ProgramConfigurations.ALL), XmlLanguage]
    public class MappingFileProjectFileLanguageService : IDerivedProjectFileLanguageService
    {
        public static ProjectFileType MAPPING_FILE = new ProjectFileType("MappingFile");
        private static readonly PsiLanguageType[] POSSIBLE_PSI_LANGUAGE_TYPES = new PsiLanguageType[] { HbmXmlLanguage.Instance };
        //private readonly IWordIndexLanguageProvider m_WordIndexLanguageProvider = new XmlWordIndexLanguageProvider();

        public ProjectFileType GetProjectFileType(IProjectFile file) {
            //Logger.LogMessage("MappingFileProjectFileLanguageService.GetProjectFileType for file {0}", file.Name);
            if (file.Name.EndsWith(Constants.MappingFileExtension)) {
                return MAPPING_FILE;
            }
            return ProjectFileType.UNKNOWN;
        }

        public PsiLanguageType GetPsiLanguageType(IProjectFile file) {
            //Logger.LogMessage("MappingFileProjectFileLanguageService.GetPsiLanguageType for file {0}", file.Name);
            return GetPsiLanguageType(file.LanguageType);
        }

        public PsiLanguageType GetPsiLanguageType(ProjectFileType projectFileType) {
            //Logger.LogMessage("MappingFileProjectFileLanguageService.GetPsiLanguageType for project file type {0}", projectFileType.Name);
            if (projectFileType != MAPPING_FILE) {
                return UnknownLanguage.UNKNOWN;
            }
            return HbmXmlLanguage.Instance;
        }

        public ILexerFactory CreateLexer(ProjectFileType languageType, IBuffer buffer) {
            //Logger.LogMessage("MappingFileProjectFileLanguageService.CreateLexer for language type {0}", languageType.Name);
            if (languageType == MAPPING_FILE) {
                return new XmlLexerFactory(HbmXmlLanguage.Instance);
            }
            return null;
        }

        public ProjectFileType LanguageType {
            get { return MAPPING_FILE; }
        }

        public Image Icon {
            get { return null; }
        }

        public ICollection<PsiLanguageType> PossiblePsiLanguageTypes {
            get {
                Logger.LogMessage("MappingFileProjectFileLanguageService.PossiblePsiLanguageTypes");
                return POSSIBLE_PSI_LANGUAGE_TYPES;
            }
        }

        public IWordIndexLanguageProvider WordIndexLanguageProvider {
            get {
                Logger.LogMessage("MappingFileProjectFileLanguageService.WordIndexLanguageProvider");
                return ProjectFileLanguageServiceManager.Instance.GetService(XmlLanguage.Instance).WordIndexLanguageProvider;
                //return m_WordIndexLanguageProvider; 
            }
        }

        public void Init() {
        }

        public void Dispose() {
        }
    }
}