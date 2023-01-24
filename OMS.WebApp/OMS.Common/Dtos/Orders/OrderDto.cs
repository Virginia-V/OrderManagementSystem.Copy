namespace OMS.Common.Dtos.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderedAt { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string Customer { get; set; }
        public decimal TotalOrderedAmount { get; set; }
        public string OrderType { get; set; }

    }
}
