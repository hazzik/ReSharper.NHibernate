using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService
{
    public class MappingFileTypePresenter : ITypePresenter
    {
        public static readonly MappingFileTypePresenter Instance = new MappingFileTypePresenter();

        public string GetPresentableName(IType type) {
            Logger.LogMessage("MappingFileTypePresenter.GetPresentableName {0}", type);
            return GetTypePresenter(CSharpLanguageService.CSHARP).GetPresentableName(type);
        }

        public string GetLongPresentableName(IType type) {
            Logger.LogMessage("MappingFileTypePresenter.GetLongPresentableName {0}", type);
            return GetTypePresenter(CSharpLanguageService.CSHARP).GetLongPresentableName(type);

        }

        private static ITypePresenter GetTypePresenter(PsiLanguageType language) {
            JetBrains.ReSharper.Psi.LanguageService languageService = LanguageServiceManager.Instance.GetLanguageService(language);
            if (languageService != null) {
                return languageService.TypePresenter;
            }
            return null;
        }
    }
}