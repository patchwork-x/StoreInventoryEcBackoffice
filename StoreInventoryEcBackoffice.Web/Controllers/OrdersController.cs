using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreInventoryEcBackoffice.Web.Data;
using StoreInventoryEcBackoffice.Web.ViewModels;

namespace StoreInventoryEcBackoffice.Web.Controllers;

public class OrdersController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrdersController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(
        string? keyword,
        string? orderStatus,
        DateTime? fromDate,
        DateTime? toDate)
    {
        var query = _context.Orders
            .Include(o => o.OrderItems)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(o =>
                o.OrderNumber.Contains(keyword) ||
                o.CustomerName.Contains(keyword));
        }

        if (!string.IsNullOrWhiteSpace(orderStatus))
        {
            query = query.Where(o => o.OrderStatus == orderStatus);
        }

        if (fromDate.HasValue)
        {
            query = query.Where(o => o.OrderDate.Date >= fromDate.Value.Date);
        }

        if (toDate.HasValue)
        {
            query = query.Where(o => o.OrderDate.Date <= toDate.Value.Date);
        }

        var orders = await query
            .OrderByDescending(o => o.OrderDate)
            .Select(o => new OrderListItemViewModel
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                CustomerName = o.CustomerName,
                OrderDate = o.OrderDate,
                OrderStatus = o.OrderStatus,
                TotalAmount = o.TotalAmount,
                ShippingDueDate = o.ShippingDueDate,
                ShippedAt = o.ShippedAt,
                ItemCount = o.OrderItems.Sum(i => i.Quantity)
            })
            .ToListAsync();

        var shippingWaitingStatuses = new[] { "受注", "在庫確認中", "出荷準備中" };

        var viewModel = new OrderListViewModel
        {
            Keyword = keyword,
            OrderStatus = orderStatus,
            FromDate = fromDate,
            ToDate = toDate,
            OrderStatuses = await _context.Orders
                .Select(o => o.OrderStatus)
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync(),
            Orders = orders,
            ShippingWaitingCount = orders.Count(o => shippingWaitingStatuses.Contains(o.OrderStatus)),
            TotalAmount = orders.Sum(o => o.TotalAmount)
        };

        return View(viewModel);
    }
}
