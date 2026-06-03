using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreInventoryEcBackoffice.Web.Data;
using StoreInventoryEcBackoffice.Web.ViewModels;

namespace StoreInventoryEcBackoffice.Web.Controllers;

public class OperationLogsController : Controller
{
    private readonly ApplicationDbContext _context;

    public OperationLogsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(
        string? keyword,
        string? actionType,
        string? targetTable,
        DateTime? fromDate,
        DateTime? toDate)
    {
        var query = _context.OperationLogs
            .Include(l => l.User)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(l =>
                l.Description.Contains(keyword) ||
                l.TargetTable.Contains(keyword) ||
                (l.User != null && l.User.UserName.Contains(keyword)));
        }

        if (!string.IsNullOrWhiteSpace(actionType))
        {
            query = query.Where(l => l.ActionType == actionType);
        }

        if (!string.IsNullOrWhiteSpace(targetTable))
        {
            query = query.Where(l => l.TargetTable == targetTable);
        }

        if (fromDate.HasValue)
        {
            query = query.Where(l => l.CreatedAt.Date >= fromDate.Value.Date);
        }

        if (toDate.HasValue)
        {
            query = query.Where(l => l.CreatedAt.Date <= toDate.Value.Date);
        }

        var logs = await query
            .OrderByDescending(l => l.CreatedAt)
            .Select(l => new OperationLogListItemViewModel
            {
                Id = l.Id,
                UserName = l.User != null ? l.User.UserName : "-",
                ActionType = l.ActionType,
                TargetTable = l.TargetTable,
                TargetId = l.TargetId,
                Description = l.Description,
                CreatedAt = l.CreatedAt
            })
            .ToListAsync();

        var viewModel = new OperationLogListViewModel
        {
            Keyword = keyword,
            ActionType = actionType,
            TargetTable = targetTable,
            FromDate = fromDate,
            ToDate = toDate,

            ActionTypes = await _context.OperationLogs
                .Select(l => l.ActionType)
                .Distinct()
                .OrderBy(a => a)
                .ToListAsync(),

            TargetTables = await _context.OperationLogs
                .Select(l => l.TargetTable)
                .Distinct()
                .OrderBy(t => t)
                .ToListAsync(),

            Logs = logs,
            CreateCount = logs.Count(l => l.ActionType == "Create"),
            UpdateCount = logs.Count(l => l.ActionType == "Update"),
            DeleteCount = logs.Count(l => l.ActionType == "Delete")
        };

        return View(viewModel);
    }
}
