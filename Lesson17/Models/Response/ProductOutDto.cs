using Lesson6.ProductsClassRealization;

namespace Lesson17.Models.Response
{
    public class ProductOutDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public float Amount { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
