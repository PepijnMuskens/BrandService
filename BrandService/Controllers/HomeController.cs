﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Text.Json;

namespace BrandService.Controllers
{
    [ApiController]
    [Route("Home/")]
    [EnableCors("CorsPolicy")]
    public class HomeController : Controller
    {
        private string connectionString = "Server=studmysql01.fhict.local;Uid=dbi458416;Database=dbi458416;Pwd=1234";
        //private string connectionString = "server=localhost;user=root;database=pimwoc;port=3306;password='';SslMode=none";
        private BrandController brandController;

        private string query;
        MySqlConnection connection; 
        public HomeController()
        {
            connection = new MySqlConnection(connectionString);
            query = "";
            brandController = new BrandController();

        }
        [HttpGet("Basic")]
        public string Brand()
        {
            Home home = new Home();
            home.Partners = countPartners();
            home.Fields = 200;
            home.Brands = Brands(10);


            foreach(Brand brand in home.Brands)
            {
                home.Totalproducts += brand.ProductCount;
            }

            return JsonSerializer.Serialize(home);


        }

        private List<Brand> Brands(int nr)
        {
            Random random = new Random();
            List<Brand> brands = new List<Brand>();
            try
            {
                connection.Open();
                query = $"SELECT * From brands LIMIT {nr};";
                var cmd = new MySqlCommand(query, connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Brand brand = new Brand("", "");
                    brand.Id = reader.GetInt32(0);
                    brand.Name = reader.GetString(1);
                    brand.Country = reader.GetString(2);
                    brand.ProductCount = random.Next(10, 100);

                    brands.Add(brand);
                }
            }
            catch
            {

            }
            connection.Close();
            return brands;
        }
        private int countPartners()
        {
            try
            {
                connection.Open();
                query = "select COUNT(id) FROM brands;";
                var cmd = new MySqlCommand(query, connection);
                int brands = Convert.ToInt32(cmd.ExecuteScalar());
                query = "select COUNT(id) FROM retailer;";
                cmd = new MySqlCommand(query, connection);
                int retailers = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
                return retailers + brands;
            }
            catch
            {
                connection.Close();
                return 0;
            }
        }
    }
}
