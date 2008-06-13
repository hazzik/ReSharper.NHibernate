namespace NHibernatePlugin.Tests
{
    public class Customer
    {
        public string Name { get; set; }

        public Address Address { get; set; }

        public Customer Parent { get; set; }
    }
}