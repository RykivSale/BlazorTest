using BlazorTest.Models.Enums;

namespace BlazorTest.Models
{
    public interface IEngine
    {
        public double LitrAtKm { get; set; }
        public int Power { get; set; }
        public CarEngineTypes Type { get; set; }
        public string GetCarEngine();
    }
}