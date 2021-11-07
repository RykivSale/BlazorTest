using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTest.Models
{
    public class CarInfoPage : Advertisement
    {
        public List<byte[]> Images = new List<byte[]>();
        public string Color { get; set; }
        public string YearOfConstruction { get; set; }
        public string CarCase { get; set; }
        public string Equipment { get; set; }
        public string Tax { get; set; }
        public string Gearbox { get; set; }
        public string MoreInfo { get; set; }
        public string NumberPlate { get; set; }
        public string AdvertName { get; set; }
        public string AdvertSurname { get; set; }
        public string AdvertLastname { get; set; }
        public string AdvertPhone { get; set; }
        public CarInfoPage(Car car,Advertiser advertiser):base(car)
        {
            Color = car.Color;
            YearOfConstruction = car.YearOfConstruction;
            CarCase = car.GetCarCase();
            Equipment = car.GetEquipment();
            Tax = car.GetTax();
            Gearbox = car.GetGearbox();
            MoreInfo = car.MoreInfo;
            VinCode = car.VinCode;
            NumberPlate = car.NumberPlate;
            AdvertName = advertiser.Name;
            AdvertSurname = advertiser.Surname;
            AdvertLastname = advertiser.Lastname;
            AdvertPhone = advertiser.Phone_number;
        }
        public void UploadImages(List<byte[]> images)
        {
            Image = images[0];
            Images.AddRange(images);
        }
    }
}
