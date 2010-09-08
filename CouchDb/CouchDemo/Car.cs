namespace CouchDemo {
    using Divan;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

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
        #region CouchDocument Members

        public override void WriteJson(JsonWriter writer)
        {
            // This will write id and rev
            base.WriteJson(writer);

            writer.WritePropertyName("docType");
            writer.WriteValue("car");
            writer.WritePropertyName("Make");
            writer.WriteValue(Make);
            writer.WritePropertyName("Model");
            writer.WriteValue(Model);
            writer.WritePropertyName("Hps");
            writer.WriteValue(HorsePowers);
        }

        public override void ReadJson(JObject obj)
        {
            // This will read id and rev
            base.ReadJson(obj);

            Make = obj["Make"].Value<string>();
            Model = obj["Model"].Value<string>();
            HorsePowers = obj["Hps"].Value<int>();
        }

        #endregion
    }
}