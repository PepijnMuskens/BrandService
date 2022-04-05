using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Text.Json;

namespace BrandService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : Controller
    {
        private string connectionString = "Server=studmysql01.fhict.local;Uid=dbi458416;Database=dbi458416;Pwd=1234";
        //private string connectionString = "server=localhost;user=root;database=pimwoc;port=3306;password='';SslMode=none";

        private string query;
        MySqlConnection connection; 
        public BrandController()
        {
            connection = new MySqlConnection(connectionString);
            query = "";

        }
        [HttpGet("Brand")]
        public string Brand(string name)
        {
            Brand brand = new Brand("", "");
            try
            {
                connection.Open();
                query = $"SELECT brands.Id, brands.Name, country.English FROM `brands` INNER JOIN country ON brands.Country = country.Id WHERE brands.Name = '{name}'";
                var cmd = new MySqlCommand(query, connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    brand.Id = reader.GetInt32(0);
                    brand.Name = reader.GetString(1);
                    brand.Country = reader.GetString(2);
                }
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
            return JsonSerializer.Serialize(brand);

        }
        [HttpGet("AllBrands")]
        public string Brands()
        {
            List<Brand> brands = new List<Brand>();
            try
            {
                connection.Open();

                query = "SELECT brands.Id, brands.Name, country.English FROM `brands` INNER JOIN country ON brands.Country = country.Id;";
                var cmd = new MySqlCommand(query, connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Brand brand = new Brand("", "");
                    brand.Id = reader.GetInt32(0);
                    brand.Name = reader.GetString(1);
                    brand.Country = reader.GetString(2);

                    brands.Add(brand);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
            return JsonSerializer.Serialize(brands);

        }
    }
}
