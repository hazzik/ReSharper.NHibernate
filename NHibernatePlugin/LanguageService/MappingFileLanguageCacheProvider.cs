using System;
using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Caches2;
using JetBrains.ReSharper.Psi.Impl;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace NHibernatePlugin.LanguageService
{
    public class MappingFileLanguageCacheProvider : ILanguageCacheProvider
    {
        private readonly PsiLanguageType m_LanguageType;

        public MappingFileLanguageCacheProvider(PsiLanguageType languageType) {
            m_LanguageType = languageType;
        }

        public void BuildCache(ISandBox dummyHolder, ICacheBuilder builder) {
        }

        public bool IsCaseSensitive(IPsiModule module) {
            throw new InvalidOperationException();
        }

        public void BuildCache(IFile file, ICacheBuilder builder) {
        }

        public ProjectFilePart LoadProjectFilePart(IPsiSourceFile projectFile, ProjectFilePartsTree tree, IReader reader) {
            return null;
        }

        public bool NeedCacheUpdate(ITreeNode elementContainingChanges, PsiChangedElementType type) {
            return false;
        }

        public IEnumerable<IPsiSourceFile> OnPsiModulePropertiesChange(IPsiModule module) {
            return EmptyList<IPsiSourceFile>.InstanceList;
        }

        public IEnumerable<IPsiSourceFile> OnProjectModelChange(ProjectModelChange projectModelChange) {
            return EmptyList<IPsiSourceFile>.InstanceList;
        }

        public Part ReadPart(byte tag, IReader reader) {
            return null;
        }

        public PsiLanguageType LanguageType {
            get { return m_LanguageType; }
        }
    }
}