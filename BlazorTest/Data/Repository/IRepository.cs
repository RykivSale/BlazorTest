using System.Collections.Generic;
using BlazorTest.Models;

namespace BlazorTest.Data.Repository
{
    public interface IRepository
    {
        IEnumerable<Advertisement> GetAdvertisements();
        void AddAdvertisment(Car carstring,Advertiser advertiser, List<byte[]> imagesBytesForm);
        void DelAdvertisment(string VinCode);

        IEnumerable<Advertiser> GetAdvertiser();
        IEnumerable<Car> GetCars();
        IEnumerable<CarInfoPage> GetAllNodes();

        DB GetDB();
    }
}
