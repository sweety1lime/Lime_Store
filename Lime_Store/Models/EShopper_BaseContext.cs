using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Lime_Store.Models
{
    public partial class EShopper_BaseContext : DbContext
    {
        public EShopper_BaseContext()
        {
        }

        public EShopper_BaseContext(DbContextOptions<EShopper_BaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Availability> Availabilities { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Condition> Conditions { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-H0VEMOV\\CLOWN;Initial Catalog=Lime_Shop;Persist Security Info=True;User ID=sa;Password=12345678");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Availability>(entity =>
            {
                entity.ToTable("Availability");

                entity.Property(e => e.AvailabilityId).HasColumnName("Availability_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(e => e.NameBrand)
                    .HasName("PK_Name");

                entity.ToTable("Brand");

                entity.Property(e => e.NameBrand)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Characteristic)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Condition>(entity =>
            {
                entity.ToTable("Condition");

                entity.Property(e => e.ConditionId).HasColumnName("Condition_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("Order_ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Index)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('-')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("Product_ID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("Product_ID");

                entity.Property(e => e.AvailabilityId).HasColumnName("Availability_ID");

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ConditionId).HasColumnName("Condition_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.PictureAddOne)
                    .HasMaxLength(50)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.PictureMain)
                    .HasMaxLength(50)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.Availability)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.AvailabilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Availability");

                entity.HasOne(d => d.BrandNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Brand)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Brand");

                entity.HasOne(d => d.Condition)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ConditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Condition");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
