using OMS.Domain;
using Microsoft.EntityFrameworkCore;

namespace OMS.Dal
{
    public class OMSDbContext : DbContext
    {
        public OMSDbContext(DbContextOptions<OMSDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OMSDbContext).Assembly);
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderType> OrderTypes { get; set; }
        public virtual DbSet<OrderedProduct> OrderedProducts { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<PaymentTerm> PaymentTerms { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }
    }
}
