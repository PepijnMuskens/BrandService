using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Text.Json;

namespace BrandService.Controllers
{
    [ApiController]
    [Route("Brand/")]
    
    public class BrandController : Controller
    {
       
        private readonly string connectionString = "Server=studmysql01.fhict.local;Uid=dbi458416;Database=dbi458416;Pwd=1234";
        readonly MySqlConnection connection; 
        public BrandController()
        {
            connection = new MySqlConnection(connectionString);

        }
        [HttpGet("Brand")]
        public string Brand(string name)
        {
            Brand brand;
            try
            {
                connection.Open();
                string query = $"SELECT brands.Id, brands.Name, country.English FROM `brands` INNER JOIN country ON brands.Country = country.Id WHERE brands.Name = '{name}'";
                var cmd = new MySqlCommand(query, connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    brand = new Brand(reader.GetString(1), reader.GetString(2), (byte[])reader.GetValue(3));
                    brand.Id = reader.GetInt32(0);
                    return JsonSerializer.Serialize(brand);
                }
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
            return JsonSerializer.Serialize(new Brand());

        }
        
        [HttpGet("Brands")]
        public string Brands()
        {
            List<Brand> brands = new List<Brand>();
            try
            {
                connection.Open();

                string query = "SELECT brands.Id, brands.Name, country.English, icons.Icon FROM `brands` INNER JOIN country ON brands.Country = country.Id INNER JOIN icons ON brands.Icon = icons.Id;";
                var cmd = new MySqlCommand(query, connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   
                    Brand brand = new Brand(reader.GetString(1), reader.GetString(2), (byte[])reader.GetValue(3));
                    brand.Id = reader.GetInt32(0);
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
