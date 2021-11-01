namespace BlazorTest.Models
{
    public class Advertiser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public string Phone_number { get; set; }
        public Advertiser(int id, string name, string surname, string lastname, string phone)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Lastname = lastname;
            Phone_number = phone;
        }
    }
}