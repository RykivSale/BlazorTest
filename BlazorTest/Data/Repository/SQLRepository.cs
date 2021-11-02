using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BlazorTest.Models;
using BlazorTest.Models.CarModules;
using BlazorTest.Models.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BlazorTest.Data.Repository
{
    public class SQLRepository : IRepository
    {
        private readonly DB _context;

        List<Car> cars = new List<Car>();
        List<Advertiser> advertisers = new List<Advertiser>();
        List<KeyValuePair<int, string>> linking_information = new List<KeyValuePair<int, string>>();
        public List<CarInfoPage> advertisements = new List<CarInfoPage>();
        

        public SQLRepository(DB context)
        {
            _context = context;
            using (var conn = new SqlConnection(_context.Database.GetConnectionString()))
            {
                conn.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = conn;
                sqlCommand.CommandText =
                    @"Select CarStat.*,Cars.Numberplate From Cars,CarStat Where CarStat.VinCode=Cars.VinCode;";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    cars.Add(new Car(
                        sqlDataReader["Model"].ToString(),
                        sqlDataReader["Color"].ToString(),
                        sqlDataReader["YearOfConstruction"].ToString(),
                        Convert.ToInt32(sqlDataReader["Mileage"]),
                        new CarCase((CarCaseModels)Enum.Parse(typeof(CarCaseModels), sqlDataReader["CarCase"].ToString())),
                        new CarEngine(Convert.ToDouble(sqlDataReader["Engine"].ToString().Replace(" ", "").Split("/")[0]), 
                        Convert.ToInt32(sqlDataReader["Engine"].ToString().Replace(" ", "").Split("/")[1]), (CarEngineTypes)Enum.Parse(typeof(CarEngineTypes), sqlDataReader["Engine"].ToString().Replace(" ", "").Split("/")[2])),
                        new CarEquipment((CarEquipmentTypes)Enum.Parse(typeof(CarEquipmentTypes), sqlDataReader["Equipment"].ToString())),
                         Convert.ToInt32(sqlDataReader["Tax"]),
                         new Gearbox((CarGearboxTypes)Enum.Parse(typeof(CarGearboxTypes), sqlDataReader["Gearbox"].ToString())),
                         sqlDataReader["MoreInfo"].ToString(),
                         sqlDataReader["VinCode"].ToString(),
                          sqlDataReader["Numberplate"].ToString(),
                          Convert.ToInt32(sqlDataReader["Cost"])
                          ));
                }
                if (!sqlDataReader.IsClosed && sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
                sqlCommand.CommandText =
                   @"Select Advertisers.* From Advertisers, Cars Where Advertisers.Id=Cars.AdvertiserId;";
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    advertisers.Add(new Advertiser(
                        Convert.ToInt32(sqlDataReader["Id"]),
                        sqlDataReader["Name"].ToString(),
                         sqlDataReader["Surname"].ToString(),
                          sqlDataReader["Lastname"].ToString(),
                           sqlDataReader["Phone_number"].ToString()
                        ));
                }
                if (!sqlDataReader.IsClosed && sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
                sqlCommand.CommandText =
                    @"Select Cars.AdvertiserId,Cars.VinCode From Cars,CarStat Where Cars.VinCode=CarStat.VinCode;";
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    linking_information.Add(new KeyValuePair<int, string>( Convert.ToInt32(sqlDataReader["AdvertiserId"]),
                        sqlDataReader["VinCode"].ToString()));

                }
                if (!sqlDataReader.IsClosed && sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
            }
        }
        public void AddAdvertisment(Car car,Advertiser advertiser, string pathToImage)    
        {

            advertisers.Add(advertiser);
            cars.Add(car);
            linking_information.Add(new KeyValuePair<int, string>(advertiser.Id, car.VinCode));

            advertisements.Add(new CarInfoPage(car, advertiser));
            advertisements[advertisements.Count - 1].Image = File.ReadAllBytes(pathToImage);
            /*advertisements[advertisements.Count - 1].Images.Add(File.ReadAllBytes(pathToImage))*/;
            var conn = new SqlConnection(_context.Database.GetConnectionString());
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(
                $"INSERT INTO [CarStat] (VinCode, Model, Color, Mileage, CarCase, Engine, Equipment,Tax, Gearbox, MoreInfo," +
                $"Cost, YearOfConstruction) " +
                $"VALUES (@VinCode, @Model, @Color, @Mileage, @CarCase, @Engine, @Equipment, @Tax, @Gearbox, @MoreInfo," +
                $"@Cost, @YearOfConstruction)", conn);
            sqlCommand.Parameters.AddWithValue("VinCode", car.VinCode);
            sqlCommand.Parameters.AddWithValue("Model", car.ModelName);
            sqlCommand.Parameters.AddWithValue("Color", car.Color);
            sqlCommand.Parameters.AddWithValue("Mileage", car.Mileage);
            sqlCommand.Parameters.AddWithValue("CarCase", car.Gearbox.CarGearboxType.ToString());
            sqlCommand.Parameters.AddWithValue("Engine", $"{car.Engine.LitrAtKm} / {car.Engine.Power} / {car.Engine.Type.ToString()}");
            sqlCommand.Parameters.AddWithValue("Equipment", car.Equipment.CarIEquipmentType.ToString());
            sqlCommand.Parameters.AddWithValue("Tax", car.Tax);
            sqlCommand.Parameters.AddWithValue("Gearbox", car.Gearbox.CarGearboxType.ToString());
            sqlCommand.Parameters.AddWithValue("MoreInfo", car.MoreInfo);
            sqlCommand.Parameters.AddWithValue("Cost", car.Cost);
            sqlCommand.Parameters.AddWithValue("YearOfConstruction", car.YearOfConstruction);
            sqlCommand.ExecuteNonQuery();


           
            sqlCommand.CommandText =
                @"Insert INTO [Cars] (AdvertiserId,VinCode,Numberplate) Values (@AdvertiserId,@VinCode1,@Numberplate);";
            sqlCommand.Parameters.AddWithValue("VinCode1", car.VinCode);
            sqlCommand.Parameters.AddWithValue("AdvertiserId", advertiser.Id);
            sqlCommand.Parameters.AddWithValue("Numberplate", car.NumberPlate);
            sqlCommand.ExecuteNonQuery();

            //sqlCommand.Connection.Close();

            sqlCommand.CommandText =
                @"Insert INTO [Advertisers] (Id, Name,Surname,Lastname,Phone_number) Values (@Id, @Name,@Surname,@Lastname,@Phone_number);";
            sqlCommand.Parameters.AddWithValue("Id", advertiser.Id);
            sqlCommand.Parameters.AddWithValue("Name", advertiser.Name);
            sqlCommand.Parameters.AddWithValue("Surname", advertiser.Surname);
            sqlCommand.Parameters.AddWithValue("Lastname", advertiser.Lastname);
            sqlCommand.Parameters.AddWithValue("Phone_number", advertiser.Phone_number);
            sqlCommand.ExecuteNonQuery();

            sqlCommand.Connection.Close();
        }

        public void DelAdvertisment(string VinCode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Advertisement> GetAdvertisements()
        {
            advertisements.Clear();
            int index = 0;
            foreach (var link in linking_information)
            {
                var carInfo = from car in cars
                              from advertiser in advertisers
                              where car.VinCode == link.Value
                              where advertiser.Id == link.Key
                              select new CarInfoPage(car, advertiser);


                advertisements.AddRange(carInfo);
                advertisements[index].Image = Advertisement.UploadImage(_context.Database.GetConnectionString(), link.Value);
                ++index;
            }
            return advertisements;
        }
    }
}
