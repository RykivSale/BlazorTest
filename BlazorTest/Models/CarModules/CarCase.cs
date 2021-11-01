using BlazorTest.Models.Enums;

namespace BlazorTest.Models.CarModules
{

    public class CarCase : ICarCase
    {
        public CarCaseModels Name { get; set; }
        public CarCase(CarCaseModels name)
        {
            Name = name;
        }
        public string GetCarCase()
        {
            switch (Name)
            {
                case CarCaseModels.Sedan: return "Седан";

                case CarCaseModels.Univesal: return "Универсал";

                case CarCaseModels.Hatchback: return "Хэтчбек";

                case CarCaseModels.LiftBack: return "Лифтбек";

                case CarCaseModels.Coop:
                    return "Купе";

                case CarCaseModels.Limyzin:
                    return "Лимузин";

                case CarCaseModels.Cabriolet:
                    return "Кабриолет";

                case CarCaseModels.HardRoad:
                    return "Внедорожник";
            }
            return "";
        }
    }
}
