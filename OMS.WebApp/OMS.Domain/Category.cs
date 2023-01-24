namespace OMS.Domain
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
