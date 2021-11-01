using BlazorTest.Models.Enums;

namespace BlazorTest.Models.CarModules
{
    public class CarEngine : IEngine
    {
        public double LitrAtKm { get; set; }
        public int Power { get; set; }
        public CarEngineTypes Type { get; set; }

        public CarEngine(double lAk, int power, CarEngineTypes type)
        {
            LitrAtKm = lAk;
            Power = power;
            Type = type;
        }
        public string GetCarEngine()
        {
            switch (Type)
            {
                case CarEngineTypes.Petrol:
                    return $"{LitrAtKm} л / {Power} л.с. / Бензин";
                case CarEngineTypes.Gas:
                    return $"{LitrAtKm} л / {Power} л.с. / Газовый";
                case CarEngineTypes.Electrical:
                    return $"{LitrAtKm} л / {Power} л.с. / Электро";
                case CarEngineTypes.Hybrids:
                    return $"{LitrAtKm} л / {Power} л.с. / Гибридный";
                default:
                    break;
            }
            return "";
        }
    }
}
