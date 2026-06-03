namespace StoreInventoryEcBackoffice.Web.Models.Entities;

public class Store
{
    public int Id { get; set; }

    public string StoreCode { get; set; } = string.Empty;

    public string StoreName { get; set; } = string.Empty;

    public string? Area { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
