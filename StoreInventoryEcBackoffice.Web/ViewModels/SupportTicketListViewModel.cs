namespace StoreInventoryEcBackoffice.Web.ViewModels;

public class SupportTicketListViewModel
{
    public string? Keyword { get; set; }

    public string? Department { get; set; }

    public string? TicketType { get; set; }

    public string? Priority { get; set; }

    public string? Status { get; set; }

    public List<string> Departments { get; set; } = new();

    public List<string> TicketTypes { get; set; } = new();

    public List<string> Priorities { get; set; } = new();

    public List<string> Statuses { get; set; } = new();

    public List<SupportTicketListItemViewModel> Tickets { get; set; } = new();

    public int OpenTicketCount { get; set; }

    public int HighPriorityCount { get; set; }

    public int ChangeRequestCount { get; set; }
}

public class SupportTicketListItemViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;

    public string TicketType { get; set; } = string.Empty;

    public string Priority { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string AssignedUserName { get; set; } = string.Empty;

    public string? RelatedOrderNumber { get; set; }

    public string? RelatedProductCode { get; set; }

    public string? RelatedProductName { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
