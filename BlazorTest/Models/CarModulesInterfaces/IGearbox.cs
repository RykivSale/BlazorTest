using BlazorTest.Models.Enums;

namespace BlazorTest.Models
{
    public interface IGearbox
    {
        public CarGearboxTypes CarGearboxType { get; set; }
        public string GetCarGearbox();
    }
}