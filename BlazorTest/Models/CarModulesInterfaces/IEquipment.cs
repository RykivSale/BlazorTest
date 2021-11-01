using BlazorTest.Models.Enums;

namespace BlazorTest.Models
{
    public interface IEquipment
    {
        public CarEquipmentTypes CarIEquipmentType { get; set; }
        public string GetCarEquipment();
    }
}