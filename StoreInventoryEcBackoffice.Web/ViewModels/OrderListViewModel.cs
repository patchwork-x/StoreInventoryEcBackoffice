namespace StoreInventoryEcBackoffice.Web.ViewModels;

public class OrderListViewModel
{
    public string? Keyword { get; set; }

    public string? OrderStatus { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public List<string> OrderStatuses { get; set; } = new();

    public List<OrderListItemViewModel> Orders { get; set; } = new();

    public int ShippingWaitingCount { get; set; }

    public decimal TotalAmount { get; set; }
}

public class OrderListItemViewModel
{
    public int Id { get; set; }

    public string OrderNumber { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; }

    public string OrderStatus { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public DateTime? ShippingDueDate { get; set; }

    public DateTime? ShippedAt { get; set; }

    public int ItemCount { get; set; }
}
