namespace MongoDemo
{
    using System.Collections.Generic;
    using Norm;

    public class Brand
    {
        public Brand()
        {
            Suppliers = new List<Address>();
        }
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int CustomerRating { get; set; }
        public List<Address> Suppliers { get; set; }
        public override string ToString()
        {
            return string.Format("[Brand: Name={0}, CustomerRating={1}]", Name, CustomerRating);
        }
    }
}