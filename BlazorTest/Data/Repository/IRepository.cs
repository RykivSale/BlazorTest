using System.Collections.Generic;
using BlazorTest.Models;

namespace BlazorTest.Data.Repository
{
    public interface IRepository
    {
        IEnumerable<Advertisement> GetAdvertisements();
        void AddAdvertisment(Car carstring,Advertiser advertiser, string pathToImage);
        void DelAdvertisment(string VinCode);
    }
}
