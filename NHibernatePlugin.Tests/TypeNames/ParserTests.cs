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
        private IParserError error;

        [SetUp]
        public void Setup() {
            sut = new Parser();
        }

        [Test]
        public void Simple_type_is_parsed() {
            result = sut.Parse("string", out error);
            Assert.That(error, Is.EqualTo(ParserError.None));
            Assert.That(result.TypeName, Is.EqualTo("string"));
            Assert.That(result.TypeParameters, Is.Empty);
        }

        [Test]
        public void Simple_type_with_assembly_is_parsed() {
            result = sut.Parse("string, mscorlib", out error);
            Assert.That(error, Is.EqualTo(ParserError.None), error.Message);
            Assert.That(result.TypeName, Is.EqualTo("string"));
            Assert.That(result.AssemblyName, Is.EqualTo("mscorlib"));
            Assert.That(result.TypeParameters, Is.Empty);
        }

        [Test]
        public void Simple_embedded_type_is_parsed() {
            result = sut.Parse("Customer+Order", out error);
            Assert.That(error, Is.EqualTo(ParserError.None), error.Message);
            Assert.That(result.TypeName, Is.EqualTo("Customer+Order"));
            Assert.That(result.TypeParameters, Is.Empty);
        }

        [Test]
        public void Generic_type_with_one_parameter_is_parsed() {
            result = sut.Parse("IEnumerable`1[string]", out error);
            Assert.That(error, Is.EqualTo(ParserError.None), error.Message);
            Assert.That(result.TypeName, Is.EqualTo("IEnumerable`1"));
            Assert.That(result.TypeParameters.Count, Is.EqualTo(1));
            Assert.That(result.TypeParameters[0].TypeName, Is.EqualTo("string"));
            Assert.That(result.TypeParameters[0].TypeParameters, Is.Empty);
        }

        [Test]
        public void Generic_type_with_missing_right_bracked_is_in_error() {
            result = sut.Parse("IEnumerable`1[string", out error);
            Assert.That(error.Message, Is.EqualTo("']' expected"));
        }

        [Test]
        public void Generic_type_with_three_parameters_is_parsed() {
            result = sut.Parse("System.IEnumerable`2[System.string,int,Bla.Fasel42.Blub]", out error);
            Assert.That(error, Is.EqualTo(ParserError.None), error.Message);
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
        public void Generic_type_with_three_parameters_and_white_space_is_parsed() {
            result = sut.Parse("System.IEnumerable`2 [ System.string  ,  int  ,  Bla.Fasel42.Blub  ]", out error);
            Assert.That(error, Is.EqualTo(ParserError.None), error.Message);
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
        public void Generic_type_with_one_generic_parameter_is_parsed() {
            result = sut.Parse("IList`2[IList`1[string]]", out error);
            Assert.That(error, Is.EqualTo(ParserError.None), error.Message);
            Assert.That(result.TypeName, Is.EqualTo("IList`2"));
            Assert.That(result.TypeParameters.Count, Is.EqualTo(1));
            Assert.That(result.TypeParameters[0].TypeName, Is.EqualTo("IList`1"));
            Assert.That(result.TypeParameters[0].TypeParameters.Count, Is.EqualTo(1));

            Assert.That(result.TypeParameters[0].TypeParameters[0].TypeName, Is.EqualTo("string"));
            Assert.That(result.TypeParameters[0].TypeParameters[0].TypeParameters, Is.Empty);
        }

        [Test]
        public void Syntax_error_is_reported_on_simple_type() {
            result = sut.Parse("string,", out error);
            Assert.That(error.Message, Is.EqualTo("Name expected"));
            Assert.That(error.Index, Is.EqualTo(7));
        }

        [Test]
        public void Syntax_error_is_reported_on_generic_type_if_number_is_missing() {
            result = sut.Parse("IList`[string]", out error);
            Assert.That(error.Message, Is.EqualTo("Number expected"));
            Assert.That(error.Index, Is.EqualTo(7));
        }

        [Test]
        public void Syntax_error_is_reported_on_generic_type_if_second_simple_type_is_missing() {
            result = sut.Parse("IList`1[string,]", out error);
            Assert.That(error.Message, Is.EqualTo("Name expected"));
            Assert.That(error.Index, Is.EqualTo(16));
        }

        [Test]
        public void Syntax_error_is_reported_on_generic_type_if_simple_type_is_wrong() {
            result = sut.Parse("IList`1[5]", out error);
            Assert.That(error.Message, Is.EqualTo("Name expected"));
            Assert.That(error.Index, Is.EqualTo(9));
        }
    }
}