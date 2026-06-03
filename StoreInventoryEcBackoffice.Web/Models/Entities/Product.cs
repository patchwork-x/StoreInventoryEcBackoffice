namespace StoreInventoryEcBackoffice.Web.Models.Entities;

public class Product
{
    public int Id { get; set; }

    public string ProductCode { get; set; } = string.Empty;

    public string? JanCode { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int ProductCategoryId { get; set; }

    public string ConditionRank { get; set; } = string.Empty;

    public decimal PurchasePrice { get; set; }

    public decimal SellingPrice { get; set; }

    public string EcStatus { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public ProductCategory? ProductCategory { get; set; }

    public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();
}
