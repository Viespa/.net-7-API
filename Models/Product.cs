
namespace ApiNet7.Models
{
    public class Product
    {
        // Properties
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime DateOfDischarge { get; set; }
        public bool Active { get; set; }

    }
}