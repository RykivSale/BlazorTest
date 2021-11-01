namespace BlazorTest.Models
{
    public class Car : ICar
    {
        public string ModelName { get; set; }
        public string Color { get; set; }
        public string YearOfConstruction { get; set; }
        public int Mileage { get; set; }
        public ICarCase CarCase { get; set; }
        public IEngine Engine { get; set; }
        public IEquipment Equipment { get; set; }
        public int Tax { get; set; }
        public IGearbox Gearbox { get; set; }
        public string MoreInfo { get; set; }
        public string VinCode { get; set; }
        public string NumberPlate { get; set; }
        public int Cost { get; set; }
        public Car(string modelName, string color, string yearOfConstruction, int mileage, ICarCase carCase, IEngine engine, IEquipment equipment,
            int tax, IGearbox gearbox, string moreinfo, string vinCode, string numberplate, int cost)
        {
            ModelName = modelName;
            Color = color;
            YearOfConstruction = yearOfConstruction;
            Mileage = mileage;
            CarCase = carCase;
            Engine = engine;
            Equipment = equipment;
            Tax = tax;
            Gearbox = gearbox;
            MoreInfo = moreinfo;
            VinCode = vinCode;
            NumberPlate = numberplate;
            Cost = cost;
        }
        public string GetMileage() => Mileage.ToString();
        public string GetCarCase() => CarCase.GetCarCase();
        public string GetEngine() => Engine.GetCarEngine();
        public string GetEquipment() => Equipment.GetCarEquipment();
        public string GetTax() => Tax.ToString();
        public string GetGearbox() => Gearbox.GetCarGearbox();
        public string GetCost() => Cost.ToString();
    }
}
