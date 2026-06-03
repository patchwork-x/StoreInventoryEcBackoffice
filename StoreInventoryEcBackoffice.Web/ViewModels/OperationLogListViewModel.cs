namespace StoreInventoryEcBackoffice.Web.ViewModels;

public class OperationLogListViewModel
{
    public string? Keyword { get; set; }

    public string? ActionType { get; set; }

    public string? TargetTable { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public List<string> ActionTypes { get; set; } = new();

    public List<string> TargetTables { get; set; } = new();

    public List<OperationLogListItemViewModel> Logs { get; set; } = new();

    public int CreateCount { get; set; }

    public int UpdateCount { get; set; }

    public int DeleteCount { get; set; }
}

public class OperationLogListItemViewModel
{
    public int Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string ActionType { get; set; } = string.Empty;

    public string TargetTable { get; set; } = string.Empty;

    public int? TargetId { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
