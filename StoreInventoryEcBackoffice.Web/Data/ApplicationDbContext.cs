using Microsoft.EntityFrameworkCore;
using StoreInventoryEcBackoffice.Web.Models.Entities;

namespace StoreInventoryEcBackoffice.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Inventory> Inventory => Set<Inventory>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<SupportTicket> SupportTickets => Set<SupportTicket>();
    public DbSet<TicketComment> TicketComments => Set<TicketComment>();
    public DbSet<OperationLog> OperationLogs => Set<OperationLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(255).IsRequired();
            entity.Property(e => e.Role).HasMaxLength(50).IsRequired();
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.Property(e => e.StoreCode).HasMaxLength(20).IsRequired();
            entity.Property(e => e.StoreName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Area).HasMaxLength(50);
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.Property(e => e.CategoryName).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductCode).HasMaxLength(50).IsRequired();
            entity.Property(e => e.JanCode).HasMaxLength(20);
            entity.Property(e => e.ProductName).HasMaxLength(200).IsRequired();
            entity.Property(e => e.ConditionRank).HasMaxLength(10).IsRequired();
            entity.Property(e => e.EcStatus).HasMaxLength(30).IsRequired();

            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(18,2)");
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(18,2)");

            entity.HasOne(e => e.ProductCategory)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.ProductCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasOne(e => e.Store)
                .WithMany(e => e.Inventories)
                .HasForeignKey(e => e.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Product)
                .WithMany(e => e.Inventories)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => new { e.StoreId, e.ProductId })
                .IsUnique();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderNumber).HasMaxLength(50).IsRequired();
            entity.Property(e => e.CustomerName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.OrderStatus).HasMaxLength(30).IsRequired();
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");

            entity.HasOne(e => e.Order)
                .WithMany(e => e.OrderItems)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Product)
                .WithMany(e => e.OrderItems)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<SupportTicket>(entity =>
        {
            entity.Property(e => e.Title).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Department).HasMaxLength(100).IsRequired();
            entity.Property(e => e.TicketType).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Priority).HasMaxLength(20).IsRequired();
            entity.Property(e => e.Status).HasMaxLength(30).IsRequired();
            entity.Property(e => e.Description).IsRequired();

            entity.HasOne(e => e.AssignedUser)
                .WithMany(e => e.AssignedTickets)
                .HasForeignKey(e => e.AssignedUserId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.RelatedOrder)
                .WithMany(e => e.SupportTickets)
                .HasForeignKey(e => e.RelatedOrderId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.RelatedProduct)
                .WithMany(e => e.SupportTickets)
                .HasForeignKey(e => e.RelatedProductId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<TicketComment>(entity =>
        {
            entity.Property(e => e.Comment).IsRequired();

            entity.HasOne(e => e.SupportTicket)
                .WithMany(e => e.TicketComments)
                .HasForeignKey(e => e.SupportTicketId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany(e => e.TicketComments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<OperationLog>(entity =>
        {
            entity.Property(e => e.ActionType).HasMaxLength(50).IsRequired();
            entity.Property(e => e.TargetTable).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Description).IsRequired();

            entity.HasOne(e => e.User)
                .WithMany(e => e.OperationLogs)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
