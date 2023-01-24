namespace OMS.Domain
{
    public class PaymentStatus : BaseEntity
    {
        public string Status { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
