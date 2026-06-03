namespace StoreInventoryEcBackoffice.Web.Models.Entities;

public class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public ICollection<SupportTicket> AssignedTickets { get; set; } = new List<SupportTicket>();

    public ICollection<TicketComment> TicketComments { get; set; } = new List<TicketComment>();

    public ICollection<OperationLog> OperationLogs { get; set; } = new List<OperationLog>();
}
