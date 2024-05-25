using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using swp391_sap1805_g6.Entities;

namespace swp391_sap1805_g6;

public partial class BanhangContext : DbContext
{
    private string? GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];

        return strConn;
    }
    public BanhangContext()
    {
    }

    public BanhangContext(DbContextOptions<BanhangContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Buyback> Buybacks { get; set; }

    public virtual DbSet<BuybackProduct> BuybackProducts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Dashboard> Dashboards { get; set; }

    public virtual DbSet<GoldPrice> GoldPrices { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Quay> Quays { get; set; }

    public virtual DbSet<ReturnPolicy> ReturnPolicies { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SalesPoint> SalesPoints { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Warranty> Warranties { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admin__43AA41416F1218BD");

            entity.ToTable("Admin");

            entity.HasIndex(e => e.AdminName, "UQ__Admin__37EDA0F78C21B591").IsUnique();

            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.AdminName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("admin_name");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Admins)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Admin__role_id__398D8EEE");
        });

        modelBuilder.Entity<Buyback>(entity =>
        {
            entity.HasKey(e => e.BuybackId).HasName("PK__Buyback__A87F04E90EB3CA3B");

            entity.ToTable("Buyback");

            entity.Property(e => e.BuybackId).HasColumnName("buyback_id");
            entity.Property(e => e.BuybackDate).HasColumnName("buyback_date");
            entity.Property(e => e.BuybackPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("buyback_price");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.OriginalSaleId).HasColumnName("original_sale_id");
            entity.Property(e => e.OriginalSalePrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("original_sale_price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Buybacks)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Buyback__custome__5CD6CB2B");

            entity.HasOne(d => d.OriginalSale).WithMany(p => p.Buybacks)
                .HasForeignKey(d => d.OriginalSaleId)
                .HasConstraintName("FK__Buyback__origina__5EBF139D");

            entity.HasOne(d => d.Product).WithMany(p => p.Buybacks)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Buyback__product__5BE2A6F2");

            entity.HasOne(d => d.Staff).WithMany(p => p.Buybacks)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__Buyback__staff_i__5DCAEF64");
        });

        modelBuilder.Entity<BuybackProduct>(entity =>
        {
            entity.HasKey(e => e.BuybackProductId).HasName("PK__BuybackP__2A7F0BD373B7341C");

            entity.ToTable("BuybackProduct");

            entity.Property(e => e.BuybackProductId).HasColumnName("buyback_product_id");
            entity.Property(e => e.BuybackId).HasColumnName("buyback_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Buyback).WithMany(p => p.BuybackProducts)
                .HasForeignKey(d => d.BuybackId)
                .HasConstraintName("FK__BuybackPr__buyba__628FA481");

            entity.HasOne(d => d.Product).WithMany(p => p.BuybackProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__BuybackPr__produ__619B8048");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__CD65CB85361E1E06");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.LoyaltyPoints)
                .HasDefaultValue(0)
                .HasColumnName("loyalty_points");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Dashboard>(entity =>
        {
            entity.HasKey(e => e.DashboardId).HasName("PK__Dashboar__5E2AEAE670FD2B99");

            entity.ToTable("Dashboard");

            entity.Property(e => e.DashboardId).HasColumnName("dashboard_id");
            entity.Property(e => e.Data)
                .HasColumnType("text")
                .HasColumnName("data");
            entity.Property(e => e.GeneratedAt)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("generated_at");
            entity.Property(e => e.Metrics)
                .HasColumnType("text")
                .HasColumnName("metrics");
        });

        modelBuilder.Entity<GoldPrice>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("PK__GoldPric__1681726D5C7300E5");

            entity.ToTable("GoldPrice");

            entity.Property(e => e.PriceId).HasColumnName("price_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.GoldPrice1)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("gold_price");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__F58DFD491C266847");

            entity.ToTable("Invoice");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.IssueDate).HasColumnName("issue_date");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Order).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Invoice__order_i__656C112C");
        });

        modelBuilder.Entity<LoyaltyProgram>(entity =>
        {
            entity.HasKey(e => e.LoyaltyId).HasName("PK__LoyaltyP__63C28F5C58C80FA2");

            entity.ToTable("LoyaltyProgram");

            entity.Property(e => e.LoyaltyId).HasColumnName("loyalty_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.LastUpdated).HasColumnName("last_updated");
            entity.Property(e => e.PointsAccumulated).HasColumnName("points_accumulated");
            entity.Property(e => e.PointsRedeemed).HasColumnName("points_redeemed");

            entity.HasOne(d => d.Customer).WithMany(p => p.LoyaltyPrograms)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__LoyaltyPr__custo__48CFD27E");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__46596229FBA168C0");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Discount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("discount");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Order__customer___5812160E");

            entity.HasOne(d => d.Staff).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__Order__staff_id__59063A47");
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("PK__Order_Pr__022945F6317ACC86");

            entity.ToTable("Order_Product");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order_Pro__order__6B24EA82");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order_Pro__produ__6C190EBB");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__47027DF5CB344EBC");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Barcode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("barcode");
            entity.Property(e => e.IsBuyback)
                .HasDefaultValue(false)
                .HasColumnName("is_buyback");
            entity.Property(e => e.ManufacturingCost)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("manufacturing_cost");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.StoneCost)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("stone_cost");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type");
            entity.Property(e => e.WarrantyInfo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("warranty_info");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("weight");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__2CB9556B2E366591");

            entity.ToTable("Promotion");

            entity.Property(e => e.PromotionId).HasColumnName("promotion_id");
            entity.Property(e => e.ApprovedBy)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("approved_by");
            entity.Property(e => e.DiscountRate)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("discount_rate");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.IsApproved)
                .HasDefaultValue(false)
                .HasColumnName("is_approved");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.Promotions)
                .HasPrincipalKey(p => p.AdminName)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK__Promotion__appro__3D5E1FD2");

            entity.HasMany(d => d.Orders).WithMany(p => p.Promotions)
                .UsingEntity<Dictionary<string, object>>(
                    "PromotionOrder",
                    r => r.HasOne<Order>().WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Promotion__order__73BA3083"),
                    l => l.HasOne<Promotion>().WithMany()
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Promotion__promo__72C60C4A"),
                    j =>
                    {
                        j.HasKey("PromotionId", "OrderId").HasName("PK__Promotio__A8DCC349149605D4");
                        j.ToTable("Promotion_Order");
                        j.IndexerProperty<int>("PromotionId").HasColumnName("promotion_id");
                        j.IndexerProperty<int>("OrderId").HasColumnName("order_id");
                    });

            entity.HasMany(d => d.Products).WithMany(p => p.Promotions)
                .UsingEntity<Dictionary<string, object>>(
                    "PromotionProduct",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Promotion__produ__778AC167"),
                    l => l.HasOne<Promotion>().WithMany()
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Promotion__promo__76969D2E"),
                    j =>
                    {
                        j.HasKey("PromotionId", "ProductId").HasName("PK__Promotio__68C972B42AA53E23");
                        j.ToTable("Promotion_Product");
                        j.IndexerProperty<int>("PromotionId").HasColumnName("promotion_id");
                        j.IndexerProperty<int>("ProductId").HasColumnName("product_id");
                    });
        });

        modelBuilder.Entity<Quay>(entity =>
        {
            entity.HasKey(e => e.QuayId).HasName("PK__Quay__565E339337E81B13");

            entity.ToTable("Quay");

            entity.Property(e => e.QuayId).HasColumnName("quay_id");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Revenue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("revenue");

            entity.HasMany(d => d.Products).WithMany(p => p.Quays)
                .UsingEntity<Dictionary<string, object>>(
                    "QuayProduct",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Quay_Prod__produ__6FE99F9F"),
                    l => l.HasOne<Quay>().WithMany()
                        .HasForeignKey("QuayId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Quay_Prod__quay___6EF57B66"),
                    j =>
                    {
                        j.HasKey("QuayId", "ProductId").HasName("PK__Quay_Pro__122E144CE75D0B3B");
                        j.ToTable("Quay_Product");
                        j.IndexerProperty<int>("QuayId").HasColumnName("quay_id");
                        j.IndexerProperty<int>("ProductId").HasColumnName("product_id");
                    });
        });

        modelBuilder.Entity<ReturnPolicy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__ReturnPo__47DA3F03461E4F7E");

            entity.ToTable("ReturnPolicy");

            entity.Property(e => e.PolicyId).HasColumnName("policy_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__760965CCF845C223");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<SalesPoint>(entity =>
        {
            entity.HasKey(e => e.SalesPointId).HasName("PK__SalesPoi__A961873A37CD96AA");

            entity.ToTable("SalesPoint");

            entity.Property(e => e.SalesPointId).HasColumnName("sales_point_id");
            entity.Property(e => e.QuayId).HasColumnName("quay_id");
            entity.Property(e => e.SalesDate).HasColumnName("sales_date");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.TotalSales)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_sales");

            entity.HasOne(d => d.Quay).WithMany(p => p.SalesPoints)
                .HasForeignKey(d => d.QuayId)
                .HasConstraintName("FK__SalesPoin__quay___5441852A");

            entity.HasOne(d => d.Staff).WithMany(p => p.SalesPoints)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__SalesPoin__staff__5535A963");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__1963DD9C7AB7BBF3");

            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.QuayId).HasColumnName("quay_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Quay).WithMany(p => p.Staff)
                .HasForeignKey(d => d.QuayId)
                .HasConstraintName("FK__Staff__quay_id__5070F446");

            entity.HasOne(d => d.Role).WithMany(p => p.Staff)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Staff__role_id__5165187F");
        });

        modelBuilder.Entity<Warranty>(entity =>
        {
            entity.HasKey(e => e.WarrantyId).HasName("PK__Warranty__24E65B044D1FDE2A");

            entity.ToTable("Warranty");

            entity.Property(e => e.WarrantyId).HasColumnName("warranty_id");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Warranties)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Warranty__produc__68487DD7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
