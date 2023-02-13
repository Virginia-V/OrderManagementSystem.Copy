namespace OMS.Domain
{
    public class PaymentTerm : Entity
    {
        public string PaymentTermsType { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }

    }
}
