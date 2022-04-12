using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Text.Json;

namespace BrandService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : Controller
    {
        private string connectionString = "Server=studmysql01.fhict.local;Uid=dbi458416;Database=dbi458416;Pwd=1234";
        //private string connectionString = "server=localhost;user=root;database=pimwoc;port=3306;password='';SslMode=none";

        private string query;
        MySqlConnection connection; 
        public CountryController()
        {
            connection = new MySqlConnection(connectionString);
            query = "";

        }
        [HttpGet("Country")]
        public string Brand()
        {
            Dictionary<int,string> countrys = new Dictionary<int,string>();

            try
            {
                connection.Open();
                query = $"SELECT Id, English FROM `country` ";
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

                query = "UPDATE `country` SET `English`='America' WHERE Dutch = 'Amerika'";
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
