using BlazorTest.Models.Enums;

namespace BlazorTest.Models.CarModules
{
    public class Gearbox : IGearbox
    {
        public CarGearboxTypes CarGearboxType { get; set; }

        public Gearbox(CarGearboxTypes carGearboxTypes)
        {
            CarGearboxType = carGearboxTypes;
        }
        public string GetCarGearbox()
        {
            switch (CarGearboxType)
            {
                case CarGearboxTypes.Mechanic:
                    return "Механика";
                case CarGearboxTypes.Automate:
                    return "Автомат";
                default:
                    break;
            }
            return "";
        }
    }
}
