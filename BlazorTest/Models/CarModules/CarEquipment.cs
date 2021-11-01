using BlazorTest.Models.Enums;

namespace BlazorTest.Models.CarModules
{
    public class CarEquipment : IEquipment
    {
        public CarEquipmentTypes CarIEquipmentType { get; set; }

        public CarEquipment(CarEquipmentTypes equipmentTypes)
        {
            CarIEquipmentType = equipmentTypes;
        }

        public string GetCarEquipment()
        {
            switch (CarIEquipmentType)
            {
                case CarEquipmentTypes.Base:
                    return "Базовый";
                case CarEquipmentTypes.UnBase:
                    return "Не базовый";
                default:
                    break;
            }
            return "";
        }
    }
}
