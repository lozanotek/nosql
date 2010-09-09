namespace CouchDemo
{
    using System;
    using System.Linq;
    using Divan;

    class Program
    {
        static void Main(string[] args) {
            var dbName = "couch_demo";
            string host = "localhost";
            int port = 5984;

            var server = new CouchServer(host, port);

            if (server.HasDatabase(dbName))
            {
                server.DeleteDatabase(dbName);
            }

            var db = server.GetDatabase(dbName);
            Console.WriteLine("Created database '{0}'", dbName);

            Car car = null;
            for (int i = 0; i < 10; i++)
            {
                car = new Car("Saab", "93", 170 + i);
                db.SaveDocument(car);
            }
            
            Console.WriteLine("Saved 10 Cars with 170-180 hps each.");

            var firstCar = db.GetAllDocuments().First();
            Console.WriteLine("Here is the first of the Cars: \n\n" + firstCar);

            car.HorsePowers = 400;

            db.SaveDocument(car);
            Console.WriteLine("Modified last Car with id " + car.Id);

            var sameCar = db.GetDocument<Car>(car.Id);
            Console.WriteLine("Loaded last Car " + sameCar.Make + " " + sameCar.Model + " now with " + sameCar.HorsePowers + "hps.");

            var cars = db.GetAllDocuments<Car>();
            Console.WriteLine("Loaded all Cars: " + cars.Count());

            var tempView = db.NewTempView("test", "test", "if (doc.docType && doc.docType == 'car') emit(doc.Hps, doc);");
            var linqCars = tempView.LinqQuery<Car>();

            var fastCars = from c in linqCars where c.HorsePowers >= 175 select c;//.Make + " " + c.Model;
            foreach (var fastCar in fastCars)
                Console.WriteLine(fastCar);

            db.DeleteDocument(tempView.Doc);

            foreach (var eachCar in cars.Where(x => x.HorsePowers > 175))
            {
                db.DeleteDocument(eachCar);
                Console.WriteLine("Deleted car with id " + eachCar.Id);
            }

            // Get all cars again and see how many are left.
            Console.WriteLine("Cars left: " + db.GetAllDocuments<Car>().Count());

            Console.Write("\r\nPress any key to close. ");
            Console.ReadKey(false);
        }
    }
}
