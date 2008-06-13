using JetBrains.ReSharper.Psi.Xml.Tree;

namespace NHibernatePlugin.LanguageService.Psi
{
    public interface INamedTag : IXmlTag
    {
        IXmlAttribute GetNameAttribute();
    }
}