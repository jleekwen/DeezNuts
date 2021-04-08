using DeezNuts.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DeezNuts.Data
{
    public class DeezNutsContext : DbContext
    {
        public DbContextOptions<DeezNutsContext> options { get; }

        public DeezNutsContext(DbContextOptions<DeezNutsContext> options) : base(options)
        {
            this.options = options;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<CustomerSession> CustomerSessions { get; set; }

        public DbSet<ListeningAction> ListeningActions { get; set; }
        public DbSet<TypedListeningAction> TypedListeningActions { get; set; }
        public DbSet<GeneralListeningAction> GeneralListeningActions { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<TypedMessage> TypedMessages { get; set; }

        public DbSet<MessageLog> MessageLogs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<CustomerSession>().ToTable("CustomerSessions");
            modelBuilder.Entity<CustomerAddress>().ToTable("CustomerAddresses");

            modelBuilder.Entity<ListeningAction>().ToTable("ListeningActions");
            modelBuilder.Entity<TypedListeningAction>();
            modelBuilder.Entity<GeneralListeningAction>();

            modelBuilder.Entity<Message>().ToTable("Messages");
            modelBuilder.Entity<TypedMessage>();

            modelBuilder.Entity<MessageLog>().ToTable("MessageLogs")
                .HasIndex(m => m.TwilioMessageSid)
                .IsUnique();

            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<ProductPrice>().ToTable("ProductPrices");
            modelBuilder.Entity<Setting>().ToTable("Settings");
        }
    }
}
