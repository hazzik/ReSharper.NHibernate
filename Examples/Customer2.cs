namespace Examples
{
    public class Customer2 : Entity
    {
        protected string _Street;
        protected long _Id;

        //public long Id {
        //    get { return _Id; }
        //}
        
        public string Name { get; set; }

        public new long EntityId {
            get { return _Id; }
        }

        public Wrapper<Customer2> GenericSpecial { get; set; }
    }
}