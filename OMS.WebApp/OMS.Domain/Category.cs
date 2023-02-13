namespace OMS.Domain
{
    public class Category : Entity
    {
        public string CategoryName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
