using System.ComponentModel.DataAnnotations.Schema;

namespace OMS.Domain
{
    public class Order : Entity
    {
        public DateTime OrderedAt { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int OrderTypeId { get; set; }
        public virtual OrderType OrderType { get; set; }
        [NotMapped]
        public decimal TotalOrderedAmount
        {
            get { return OrderedProducts.Select(x => x.Quantity * x.Product.ProductPrice).Sum(); }
            private set { TotalOrderedAmount = value; }
        }
        public virtual ICollection<OrderedProduct> OrderedProducts { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
