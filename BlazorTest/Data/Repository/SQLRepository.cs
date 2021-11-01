using System;
using System.Collections.Generic;
using BlazorTest.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BlazorTest.Data.Repository
{
    public class SQLRepository : IRepository
    {
        private readonly DB _context;

        public SQLRepository(DB context)
        {
            _context = context;
        }

        public void AddAdvertisment(string modelName, string pathToImage, string info)
        {
            throw new NotImplementedException();
        }

        public void DelAdvertisment(string VinCode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Advertisement> GetAdvertisements()
        {
            using (var conn = new SqlConnection(_context.Database.GetConnectionString()))
            {
                conn.Open();
                List<Advertisement> advs = new List<Advertisement>();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = conn;
                sqlCommand.CommandText =
                    @"Select Cars.Numberplate, CarStat.Model, CarStat.Info From Cars,CarStat Where Cars.VinCode=CarStat.VinCode;";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    advs.Add(new Advertisement(new Car()
                }
                if (!sqlDataReader.IsClosed && sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
                return advs;
            }
        }
    }
}
