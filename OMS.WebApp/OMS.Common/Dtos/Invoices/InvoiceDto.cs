namespace OMS.Common.Dtos.Invoices
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string Discount { get; set; }
        public decimal ShippingAmount { get; set; }
        public decimal InvoicedAmount { get; set; }
        public string PaymentTerm { get; set; }
        public DateTime DueDate { get; set; }
        public string PaymentStatus { get; set; }

    }
}
