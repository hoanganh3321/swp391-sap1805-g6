using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using swp391_sap1805_g6.Entities;

namespace swp391_sap1805_g6;

public partial class SaleProject2Context : DbContext
{
    public SaleProject2Context()
    {
    }

    public SaleProject2Context(DbContextOptions<SaleProject2Context> options)
        : base(options)
    {
    }
    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:Conn"];

        return strConn;
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<GoldPrice> GoldPrices { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Quay> Quays { get; set; }

    public virtual DbSet<ReturnPolicy> ReturnPolicies { get; set; }

    public virtual DbSet<Warranty> Warranties { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__CD65CB85D44975E2");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.LoyaltyPoints).HasColumnName("loyalty_points");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C52E0BA83E9E7DB4");

            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Position)
                .HasMaxLength(100)
                .HasColumnName("position");
            entity.Property(e => e.QuayId).HasColumnName("quay_id");

            entity.HasOne(d => d.Quay).WithMany(p => p.Employees)
                .HasForeignKey(d => d.QuayId)
                .HasConstraintName("FK__Employee__quay_i__403A8C7D");
        });

        modelBuilder.Entity<GoldPrice>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("PK__GoldPric__1681726D1DDAF699");

            entity.ToTable("GoldPrice");

            entity.Property(e => e.PriceId)
                .ValueGeneratedNever()
                .HasColumnName("price_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.GoldPrice1)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("gold_price");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__F58DFD497CCDEC08");

            entity.ToTable("Invoice");

            entity.HasIndex(e => e.OrderId, "UQ__Invoice__465962282072F77C").IsUnique();

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.IssueDate).HasColumnName("issue_date");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Order).WithOne(p => p.Invoice)
                .HasForeignKey<Invoice>(d => d.OrderId)
                .HasConstraintName("FK__Invoice__order_i__534D60F1");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__4659622942D86EDF");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Discount)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("discount");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Order__customer___4316F928");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Order__employee___440B1D61");

            entity.HasMany(d => d.Products).WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderProduct",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Order_Pro__produ__4BAC3F29"),
                    l => l.HasOne<Order>().WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Order_Pro__order__4AB81AF0"),
                    j =>
                    {
                        j.HasKey("OrderId", "ProductId").HasName("PK__Order_Pr__022945F643256275");
                        j.ToTable("Order_Product");
                        j.IndexerProperty<int>("OrderId").HasColumnName("order_id");
                        j.IndexerProperty<int>("ProductId").HasColumnName("product_id");
                    });
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__47027DF56A456DB5");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Barcode)
                .HasMaxLength(50)
                .HasColumnName("barcode");
            entity.Property(e => e.GoldPriceId).HasColumnName("gold_price_id");
            entity.Property(e => e.ManufacturingCost)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("manufacturing_cost");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ReturnPolicyId).HasColumnName("return_policy_id");
            entity.Property(e => e.StoneCost)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("stone_cost");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.WarrantyInfo)
                .HasMaxLength(255)
                .HasColumnName("warranty_info");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("weight");

            entity.HasOne(d => d.GoldPrice).WithMany(p => p.Products)
                .HasForeignKey(d => d.GoldPriceId)
                .HasConstraintName("FK__Product__gold_pr__46E78A0C");

            entity.HasOne(d => d.ReturnPolicy).WithMany(p => p.Products)
                .HasForeignKey(d => d.ReturnPolicyId)
                .HasConstraintName("FK__Product__return___47DBAE45");

            entity.HasMany(d => d.Promotions).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductPromotion",
                    r => r.HasOne<Promotion>().WithMany()
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Product_P__promo__571DF1D5"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Product_P__produ__5629CD9C"),
                    j =>
                    {
                        j.HasKey("ProductId", "PromotionId").HasName("PK__Product___E5C9E8A31B50C4CA");
                        j.ToTable("Product_Promotion");
                        j.IndexerProperty<int>("ProductId").HasColumnName("product_id");
                        j.IndexerProperty<int>("PromotionId").HasColumnName("promotion_id");
                    });
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__2CB9556BCF543652");

            entity.ToTable("Promotion");

            entity.Property(e => e.PromotionId)
                .ValueGeneratedNever()
                .HasColumnName("promotion_id");
            entity.Property(e => e.DiscountRate)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("discount_rate");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
        });

        modelBuilder.Entity<Quay>(entity =>
        {
            entity.HasKey(e => e.QuayId).HasName("PK__Quay__565E339342FCF262");

            entity.ToTable("Quay");

            entity.Property(e => e.QuayId).HasColumnName("quay_id");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Revenue)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("revenue");

            entity.HasMany(d => d.Products).WithMany(p => p.Quays)
                .UsingEntity<Dictionary<string, object>>(
                    "QuayProduct",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Quay_Prod__produ__4F7CD00D"),
                    l => l.HasOne<Quay>().WithMany()
                        .HasForeignKey("QuayId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Quay_Prod__quay___4E88ABD4"),
                    j =>
                    {
                        j.HasKey("QuayId", "ProductId").HasName("PK__Quay_Pro__122E144CE3059C63");
                        j.ToTable("Quay_Product");
                        j.IndexerProperty<int>("QuayId").HasColumnName("quay_id");
                        j.IndexerProperty<int>("ProductId").HasColumnName("product_id");
                    });
        });

        modelBuilder.Entity<ReturnPolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__ReturnPo__47DA3F03E6494D3A");

            entity.ToTable("ReturnPolicy");

            entity.Property(e => e.PolicyId)
                .ValueGeneratedNever()
                .HasColumnName("policy_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Warranty>(entity =>
        {
            entity.HasKey(e => e.WarrantyId).HasName("PK__Warranty__24E65B042DF7FFDB");

            entity.ToTable("Warranty");

            entity.HasIndex(e => e.ProductId, "UQ__Warranty__47027DF4C40BC40F").IsUnique();

            entity.Property(e => e.WarrantyId)
                .ValueGeneratedNever()
                .HasColumnName("warranty_id");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Product).WithOne(p => p.Warranty)
                .HasForeignKey<Warranty>(d => d.ProductId)
                .HasConstraintName("FK__Warranty__produc__5AEE82B9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
