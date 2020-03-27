using System.Data.Entity;

namespace Anca.Rizan.WebRentC.Core.Models
{

    public partial class RentCDataContext : DbContext
    {
        public RentCDataContext()
            : base("name=RentCDataContext")
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Property(e => e.Plate)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.Manufacturer)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.Model)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.PricePerDay)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Car>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Car)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Coupon>()
                .Property(e => e.CouponCode)
                .IsUnicode(false);

            modelBuilder.Entity<Coupon>()
                .Property(e => e.Discount)
                .HasPrecision(4, 2);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
              .HasMany(e => e.Cars);
              

            modelBuilder.Entity<Permission>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Permission>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.Permissions)
                .Map(m => m.ToTable("RolesPermissions").MapLeftKey("PermissionID").MapRightKey("RoleID"));

            modelBuilder.Entity<Reservation>()
                .Property(e => e.CouponCode)
                .IsUnicode(false);

            modelBuilder.Entity<ReservationStatus>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ReservationStatus>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ReservationStatus>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.ReservationStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
