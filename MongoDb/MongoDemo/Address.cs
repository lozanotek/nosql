namespace MongoDemo
{
    using Norm;

    public class Address
    {
        public ObjectId _id { get; set; }
        public string StreetName { get; set; }

        public override string ToString() {
            return StreetName;
        }
    }
}