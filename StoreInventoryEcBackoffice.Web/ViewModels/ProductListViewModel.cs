using StoreInventoryEcBackoffice.Web.Models.Entities;

namespace StoreInventoryEcBackoffice.Web.ViewModels;

public class ProductListViewModel
{
    public string? Keyword { get; set; }

    public int? CategoryId { get; set; }

    public string? EcStatus { get; set; }

    public List<ProductCategory> Categories { get; set; } = new();

    public List<string> EcStatuses { get; set; } = new();

    public List<ProductListItemViewModel> Products { get; set; } = new();
}

public class ProductListItemViewModel
{
    public int Id { get; set; }

    public string ProductCode { get; set; } = string.Empty;

    public string? JanCode { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string CategoryName { get; set; } = string.Empty;

    public string ConditionRank { get; set; } = string.Empty;

    public decimal PurchasePrice { get; set; }

    public decimal SellingPrice { get; set; }

    public string EcStatus { get; set; } = string.Empty;

    public int TotalStockQuantity { get; set; }
}
