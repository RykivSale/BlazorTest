using System.Collections.Generic;
using BlazorTest.Models;

namespace BlazorTest.Data.Repository
{
    public interface IRepository
    {
        IEnumerable<Advertisement> GetAdvertisements();
        void AddAdvertisment(Car carstring,Advertiser advertiser, string pathToImage);
        void DelAdvertisment(string VinCode);

        IEnumerable<Advertiser> GetAdvertiser();
        IEnumerable<Car> GetCars();
        IEnumerable<CarInfoPage> GetAllNodes();

        DB GetDB();
    }
}
