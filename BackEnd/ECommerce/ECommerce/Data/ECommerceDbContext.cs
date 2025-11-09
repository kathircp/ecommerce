using Microsoft.EntityFrameworkCore;
using ECommerce.Models;

namespace ECommerce.Data
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<FilterBy> Filters { get; set; } = null!;
        public DbSet<IndexPage> IndexPages { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<Shipment> Shipments { get; set; } = null!;
        public DbSet<Track> Reviews { get; set; } = null!;
        public DbSet<UserDetail> UserDetails { get; set; } = null!;
        public DbSet<Track> Tracks { get; set; } = null!;

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Product>(b =>
        //    {
        //        b.HasKey(p => p.Id);
        //        b.Property(p => p.Name).IsRequired();
        //    });

        //    modelBuilder.Entity<Order>(b =>
        //    {
        //        b.HasKey(o => o.Id);               
        //    });

        //    modelBuilder.Entity<OrderItem>(b =>
        //    {
        //        b.Property(i => i.ProductName).IsRequired();
        //    });

        //    modelBuilder.Entity<User>(b =>
        //    {
        //        b.HasKey(u => u.Id);
        //        b.Property(u => u.Username).IsRequired();
        //        // store roles as json or simple comma-separated string for demo; keep as not mapped here
        //    });
        //}
    }
}