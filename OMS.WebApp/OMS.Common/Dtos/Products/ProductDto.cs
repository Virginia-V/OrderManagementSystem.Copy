namespace OMS.Common.Dtos.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductSKU { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
