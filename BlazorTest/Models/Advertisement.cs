namespace BlazorTest.Models
{
    public class Advertisement
    {
        public string ModelName { get; set; }
        public byte Image { get; set; }
        public string Engine { get; set; }
        public string Cost { get; set; }
        public string Mileage { get; set; }
        public Advertisement(Car car)
        {
            ModelName = car.ModelName;
            Cost = car.GetCost();
            Mileage = car.GetMileage();
            Engine = car.GetEngine();
        }
    }
}
