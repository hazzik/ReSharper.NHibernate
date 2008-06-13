using NHibernatePlugin.Helper;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace NHibernatePlugin.Tests.Helper
{
    [TestFixture]
    public class AccessMethodTests
    {
        [Test]
        public void Null_Defaults_to_property() {
            AccessMethod accessMethod = new AccessMethod(null);
            Assert.That(accessMethod.Method, Is.EqualTo("property"));
            Assert.That(accessMethod.Naming, Is.EqualTo(""));
        }

        [Test]
        public void EmptyString_Defaults_to_property() {
            AccessMethod accessMethod = new AccessMethod("");
            Assert.That(accessMethod.Method, Is.EqualTo("property"));
            Assert.That(accessMethod.Naming, Is.EqualTo(""));
        }

        [Test]
        public void property_Defaults_to_property() {
            AccessMethod accessMethod = new AccessMethod("property");
            Assert.That(accessMethod.Method, Is.EqualTo("property"));
            Assert.That(accessMethod.Naming, Is.EqualTo(""));
        }

        [Test]
        public void property_dot_Defaults_to_property() {
            AccessMethod accessMethod = new AccessMethod("property.");
            Assert.That(accessMethod.Method, Is.EqualTo("property"));
            Assert.That(accessMethod.Naming, Is.EqualTo(""));
        }

        [Test]
        public void property_camelcase() {
            AccessMethod accessMethod = new AccessMethod("property.camelcase");
            Assert.That(accessMethod.Method, Is.EqualTo("property"));
            Assert.That(accessMethod.Naming, Is.EqualTo("camelcase"));
        }

        [Test]
        public void DefaultNaming() {
            AccessName("", "FOO", "FOO");
            AccessName("", "bar", "bar");
            AccessName("", "F", "F");
            AccessName("", "f", "f");
            AccessName("", "", "");
        }

        [Test]
        public void Camelcase() {
            AccessName(".camelcase", "FOO", "fOO");
            AccessName(".camelcase", "bar", "bar");
            AccessName(".camelcase", "F", "f");
            AccessName(".camelcase", "f", "f");
            AccessName(".camelcase", "", "");
        }

        [Test]
        public void Camelcase_underscore() {
            AccessName(".camelcase-underscore", "FOO", "_fOO");
            AccessName(".camelcase-underscore", "bar", "_bar");
            AccessName(".camelcase-underscore", "F", "_f");
            AccessName(".camelcase-underscore", "f", "_f");
            AccessName(".camelcase-underscore", "", "_");
        }

        [Test]
        public void Pascalcase() {
            AccessName(".pascalcase", "FOO", "FOO");
            AccessName(".pascalcase", "bar", "Bar");
            AccessName(".pascalcase", "F", "F");
            AccessName(".pascalcase", "f", "F");
            AccessName(".pascalcase", "", "");
        }

        [Test]
        public void Pascalcase_underscore() {
            AccessName(".pascalcase-underscore", "FOO", "_FOO");
            AccessName(".pascalcase-underscore", "bar", "_Bar");
            AccessName(".pascalcase-underscore", "F", "_F");
            AccessName(".pascalcase-underscore", "f", "_F");
            AccessName(".pascalcase-underscore", "", "_");
        }

        [Test]
        public void Pascalcase_m_underscore() {
            AccessName(".pascalcase-m-underscore", "FOO", "m_FOO");
            AccessName(".pascalcase-m-underscore", "bar", "m_Bar");
            AccessName(".pascalcase-m-underscore", "F", "m_F");
            AccessName(".pascalcase-m-underscore", "f", "m_F");
            AccessName(".pascalcase-m-underscore", "", "m_");
        }

        [Test]
        public void Pascalcase_m() {
            AccessName(".pascalcase-m", "FOO", "mFOO");
            AccessName(".pascalcase-m", "bar", "mBar");
            AccessName(".pascalcase-m", "F", "mF");
            AccessName(".pascalcase-m", "f", "mF");
            AccessName(".pascalcase-m", "", "m");
        }

        [Test]
        public void Lowercase() {
            AccessName(".lowercase", "FOO", "foo");
            AccessName(".lowercase", "bar", "bar");
            AccessName(".lowercase", "F", "f");
            AccessName(".lowercase", "f", "f");
            AccessName(".lowercase", "", "");
        }

        [Test]
        public void Lowercase_underscore() {
            AccessName(".lowercase-underscore", "FOO", "_foo");
            AccessName(".lowercase-underscore", "bar", "_bar");
            AccessName(".lowercase-underscore", "F", "_f");
            AccessName(".lowercase-underscore", "f", "_f");
            AccessName(".lowercase-underscore", "", "_");
        }

        [Test]
        public void Legal_Access_Methods() {
            AssertUnknown("property", false);

            AssertUnknown("field", false);
            AssertUnknown("field.camelcase", false);
            AssertUnknown("field.camelcase-underscore", false);
            AssertUnknown("field.pascalcase", false);
            AssertUnknown("field.pascalcase-underscore", false);
            AssertUnknown("field.pascalcase-m-underscore", false);
            AssertUnknown("field.pascalcase-m", false);
            AssertUnknown("field.lowercase", false);
            AssertUnknown("field.lowercase-underscore", false);

            AssertUnknown("nosetter.camelcase", false);
            AssertUnknown("nosetter.camelcase-underscore", false);
            AssertUnknown("nosetter.pascalcase", false);
            AssertUnknown("nosetter.pascalcase-underscore", false);
            AssertUnknown("nosetter.pascalcase-m-underscore", false);
            AssertUnknown("nosetter.pascalcase-m", false);
            AssertUnknown("nosetter.lowercase", false);
            AssertUnknown("nosetter.lowercase-underscore", false);
        }

        [Test]
        public void Unknown_Access_Methods() {
            AssertUnknown("nosetter", true);
            AssertUnknown("property.camelcase", true);
            AssertUnknown("property.camelcase-underscore", true);
            AssertUnknown("property.pascalcase", true);
            AssertUnknown("property.pascalcase-underscore", true);
            AssertUnknown("property.pascalcase-m-underscore", true);
            AssertUnknown("property.pascalcase-m", true);
            AssertUnknown("property.lowercase", true);
            AssertUnknown("property.lowercase-underscore", true);
        }

        private static void AssertUnknown(string access, bool expected) {
            AccessMethod accessMethod = new AccessMethod(access);
            Assert.That(accessMethod.Unknown, Is.EqualTo(expected));
        }

        private static void AccessName(string naming, string oldName, string newName) {
            AccessMethod accessMethod = new AccessMethod(naming);
            Assert.That(accessMethod.Name(oldName), Is.EqualTo(newName));
        }
    }
}