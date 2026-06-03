namespace StoreInventoryEcBackoffice.Web.Models.Entities;

public class OperationLog
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string ActionType { get; set; } = string.Empty;

    public string TargetTable { get; set; } = string.Empty;

    public int? TargetId { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public User? User { get; set; }
}
