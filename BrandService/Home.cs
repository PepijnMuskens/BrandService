namespace BrandService
{
    public class Home
    {
        public int Totalproducts { get; set; }
        public int Partners { get; set; }
        public int Fields { get; set; }
        public List<Brand> Brands { get; set; }

        public Home()
        {
            Brands = new List<Brand>();
        }
    }
}
