using BlazorTest.Models.Enums;

namespace BlazorTest.Models
{

    public interface ICarCase
    {
        public CarCaseModels Name { get; set; }
        public string GetCarCase();
    }
}