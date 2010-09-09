namespace CouchDemo
{
    using Divan;

    public class Car : CouchDocument
    {
        public string Make;
        public string Model;
        public int HorsePowers;

        public Car()
        {
            // This constructor is needed by Divan
        }

        public Car(string make, string model, int hps)
        {
            Make = make;
            Model = model;
            HorsePowers = hps;
        }
    }
}