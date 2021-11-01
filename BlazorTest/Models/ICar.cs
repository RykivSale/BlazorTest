namespace BlazorTest.Models
{
    public interface ICar
    {
        public string ModelName { get; set; }
        public string Color { get; set; }
        public string YearOfConstruction { get; set; } //Год выпуска
        public int Mileage { get; set; } //Пробег
        public ICarCase CarCase { get; set; } //Кузов
        public IEngine Engine { get; set; } //Двигатель 
        public IEquipment Equipment { get; set; } //Комплекстация 
        public int Tax { get; set; } //Налог
        public IGearbox Gearbox { get; set; } //Коробка передач 
        public string MoreInfo { get; set; }
        public string VinCode { get; set; }
        public string NumberPlate { get; set; }
        public int Cost { get; set; }
    }
}