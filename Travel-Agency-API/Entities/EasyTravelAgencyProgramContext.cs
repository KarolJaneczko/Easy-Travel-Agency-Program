using Microsoft.EntityFrameworkCore;

namespace Travel_Agency_API.Entities {
    public partial class EasyTravelAgencyProgramContext : DbContext {
        public EasyTravelAgencyProgramContext() {
        }

        public EasyTravelAgencyProgramContext(DbContextOptions<EasyTravelAgencyProgramContext> options)
            : base(options) {
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Easy-Travel-Agency-Program;Encrypt=false;Trusted_Connection=true");

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Customer>(entity => {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
                entity.Property(e => e.CustomerBirthdate).HasColumnType("date");
                entity.Property(e => e.CustomerCountry)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.CustomerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.CustomerSurname)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User).WithMany(p => p.Customers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_UserId_User");
            });

            modelBuilder.Entity<Order>(entity => {
                entity.HasKey(e => e.OrdersId);

                entity.Property(e => e.OrdersCity)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.OrdersCountry)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.OrdersDescription)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_CustomerId_Customer");
            });

            modelBuilder.Entity<User>(entity => {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.UserLogin)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.UserPassword)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}