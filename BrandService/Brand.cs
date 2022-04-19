namespace BrandService
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Country { get; set; }
        public int ProductCount { get; set; }
        public byte[] Icon { get; set; }

        public Brand(string name, string country, byte[] icon)
        {
            Name = name;
            Country = country;
            Icon = icon;
        }
        public Brand()
        {
            Name = "";
            Country = "";
            Icon = new byte[0];
        }
    }
}
