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

        [Test]
        public void Full_qualified_name_with_assembly_and_namespace() {
            TypeNameParser typeNameParser = new TypeNameParser("System.String", "mscorlib", "System");
            Assert.That(typeNameParser.TypeName, Is.EqualTo("String"));
            Assert.That(typeNameParser.Namespace, Is.EqualTo("System"));
            Assert.That(typeNameParser.QualifiedTypeName, Is.EqualTo("System.String"));
            Assert.That(typeNameParser.FullQualifiedTypeName, Is.EqualTo("System.String, mscorlib"));
            Assert.That(typeNameParser.AssemblyName, Is.EqualTo("mscorlib"));
            Assert.That(typeNameParser.FullQualifiedAssemblyName, Is.EqualTo("mscorlib"));
        }

        [Test]
        public void Full_qualified_name_with_assembly_and_namespace2() {
            TypeNameParser typeNameParser = new TypeNameParser("System.String, mscorlib", "mscorlib", "System");
            Assert.That(typeNameParser.TypeName, Is.EqualTo("String"));
            Assert.That(typeNameParser.Namespace, Is.EqualTo("System"));
            Assert.That(typeNameParser.QualifiedTypeName, Is.EqualTo("System.String"));
            Assert.That(typeNameParser.FullQualifiedTypeName, Is.EqualTo("System.String, mscorlib"));
            Assert.That(typeNameParser.AssemblyName, Is.EqualTo("mscorlib"));
            Assert.That(typeNameParser.FullQualifiedAssemblyName, Is.EqualTo("mscorlib"));
        }

        [Test]
        public void Full_qualified_name_with_assembly_and_namespace3() {
            TypeNameParser typeNameParser = new TypeNameParser("My.Name.Space.Type, It.Assembly", "It.Assembly", "My.Name.Space");
            Assert.That(typeNameParser.TypeName, Is.EqualTo("Type"));
            Assert.That(typeNameParser.Namespace, Is.EqualTo("My.Name.Space"));
            Assert.That(typeNameParser.QualifiedTypeName, Is.EqualTo("My.Name.Space.Type"));
            Assert.That(typeNameParser.FullQualifiedTypeName, Is.EqualTo("My.Name.Space.Type, It.Assembly"));
            Assert.That(typeNameParser.AssemblyName, Is.EqualTo("It.Assembly"));
            Assert.That(typeNameParser.FullQualifiedAssemblyName, Is.EqualTo("It.Assembly"));
        }

        [Test]
        public void Generics() {
            TypeNameParser typeNameParser = new TypeNameParser("Cei.eMerge.Common.Range`1[System.DateTime], Cei.eMerge.Common", "Cei.eMerge.Common", "Cei.eMerge.Common");
            Assert.That(typeNameParser.TypeName, Is.EqualTo("Range`1[System.DateTime]"));
            Assert.That(typeNameParser.Namespace, Is.EqualTo("Cei.eMerge.Common"));
            Assert.That(typeNameParser.QualifiedTypeName, Is.EqualTo("Cei.eMerge.Common.Range`1[System.DateTime]"));
            Assert.That(typeNameParser.FullQualifiedTypeName, Is.EqualTo("Cei.eMerge.Common.Range`1[System.DateTime], Cei.eMerge.Common"));
            Assert.That(typeNameParser.AssemblyName, Is.EqualTo("Cei.eMerge.Common"));
            Assert.That(typeNameParser.FullQualifiedAssemblyName, Is.EqualTo("Cei.eMerge.Common"));
        }

        [Test]
        public void Generics2() {
            TypeNameParser typeNameParser = new TypeNameParser("Wrapper`1[Examples.Customer], ExamplesAsm", "ExamplesAsm", "Examples");
            Assert.That(typeNameParser.TypeName, Is.EqualTo("Wrapper`1[Examples.Customer]"));
            Assert.That(typeNameParser.Namespace, Is.EqualTo("Examples"));
            Assert.That(typeNameParser.QualifiedTypeName, Is.EqualTo("Examples.Wrapper`1[Examples.Customer]"));
            Assert.That(typeNameParser.FullQualifiedTypeName, Is.EqualTo("Examples.Wrapper`1[Examples.Customer], ExamplesAsm"));
            Assert.That(typeNameParser.AssemblyName, Is.EqualTo("ExamplesAsm"));
            Assert.That(typeNameParser.FullQualifiedAssemblyName, Is.EqualTo("ExamplesAsm"));
        }
    }
}