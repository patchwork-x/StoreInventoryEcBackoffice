namespace StoreInventoryEcBackoffice.Web.Models.Entities;

public class Order
{
    public int Id { get; set; }

    public string OrderNumber { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; }

    public string OrderStatus { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public DateTime? ShippingDueDate { get; set; }

    public DateTime? ShippedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();
}
