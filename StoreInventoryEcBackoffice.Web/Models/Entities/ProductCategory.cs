namespace StoreInventoryEcBackoffice.Web.Models.Entities;

public class ProductCategory
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
