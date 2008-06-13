using System;
using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Caches2;
using JetBrains.ReSharper.Psi.Impl;
using JetBrains.ReSharper.Psi.Tree;

namespace NHibernatePlugin.LanguageService
{
    public class MappingFileLanguageCacheProvider : ILanguageCacheProvider
    {
        private readonly PsiLanguageType m_LanguageType;

        public MappingFileLanguageCacheProvider(PsiLanguageType languageType) {
            m_LanguageType = languageType;
        }

        public void BuildCache(IDummyHolder dummyHolder, ICacheBuilder builder) {
        }

        public void BuildCache(IFile file, ICacheBuilder builder) {
        }

        public bool HasCaseSensitiveName(TypeElement element) {
            throw new InvalidOperationException();
        }

        public ProjectFilePart LoadProjectFilePart(IProjectFile projectFile, IReader reader) {
            return null;
        }

        public bool NeedCacheUpdate(IElement elementContainingChanges, PsiChangedElementType type) {
            return false;
        }

        public void OnProjectPropertiesChange(IProject project, ICollection<IProjectFile> files) {
        }

        public Part ReadPart(byte tag, IReader reader) {
            return null;
        }

        public bool CaseSensitive {
            get { return true; }
        }

        public PsiLanguageType LanguageType {
            get { return m_LanguageType; }
        }
    }
}