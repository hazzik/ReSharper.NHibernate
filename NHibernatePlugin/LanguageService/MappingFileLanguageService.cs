using JetBrains.Application;
using JetBrains.Application.Components;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Caches2;
using JetBrains.ReSharper.Psi.Parsing;
using JetBrains.ReSharper.Psi.Xml;
using JetBrains.ReSharper.Psi.Xml.Impl;
using JetBrains.ReSharper.Psi.Xml.Impl.CodeStyle;
using JetBrains.ReSharper.Psi.Xml.Parsing;
using JetBrains.Util;
using NHibernatePlugin.LanguageService.Parser;

namespace NHibernatePlugin.LanguageService
{
    [Language(typeof(HbmXmlLanguage)), ShellComponent(ProgramConfigurations.ALL)]
    public class MappingFileLanguageService : XmlLanguageServiceBase
    {
        public MappingFileLanguageService(HbmXmlLanguage xmlLanguage, IConstantValueService constantValueService, XmlTokenTypes tokenTypes, IXmlElementFactory elementFactory, XmlCodeFormatter codeFormatter, ISettingsStore settingsStore)
            : base(xmlLanguage, constantValueService, tokenTypes, elementFactory, codeFormatter, settingsStore) {
            Logger.LogMessage("NHibernatePlugin: MappingFileLanguageService ctor");
        }

        public override IParser CreateParser(ILexer lexer, IPsiModule module, IPsiSourceFile sourceFile) {
            return new MappingFileParser(lexer, ElementFactory);
        }

        public override IWordIndexLanguageProvider WordIndexLanguageProvider {
            get { return new XmlWordIndexLanguageProvider(); }
        }

        public override ILanguageCacheProvider CacheProvider {
            get { return new MappingFileLanguageCacheProvider(LanguageType); }
        }

        public override bool SupportTypeMemberCache {
            get { return false; }
        }

        public override ITypePresenter TypePresenter {
            get { return MappingFileTypePresenter.Instance; }
        }
    }
}