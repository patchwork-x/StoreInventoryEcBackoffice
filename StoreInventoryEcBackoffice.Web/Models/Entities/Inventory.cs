namespace StoreInventoryEcBackoffice.Web.Models.Entities;

public class Inventory
{
    public int Id { get; set; }

    public int StoreId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public bool IsEcAvailable { get; set; }

    public DateTime LastStockUpdatedAt { get; set; } = DateTime.Now;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public Store? Store { get; set; }

    public Product? Product { get; set; }
}
