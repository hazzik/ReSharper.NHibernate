using NHibernatePlugin.TypeNames.Scanners;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace NHibernatePlugin.Tests.TypeNames
{
    [TestFixture]
    public class ScannerTests
    {
        private IToken result;
        private Scanner sut;

        [Test]
        public void CurrentIndex_starts_at_the_beginning_of_the_input() {
            sut = new Scanner("string");
            Assert.That(sut.CurrentIndex, Is.EqualTo(0));
        }

        [Test]
        public void CurrentIndex_is_advanced_on_getting_tokens() {
            sut = new Scanner("IList`1[int,char]");

            result = sut.NextToken();
            Assert.That(sut.CurrentIndex, Is.EqualTo(5));

            result = sut.NextToken();
            Assert.That(sut.CurrentIndex, Is.EqualTo(6));

            result = sut.NextToken();
            Assert.That(sut.CurrentIndex, Is.EqualTo(7));

            result = sut.NextToken();
            Assert.That(sut.CurrentIndex, Is.EqualTo(8));

            result = sut.NextToken();
            Assert.That(sut.CurrentIndex, Is.EqualTo(11));

            result = sut.NextToken();
            Assert.That(sut.CurrentIndex, Is.EqualTo(12));

            result = sut.NextToken();
            Assert.That(sut.CurrentIndex, Is.EqualTo(16));

            result = sut.NextToken();
            Assert.That(sut.CurrentIndex, Is.EqualTo(17));
        }

        [Test]
        public void Simple_type_is_scanned() {
            sut = new Scanner("string");

            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.Name));
            Assert.That(result.Text, Is.EqualTo("string"));
            Assert.That(sut.NextToken().TokenType, Is.EqualTo(Scanner.TokenType.EOF));
            Assert.That(sut.EOF);
        }

        [Test]
        public void Generic_type_with_one_parameter_is_scanned() {
            sut = new Scanner("IList`1[int]");
            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.Name));
            Assert.That(result.Text, Is.EqualTo("IList"));

            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.Accent));
            Assert.That(result.Text, Is.EqualTo("`"));

            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.Number));
            Assert.That(result.Text, Is.EqualTo("1"));

            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.LeftBracket));
            Assert.That(result.Text, Is.EqualTo("["));

            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.Name));
            Assert.That(result.Text, Is.EqualTo("int"));

            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.RightBracket));
            Assert.That(result.Text, Is.EqualTo("]"));

            Assert.That(sut.NextToken().TokenType, Is.EqualTo(Scanner.TokenType.EOF));
            Assert.That(sut.EOF);
        }

        [Test]
        public void Generic_type_with_three_parameter_is_scanned() {
            sut = new Scanner("IList`1[int,char,string]");
            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.Name));
            Assert.That(result.Text, Is.EqualTo("IList"));

            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.Accent));
            Assert.That(result.Text, Is.EqualTo("`"));

            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.Number));
            Assert.That(result.Text, Is.EqualTo("1"));

            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.LeftBracket));
            Assert.That(result.Text, Is.EqualTo("["));

            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.Name));
            Assert.That(result.Text, Is.EqualTo("int"));

            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.Comma));
            Assert.That(result.Text, Is.EqualTo(","));

            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.Name));
            Assert.That(result.Text, Is.EqualTo("char"));

            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.Comma));
            Assert.That(result.Text, Is.EqualTo(","));

            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.Name));
            Assert.That(result.Text, Is.EqualTo("string"));

            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.RightBracket));
            Assert.That(result.Text, Is.EqualTo("]"));

            Assert.That(sut.NextToken().TokenType, Is.EqualTo(Scanner.TokenType.EOF));
            Assert.That(sut.EOF);
        }

        [Test]
        public void NextSymbol_returns_EOF_if_there_is_no_more_unprocessed_input() {
            sut = new Scanner("");
            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.EOF));
            Assert.That(sut.EOF);
        }

        [Test]
        public void Unknown_characters_are_returned_as_TokenType_Unknown() {
            sut = new Scanner("'");
            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.Unknown));
            Assert.That(result.Text, Is.EqualTo("'"));
        }
    }
}