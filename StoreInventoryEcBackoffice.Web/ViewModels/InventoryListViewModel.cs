namespace StoreInventoryEcBackoffice.Web.ViewModels;

public class InventoryListViewModel
{
    public string? Keyword { get; set; }

    public int? StoreId { get; set; }

    public bool? IsEcAvailable { get; set; }

    public string? StockStatus { get; set; }

    public List<InventoryStoreOptionViewModel> Stores { get; set; } = new();

    public List<InventoryListItemViewModel> Inventories { get; set; } = new();
}

public class InventoryStoreOptionViewModel
{
    public int Id { get; set; }

    public string StoreName { get; set; } = string.Empty;
}

public class InventoryListItemViewModel
{
    public int Id { get; set; }

    public string StoreCode { get; set; } = string.Empty;

    public string StoreName { get; set; } = string.Empty;

    public string Area { get; set; } = string.Empty;

    public string ProductCode { get; set; } = string.Empty;

    public string ProductName { get; set; } = string.Empty;

    public string CategoryName { get; set; } = string.Empty;

    public string ConditionRank { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public bool IsEcAvailable { get; set; }

    public DateTime LastStockUpdatedAt { get; set; }
}
