using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.Domain
{
    public class Invoice : BaseEntity
    {
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int DiscountId { get; set; }
        public virtual Discount Discount { get; set; }
        public decimal ShippingAmount { get; set; }
        [NotMapped]
        public decimal InvoicedAmount
        {
            get { return Order.TotalOrderedAmount - Order.TotalOrderedAmount * Discount.DiscountValue + ShippingAmount; }
            private set { InvoicedAmount = value; }
        }
        public int PaymentTermId { get; set; }
        public virtual PaymentTerm PaymentTerm { get; set; }
        public DateTime DueDate { get; set; }
        public int PaymentStatusId { get; set; }
        public virtual PaymentStatus PaymentStatus { get; set; }
    }
}
