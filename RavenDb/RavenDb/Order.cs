namespace RavenDemo {
    using System.Collections.Generic;

    public class Order
    {
        public string Id { get; set; }
        public string Customer { get; set; }
        public IList<OrderLine> OrderLines { get; set; }

        public Order()
        {
            OrderLines = new List<OrderLine>();
        }
    }
}