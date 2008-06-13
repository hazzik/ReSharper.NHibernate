namespace NHibernatePlugin.Tests
{
    public class MappedClass
    {
        public enum MeinEnum { Nothing }

        public long Id { get; set; }

        protected string m_Name;
        public string _Name;

        //public string Name {
        //    get { return _Name; }
        //    set { _Name = value; }
        //}

        public MeinEnum EinEnum { get; set; }

        protected Customer m_Customer;
        public Customer Customer {
            get {
                EinEnum = MeinEnum.Nothing;
                return m_Customer;
            }
            set { m_Customer = value; }
        }
    }
}