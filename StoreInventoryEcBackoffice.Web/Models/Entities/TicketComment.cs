namespace StoreInventoryEcBackoffice.Web.Models.Entities;

public class TicketComment
{
    public int Id { get; set; }

    public int SupportTicketId { get; set; }

    public int UserId { get; set; }

    public string Comment { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public SupportTicket? SupportTicket { get; set; }

    public User? User { get; set; }
}
