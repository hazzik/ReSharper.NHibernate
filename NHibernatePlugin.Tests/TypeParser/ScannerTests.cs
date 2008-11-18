using NHibernatePlugin.TypeNames.Scanners;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace NHibernatePlugin.Tests.TypeParser
{
    [TestFixture]
    public class ScannerTests
    {
        private IToken result;
        private Scanner sut;

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
            sut = new Scanner("IList´[int]");
            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.Name));
            Assert.That(result.Text, Is.EqualTo("IList"));

            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.Accent));
            Assert.That(result.Text, Is.EqualTo("´"));

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
            sut = new Scanner("IList´[int,char,string]");
            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.Name));
            Assert.That(result.Text, Is.EqualTo("IList"));

            result = sut.NextToken();
            Assert.That(result.TokenType, Is.EqualTo(Scanner.TokenType.Accent));
            Assert.That(result.Text, Is.EqualTo("´"));

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
    }
}