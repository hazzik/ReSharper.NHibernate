using NHibernatePlugin.Helper;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace NHibernatePlugin.Tests.Helper
{
    [TestFixture]
    public class TypeNameParserTests
    {
        [Test]
        public void NullTypeName() {
            TypeNameParser typeNameParser = new TypeNameParser(null, "", "");
            Assert.That(typeNameParser.TypeName, Is.EqualTo(""));
            Assert.That(typeNameParser.Namespace, Is.EqualTo(""));
            Assert.That(typeNameParser.QualifiedTypeName, Is.EqualTo(""));
            Assert.That(typeNameParser.FullQualifiedTypeName, Is.EqualTo(""));
            Assert.That(typeNameParser.AssemblyName, Is.EqualTo(""));
            Assert.That(typeNameParser.FullQualifiedAssemblyName, Is.EqualTo(""));
        }
        
        [Test]
        public void NullAssembly() {
            TypeNameParser typeNameParser = new TypeNameParser("", null, "");
            Assert.That(typeNameParser.TypeName, Is.EqualTo(""));
            Assert.That(typeNameParser.Namespace, Is.EqualTo(""));
            Assert.That(typeNameParser.QualifiedTypeName, Is.EqualTo(""));
            Assert.That(typeNameParser.FullQualifiedTypeName, Is.EqualTo(""));
            Assert.That(typeNameParser.AssemblyName, Is.EqualTo(""));
            Assert.That(typeNameParser.FullQualifiedAssemblyName, Is.EqualTo(""));
        }

        [Test]
        public void NullNamespace() {
            TypeNameParser typeNameParser = new TypeNameParser("", "", null);
            Assert.That(typeNameParser.TypeName, Is.EqualTo(""));
            Assert.That(typeNameParser.Namespace, Is.EqualTo(""));
            Assert.That(typeNameParser.QualifiedTypeName, Is.EqualTo(""));
            Assert.That(typeNameParser.FullQualifiedTypeName, Is.EqualTo(""));
            Assert.That(typeNameParser.AssemblyName, Is.EqualTo(""));
            Assert.That(typeNameParser.FullQualifiedAssemblyName, Is.EqualTo(""));
        }

        [Test]
        public void NameWithoutNamespace() {
            TypeNameParser typeNameParser = new TypeNameParser("String", "", "");
            Assert.That(typeNameParser.TypeName, Is.EqualTo("String"));
            Assert.That(typeNameParser.Namespace, Is.EqualTo(""));
            Assert.That(typeNameParser.QualifiedTypeName, Is.EqualTo("String"));
            Assert.That(typeNameParser.FullQualifiedTypeName, Is.EqualTo("String"));
            Assert.That(typeNameParser.AssemblyName, Is.EqualTo(""));
            Assert.That(typeNameParser.FullQualifiedAssemblyName, Is.EqualTo(""));
        }

        [Test]
        public void SimpleName() {
            TypeNameParser typeNameParser = new TypeNameParser("System.String", "", "");
            Assert.That(typeNameParser.TypeName, Is.EqualTo("String"));
            Assert.That(typeNameParser.Namespace, Is.EqualTo("System"));
            Assert.That(typeNameParser.QualifiedTypeName, Is.EqualTo("System.String"));
            Assert.That(typeNameParser.FullQualifiedTypeName, Is.EqualTo("System.String"));
            Assert.That(typeNameParser.AssemblyName, Is.EqualTo(""));
            Assert.That(typeNameParser.FullQualifiedAssemblyName, Is.EqualTo(""));
        }

        [Test]
        public void QualifiedName() {
            TypeNameParser typeNameParser = new TypeNameParser("System.String, mscorlib", "", "");
            Assert.That(typeNameParser.TypeName, Is.EqualTo("String"));
            Assert.That(typeNameParser.Namespace, Is.EqualTo("System"));
            Assert.That(typeNameParser.QualifiedTypeName, Is.EqualTo("System.String"));
            Assert.That(typeNameParser.FullQualifiedTypeName, Is.EqualTo("System.String, mscorlib"));
            Assert.That(typeNameParser.AssemblyName, Is.EqualTo("mscorlib"));
            Assert.That(typeNameParser.FullQualifiedAssemblyName, Is.EqualTo("mscorlib"));
        }

        [Test]
        public void QualifiedNameWithBlanks() {
            TypeNameParser typeNameParser = new TypeNameParser("  System.String  ,   mscorlib  ", "", "");
            Assert.That(typeNameParser.TypeName, Is.EqualTo("String"));
            Assert.That(typeNameParser.Namespace, Is.EqualTo("System"));
            Assert.That(typeNameParser.QualifiedTypeName, Is.EqualTo("System.String"));
            Assert.That(typeNameParser.FullQualifiedTypeName, Is.EqualTo("System.String, mscorlib"));
            Assert.That(typeNameParser.AssemblyName, Is.EqualTo("mscorlib"));
            Assert.That(typeNameParser.FullQualifiedAssemblyName, Is.EqualTo("mscorlib"));
        }

        [Test]
        public void FullQualifiedName() {
            TypeNameParser typeNameParser = new TypeNameParser("System.String, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", "", "");
            Assert.That(typeNameParser.TypeName, Is.EqualTo("String"));
            Assert.That(typeNameParser.Namespace, Is.EqualTo("System"));
            Assert.That(typeNameParser.QualifiedTypeName, Is.EqualTo("System.String"));
            Assert.That(typeNameParser.FullQualifiedTypeName, Is.EqualTo("System.String, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
            Assert.That(typeNameParser.AssemblyName, Is.EqualTo("mscorlib"));
            Assert.That(typeNameParser.FullQualifiedAssemblyName, Is.EqualTo("mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
        }

        [Test]
        public void SimpleName_with_assembly_and_namespace() {
            TypeNameParser typeNameParser = new TypeNameParser("String", "mscorlib", "System");
            Assert.That(typeNameParser.TypeName, Is.EqualTo("String"));
            Assert.That(typeNameParser.Namespace, Is.EqualTo("System"));
            Assert.That(typeNameParser.QualifiedTypeName, Is.EqualTo("System.String"));
            Assert.That(typeNameParser.FullQualifiedTypeName, Is.EqualTo("System.String, mscorlib"));
            Assert.That(typeNameParser.AssemblyName, Is.EqualTo("mscorlib"));
            Assert.That(typeNameParser.FullQualifiedAssemblyName, Is.EqualTo("mscorlib"));
        }
    }
}