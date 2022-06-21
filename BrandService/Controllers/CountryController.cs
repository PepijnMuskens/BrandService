using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Text.Json;

namespace BrandService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : Controller
    {
        private readonly string connectionString = "Server=studmysql01.fhict.local;Uid=dbi458416;Database=dbi458416;Pwd=1234";
        readonly MySqlConnection connection; 
        public CountryController()
        {
            connection = new MySqlConnection(connectionString);
        }
        [HttpGet("Country")]
        public string Brand()
        {
            Dictionary<int,string> countrys = new Dictionary<int,string>();

            try
            {
                connection.Open();
                string query = $"SELECT Id, English FROM `country` ";
                var cmd = new MySqlCommand(query, connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    countrys.Add(reader.GetInt32(0), reader.GetString(1));
                    
                }
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
            return JsonSerializer.Serialize(countrys);

        }
        [HttpPost("up")]
        public int Brands()
        {
            try
            {
                connection.Open();

                string query = "UPDATE `country` SET `English`='America' WHERE Dutch = 'Amerika'";
                var cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                return 1;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
            return 0;

        }
    }
}
