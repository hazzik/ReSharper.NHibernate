using NHibernatePlugin.TypeNames.Parser;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace NHibernatePlugin.Tests.TypeParser
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
            result = sut.Parse("IEnumerable´[string]");
            Assert.That(result.TypeName, Is.EqualTo("IEnumerable"));
            Assert.That(result.TypeParameters.Count, Is.EqualTo(1));
            Assert.That(result.TypeParameters[0].TypeName, Is.EqualTo("string"));
            Assert.That(result.TypeParameters[0].TypeParameters, Is.Empty);
        }

        [Test]
        public void Generic_Type_with_three_parameter_is_parsed() {
            result = sut.Parse("IEnumerable´[string,int,char]");
            Assert.That(result.TypeName, Is.EqualTo("IEnumerable"));
            Assert.That(result.TypeParameters.Count, Is.EqualTo(3));
            Assert.That(result.TypeParameters[0].TypeName, Is.EqualTo("string"));
            Assert.That(result.TypeParameters[0].TypeParameters, Is.Empty);
            Assert.That(result.TypeParameters[1].TypeName, Is.EqualTo("int"));
            Assert.That(result.TypeParameters[1].TypeParameters, Is.Empty);
            Assert.That(result.TypeParameters[2].TypeName, Is.EqualTo("char"));
            Assert.That(result.TypeParameters[2].TypeParameters, Is.Empty);
        }
    }
}