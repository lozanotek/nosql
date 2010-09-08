using System;
using System.Linq;
using System.Text;

namespace MongoDemo
{
    using System.Collections.Generic;
    using Norm;
    using Norm.BSON.DbTypes;

    class Program
    {
        static void Main(string[] args)
        {
            using (var db = Mongo.Create("mongodb://localhost/test"))
            {
                var prodCollection = db.GetCollection<Product>();
                var brandCollection = db.GetCollection<Brand>();

                // create four addresses
                List<Address> places = new List<Address>();
                for (int i = 0; i < 6; i++)
                    places.Add(new Address { StreetName = String.Format("#{0}, Street St", i) });
                // create brands
                Brand acme = new Brand { Name = "Acme Inc.", CustomerRating = 8 };
                acme.Suppliers.Add(places[0]);
                acme.Suppliers.Add(places[1]);
                brandCollection.Save(acme);
                //
                Brand apple = new Brand { Name = "Apple, Inc.", CustomerRating = 7 };
                apple.Suppliers.Add(new Address { StreetName = "Infinite Loop" });
                brandCollection.Save(apple);

                // Create products
                Product rocketBoots = new Product { Price = 100, Shipping = places[4], Name = "Rocket Boots" };
                rocketBoots.TheBrand = new DbReference<Brand>(acme.Id);
                prodCollection.Save(rocketBoots);
                //
                Product explosives = new Product { Price = 10, Shipping = places[2], Name = "Explosives" };
                explosives.TheBrand = new DbReference<Brand>(acme.Id);
                //
                Product iPod = new Product { Price = 350, Shipping = places[5], Name = "iPod Mini" };
                iPod.TheBrand = new DbReference<Brand>(apple.Id);
                prodCollection.Save(iPod);
            }

            using (var db = Mongo.Create("mongodb://localhost/test"))
            {
                var prod = db.GetCollection<Product>();

                // iterate over the products
                foreach (var product in prod.AsQueryable().ToList())
                {
                    // Retrieve a brand from a database reference
                    Brand theBrand = product.TheBrand.Fetch(() => db);
                    //
                    Console.WriteLine("Available: {0} {1}", product, theBrand);
                }
            }

            Console.WriteLine("Press any key....");
            Console.ReadKey(true);
        }
    }
}
