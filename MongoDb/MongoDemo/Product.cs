namespace MongoDemo
{
    using Norm;
    using Norm.BSON.DbTypes;

    public class Product
    {
        public Product()
        {
            // Default values are to be set here
            Shipping = new Address();
        }
        public ObjectId Id { get; set; }
        public double Price { get; set; }
        public Address Shipping { get; set; }
        public DbReference<Brand> TheBrand { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("\r\nProduct: \r\n\tPrice={0}\r\nShipping={1}\r\nName={2}\r\n", Price, Shipping, Name);
        }
    }
}