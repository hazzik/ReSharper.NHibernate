using NHibernatePlugin.TypeParser;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace NHibernatePlugin.Tests.TypeParser
{
    [TestFixture]
    public class ScannerTests
    {
        private ISymbol result;
        private Scanner sut;

        [Test]
        public void TypeName_is_scanned() {
            sut = new Scanner("string");
            result = sut.NextSymbol();
            Assert.That(result.SymbolType, Is.EqualTo(Scanner.SymbolType.Name));
            Assert.That(result.Text, Is.EqualTo("string"));
            Assert.That(sut.NextSymbol().SymbolType, Is.EqualTo(Scanner.SymbolType.EOF));
        }

        [Test]
        public void Generic_TypeName_is_scanned() {
            sut = new Scanner("IList´[int]");
            result = sut.NextSymbol();
            Assert.That(result.SymbolType, Is.EqualTo(Scanner.SymbolType.Name));
            Assert.That(result.Text, Is.EqualTo("IList"));

            result = sut.NextSymbol();
            Assert.That(result.SymbolType, Is.EqualTo(Scanner.SymbolType.Accent));
            Assert.That(result.Text, Is.EqualTo("´"));

            result = sut.NextSymbol();
            Assert.That(result.SymbolType, Is.EqualTo(Scanner.SymbolType.LeftBracket));
            Assert.That(result.Text, Is.EqualTo("["));

            result = sut.NextSymbol();
            Assert.That(result.SymbolType, Is.EqualTo(Scanner.SymbolType.Name));
            Assert.That(result.Text, Is.EqualTo("int"));

            result = sut.NextSymbol();
            Assert.That(result.SymbolType, Is.EqualTo(Scanner.SymbolType.RightBracket));
            Assert.That(result.Text, Is.EqualTo("]"));

            Assert.That(sut.NextSymbol().SymbolType, Is.EqualTo(Scanner.SymbolType.EOF));
        }

        [Test]
        public void NextSymbol_returns_EOF_if_there_is_no_more_unprocessed_input() {
            sut = new Scanner("");
            result = sut.NextSymbol();
            Assert.That(result.SymbolType, Is.EqualTo(Scanner.SymbolType.EOF));
        }
    }
}