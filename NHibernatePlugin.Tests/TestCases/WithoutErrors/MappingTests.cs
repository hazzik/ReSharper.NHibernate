using NHibernate.Cfg;
using NUnit.Framework;

namespace NHibernatePlugin.Tests.TestCases.WithoutErrors
{
    [TestFixture]
    public class MappingTests
    {
        private Configuration configuration;

        [SetUp]
        public void Setup() {
            configuration = new Configuration();
            configuration.SetProperty(Environment.Dialect, "NHibernate.Dialect.MsSql2000Dialect");
        }

        [Test]
        [Explicit("What's wrong with type='Binary'?")]
        public void Binary() {
            Verify("BinaryClass.hbm.xml");
        }

        [Test]
        public void AnsiString() {
            Verify("AnsiStringClass.hbm.xml");
        }

        [Test]
        public void Boolean() {
            Verify("BooleanClass.hbm.xml");
        }

        private void Verify(string mappingFileResourceName) {
            configuration.AddResource(string.Format("NHibernatePlugin.Tests.TestCases.WithoutErrors.{0}", mappingFileResourceName), GetType().Assembly);
            configuration.BuildSessionFactory();
        }
    }
}