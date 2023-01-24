namespace OMS.Domain
{
    public class PaymentTerm : BaseEntity
    {
        public string PaymentTermsType { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }

    }
}
