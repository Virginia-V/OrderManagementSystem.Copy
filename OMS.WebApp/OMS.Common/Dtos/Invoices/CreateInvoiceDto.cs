using System.ComponentModel.DataAnnotations;

namespace OMS.Common.Dtos.Invoices
{
    public class CreateInvoiceDto
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }
        [Required]
        [StringLength(15)]
        public string InvoiceNumber { get; set; }
        [Required]
        [StringLength(15)]
        public string PurchaseOrderNumber { get; set; }
        [Required]
        public int DiscountId { get; set; }
        [Required]
        public decimal ShippingAmount { get; set; }
        [Required]
        public int PaymentTermId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        [Required]
        public int PaymentStatusId { get; set; }
    }
}
