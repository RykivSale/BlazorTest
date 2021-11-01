using Microsoft.Data.SqlClient;

namespace BlazorTest.Models
{
    public class Advertisement
    {
        public string ModelName { get; set; }
        public byte[] Image { get; set; }
        public string Engine { get; set; }
        public string Cost { get; set; }
        public string Mileage { get; set; }
        public Advertisement(Car car)
        {
            ModelName = car.ModelName;
            Cost = car.GetCost();
            Mileage = car.GetMileage();
            Engine = car.GetEngine();
        }
        public static byte[] UploadImage(string connectionString,string vinCode)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = conn;
                sqlCommand.CommandText =
                    @"Select ImagesOfCars.image From ImagesOfCars Where ImagesOfCars.VinCode=@vinCode;";
                sqlCommand.Parameters.AddWithValue("vinCode", vinCode);
                return (byte[])sqlCommand.ExecuteScalar();
            }
        }
    }
}
