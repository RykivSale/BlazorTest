using System.Collections.Generic;
using BlazorTest.Models;

namespace BlazorTest.Data.Repository
{
    public interface IRepository
    {
        IEnumerable<Advertisement> GetAdvertisements();
        void AddAdvertisment(string modelName, string pathToImage, string info);
        void DelAdvertisment(string VinCode);
    }
}
