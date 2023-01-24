namespace OMS.Domain
{
    public class Discount : BaseEntity
    {
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
