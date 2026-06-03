namespace StoreInventoryEcBackoffice.Web.Models.Entities;

public class SupportTicket
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;

    public string TicketType { get; set; } = string.Empty;

    public string Priority { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public int? AssignedUserId { get; set; }

    public int? RelatedOrderId { get; set; }

    public int? RelatedProductId { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public User? AssignedUser { get; set; }

    public Order? RelatedOrder { get; set; }

    public Product? RelatedProduct { get; set; }

    public ICollection<TicketComment> TicketComments { get; set; } = new List<TicketComment>();
}
