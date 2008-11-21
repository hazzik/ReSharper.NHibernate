namespace NHibernatePlugin.Tests.TestCases.WithoutErrors
{
    public class BinaryClass : BaseClassWithId
    {
        public byte[] ByteArray { get; set; }

        public byte[] Binary { get; set; }
    }
}