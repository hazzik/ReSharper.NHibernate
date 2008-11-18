using NHibernatePlugin.TypeNames.Parser;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace NHibernatePlugin.Tests.TypeNames
{
    [TestFixture]
    public class ParserTests
    {
        private Parser sut;
        private IParsedType result;

        [SetUp]
        public void Setup() {
            sut = new Parser();
        }

        [Test]
        public void Simple_Type_is_parsed() {
            result = sut.Parse("string");
            Assert.That(result.TypeName, Is.EqualTo("string"));
            Assert.That(result.TypeParameters, Is.Empty);
        }

        [Test]
        public void Generic_Type_with_one_parameter_is_parsed() {
            result = sut.Parse("IEnumerable`1[string]");
            Assert.That(result.TypeName, Is.EqualTo("IEnumerable`1"));
            Assert.That(result.TypeParameters.Count, Is.EqualTo(1));
            Assert.That(result.TypeParameters[0].TypeName, Is.EqualTo("string"));
            Assert.That(result.TypeParameters[0].TypeParameters, Is.Empty);
        }

        [Test]
        public void Generic_Type_with_three_parameter_is_parsed() {
            result = sut.Parse("System.IEnumerable`2[System.string,int,Bla.Fasel42.Blub]");
            Assert.That(result.TypeName, Is.EqualTo("System.IEnumerable`2"));
            Assert.That(result.TypeParameters.Count, Is.EqualTo(3));
            Assert.That(result.TypeParameters[0].TypeName, Is.EqualTo("System.string"));
            Assert.That(result.TypeParameters[0].TypeParameters, Is.Empty);
            Assert.That(result.TypeParameters[1].TypeName, Is.EqualTo("int"));
            Assert.That(result.TypeParameters[1].TypeParameters, Is.Empty);
            Assert.That(result.TypeParameters[2].TypeName, Is.EqualTo("Bla.Fasel42.Blub"));
            Assert.That(result.TypeParameters[2].TypeParameters, Is.Empty);
        }

        [Test]
        public void Generic_Type_with_one_generic_parameter_is_parsed() {
            result = sut.Parse("IList`2[IList`1[string]]");
            Assert.That(result.TypeName, Is.EqualTo("IList`2"));
            Assert.That(result.TypeParameters.Count, Is.EqualTo(1));
            Assert.That(result.TypeParameters[0].TypeName, Is.EqualTo("IList`1"));
            Assert.That(result.TypeParameters[0].TypeParameters.Count, Is.EqualTo(1));

            Assert.That(result.TypeParameters[0].TypeParameters[0].TypeName, Is.EqualTo("string"));
            Assert.That(result.TypeParameters[0].TypeParameters[0].TypeParameters, Is.Empty);
        }
    }
}