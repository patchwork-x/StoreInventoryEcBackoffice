using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreInventoryEcBackoffice.Web.Data;
using StoreInventoryEcBackoffice.Web.ViewModels;

namespace StoreInventoryEcBackoffice.Web.Controllers;

public class SupportTicketsController : Controller
{
    private readonly ApplicationDbContext _context;

    public SupportTicketsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(
        string? keyword,
        string? department,
        string? ticketType,
        string? priority,
        string? status)
    {
        var query = _context.SupportTickets
            .Include(t => t.AssignedUser)
            .Include(t => t.RelatedOrder)
            .Include(t => t.RelatedProduct)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(t =>
                t.Title.Contains(keyword) ||
                t.Description.Contains(keyword));
        }

        if (!string.IsNullOrWhiteSpace(department))
        {
            query = query.Where(t => t.Department == department);
        }

        if (!string.IsNullOrWhiteSpace(ticketType))
        {
            query = query.Where(t => t.TicketType == ticketType);
        }

        if (!string.IsNullOrWhiteSpace(priority))
        {
            query = query.Where(t => t.Priority == priority);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(t => t.Status == status);
        }

        var tickets = await query
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new SupportTicketListItemViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Department = t.Department,
                TicketType = t.TicketType,
                Priority = t.Priority,
                Status = t.Status,
                AssignedUserName = t.AssignedUser != null ? t.AssignedUser.UserName : "-",
                RelatedOrderNumber = t.RelatedOrder != null ? t.RelatedOrder.OrderNumber : null,
                RelatedProductCode = t.RelatedProduct != null ? t.RelatedProduct.ProductCode : null,
                RelatedProductName = t.RelatedProduct != null ? t.RelatedProduct.ProductName : null,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            })
            .ToListAsync();

        var openStatuses = new[] { "未対応", "調査中", "対応中", "確認待ち" };

        var viewModel = new SupportTicketListViewModel
        {
            Keyword = keyword,
            Department = department,
            TicketType = ticketType,
            Priority = priority,
            Status = status,

            Departments = await _context.SupportTickets
                .Select(t => t.Department)
                .Distinct()
                .OrderBy(d => d)
                .ToListAsync(),

            TicketTypes = await _context.SupportTickets
                .Select(t => t.TicketType)
                .Distinct()
                .OrderBy(t => t)
                .ToListAsync(),

            Priorities = await _context.SupportTickets
                .Select(t => t.Priority)
                .Distinct()
                .OrderBy(p => p)
                .ToListAsync(),

            Statuses = await _context.SupportTickets
                .Select(t => t.Status)
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync(),

            Tickets = tickets,
            OpenTicketCount = tickets.Count(t => openStatuses.Contains(t.Status)),
            HighPriorityCount = tickets.Count(t => t.Priority == "高"),
            ChangeRequestCount = tickets.Count(t => t.TicketType == "改修依頼")
        };

        return View(viewModel);
    }
}
