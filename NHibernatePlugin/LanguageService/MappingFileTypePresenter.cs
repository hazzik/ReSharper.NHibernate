using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Impl;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService
{
    public class MappingFileTypePresenter : ITypePresenter
    {
        public static readonly MappingFileTypePresenter Instance = new MappingFileTypePresenter();

        public string GetPresentableName(IType type) {
            Logger.LogMessage("MappingFileTypePresenter.GetPresentableName {0}", type);
            return GetTypePresenter(CSharpLanguage.Instance).GetPresentableName(type);
        }

        public string GetLongPresentableName(IType type) {
            Logger.LogMessage("MappingFileTypePresenter.GetLongPresentableName {0}", type);
            return GetTypePresenter(CSharpLanguage.Instance).GetLongPresentableName(type);
        }

        public string GetUnresolvedScalarTypePresentation(string name, ICollection<IType> typeArguments, ISolution solution) {
            throw new System.NotImplementedException();
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