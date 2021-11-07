using System;
using System.Collections.Generic;
using System.Data;
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
        public void AddAdvertisment(Car car,Advertiser advertiser, List<byte[]>imageByteForm)    
        {

            advertisers.Add(advertiser);
            cars.Add(car);
            linking_information.Add(new KeyValuePair<int, string>(advertiser.Id, car.VinCode));

            advertisements.Add(new CarInfoPage(car, advertiser));
            int indexnAdv = advertisements.Count - 1;
            advertisements[indexnAdv].UploadImages(imageByteForm);

            
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
            sqlCommand.Parameters.AddWithValue("CarCase", car.CarCase.Name.ToString());
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

            int count = 0;
            foreach (var image in advertisements[indexnAdv].Images)
            {

                sqlCommand.CommandText =
               @"Insert INTO [ImagesOfCars] (VinCode,image) Values (@VinCode,@"+$"image{count})";
                sqlCommand.Parameters.AddWithValue($"image{count}", SqlDbType.VarBinary).Value=image;
                ++count;
                sqlCommand.ExecuteNonQuery();
            }
            sqlCommand.Connection.Close();
        }

        public void DelAdvertisment(string VinCode)
        {
            //var tmpNode = advertisements.Where(x => x.VinCode != VinCode).ToList()[0];
            advertisements.RemoveAll(x => x.VinCode == VinCode);
            cars = cars.Where(x => x.VinCode != VinCode).ToList();
            var tmp = linking_information.Where(y => y.Value == VinCode).ToList()[0];
            linking_information.Remove(tmp);
            advertisers.RemoveAll(x => x.Id == tmp.Key);

            var conn = new SqlConnection(_context.Database.GetConnectionString());
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(
                "Delete From Advertisers Where Advertisers.Id = (Select Cars.AdvertiserId From Cars Where Cars.VinCode = @vincode);",
                conn);
            sqlCommand.Parameters.AddWithValue("vincode", VinCode);
            sqlCommand.ExecuteNonQuery();

            sqlCommand.CommandText =
                "Delete From Cars Where Cars.VinCode=@vincode";
            sqlCommand.ExecuteNonQuery();

            sqlCommand.CommandText =
                "Delete From CarStat Where CarStat.VinCode=@vincode";
            sqlCommand.ExecuteNonQuery();

            sqlCommand.CommandText =
                "Delete From ImagesOfCars Where ImagesOfCars.VinCode=@vincode";
            sqlCommand.ExecuteNonQuery();
            sqlCommand.Connection.Close();
            //            Delete From Advertisers
            //Where Advertisers.Id = (Select Cars.AdvertiserId From Cars Where Cars.VinCode = '222')
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
                string vincode = advertisements[index].VinCode;
                advertisements[index].UploadImages(GetImagesFromDB(vincode));
                ++index;
            }
            return advertisements;
        }
        private List<byte[]> GetImagesFromDB(string vincode)
        {
            List<byte[]> _images = new List<byte[]>();
            using (var conn = new SqlConnection(_context.Database.GetConnectionString()))
            {

                conn.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = conn;
                sqlCommand.CommandText =
                    @"Select ImagesOfCars.image From ImagesOfCars Where ImagesOfCars.VinCode=@vincode";
                sqlCommand.Parameters.AddWithValue("vincode", vincode);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    var hex = (byte[])sqlDataReader["image"];
                    //byte[] tmp = StringToByteArray(hex);
                    _images.Add(hex);
                }
            }
            return _images;
        }
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public IEnumerable<Advertiser> GetAdvertiser()
        {
            return advertisers;
        }

        public IEnumerable<Car> GetCars()
        {
            return cars;

        }

        public IEnumerable<CarInfoPage> GetAllNodes()
        {
            return advertisements;

        }

        public DB GetDB()
        {
            return _context;
        }
    }
}
