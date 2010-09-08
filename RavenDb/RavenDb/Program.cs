namespace RavenDemo
{
    using System;
    using Raven.Client.Document;

    class Program
    {
        static void Main(string[] args)
        {
            var store = new DocumentStore { Url = "http://localhost:8080" };
            store.Initialize();

            using (var session = store.OpenSession())
            {
                var product = new Product
                {
                    Cost = 3.99m,
                    Name = "Milk",
                };

                session.Store(product);
                session.SaveChanges();

                Console.WriteLine("Product: ${0} - {1}", product.Cost, product.Name);

                var order = new Order
                {
                    Customer = "customers/ayende",
                    OrderLines = {
                        new OrderLine {
                            ProductId = product.Id,
                            Quantity = 3
                        }
                    }
                };

                Console.WriteLine("Order: {0} Quantity={1}", 
                    order.Customer,
                    order.OrderLines[0].Quantity);

                session.Store(order);
                session.SaveChanges();
            }

            store.Dispose();

            Console.WriteLine("Press any key...");
            Console.ReadKey(true);
        }
    }
}
