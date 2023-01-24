namespace OMS.Common.Dtos.Orders
{
    /// <summary>
    ///  The amount of Sales is the Total Ordered Amount.
    /// </summary>
    public class SalesByCategoryDto
    {
        public string Category { get; set; }
        public decimal Sales { get; set; }
    }
}
