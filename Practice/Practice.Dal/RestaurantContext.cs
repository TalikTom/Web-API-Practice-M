using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;


namespace Practice.Dal
{
    public partial class RestaurantContext : DbContext
    {
        public RestaurantContext()
            : base("name=RestaurantContext")
        {
        }

        public virtual DbSet<Chef> Chef { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerDetails> CustomerDetails { get; set; }
        public virtual DbSet<CustomerOrder> CustomerOrder { get; set; }
        public virtual DbSet<CustomerOrderItem> CustomerOrderItem { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuItem> MenuItem { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<Waiter> Waiter { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chef>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Chef>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Chef>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Chef>()
                .Property(e => e.HomeAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Chef>()
                .Property(e => e.OIB)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Chef>()
                .HasMany(e => e.CustomerOrder)
                .WithRequired(e => e.Chef)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasOptional(e => e.CustomerDetails)
                .WithRequired(e => e.Customer);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.CustomerOrder)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Reservation)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CustomerDetails>()
                .Property(e => e.Commentary)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerOrder>()
                .HasMany(e => e.CustomerOrderItem)
                .WithRequired(e => e.CustomerOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CustomerOrder>()
                .HasOptional(e => e.Payment)
                .WithRequired(e => e.CustomerOrder);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.MenuItem)
                .WithRequired(e => e.Menu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MenuItem>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<MenuItem>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<OrderItem>()
                .HasMany(e => e.CustomerOrderItem)
                .WithRequired(e => e.OrderItem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderItem>()
                .HasMany(e => e.MenuItem)
                .WithRequired(e => e.OrderItem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment>()
                .Property(e => e.PaymentAmount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Waiter>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Waiter>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Waiter>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Waiter>()
                .Property(e => e.HomeAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Waiter>()
                .Property(e => e.OIB)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Waiter>()
                .HasMany(e => e.CustomerOrder)
                .WithRequired(e => e.Waiter)
                .WillCascadeOnDelete(false);
        }

        
    }
}
