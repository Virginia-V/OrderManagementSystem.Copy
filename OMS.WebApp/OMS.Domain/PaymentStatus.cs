namespace OMS.Domain
{
    public class PaymentStatus : Entity
    {
        public string Status { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
