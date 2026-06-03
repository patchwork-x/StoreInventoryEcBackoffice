using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreInventoryEcBackoffice.Web.Data;
using StoreInventoryEcBackoffice.Web.Models;
using StoreInventoryEcBackoffice.Web.ViewModels;
using System.Diagnostics;

namespace StoreInventoryEcBackoffice.Web.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var today = DateTime.Today;

        var viewModel = new DashboardViewModel
        {
            TodayOrderCount = await _context.Orders
                .CountAsync(o => o.OrderDate.Date == today),

            ShippingWaitingCount = await _context.Orders
                .CountAsync(o => o.OrderStatus == "受注"
                              || o.OrderStatus == "在庫確認中"
                              || o.OrderStatus == "出荷準備中"),

            OpenTicketCount = await _context.SupportTickets
                .CountAsync(t => t.Status == "未対応"
                              || t.Status == "調査中"
                              || t.Status == "対応中"
                              || t.Status == "確認待ち"),

            EcPublishedProductCount = await _context.Products
                .CountAsync(p => p.EcStatus == "掲載中"),

            TotalInventoryQuantity = await _context.Inventory
                .SumAsync(i => i.Quantity),

            RecentOrders = await _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .ToListAsync(),

            RecentTickets = await _context.SupportTickets
                .Include(t => t.AssignedUser)
                .OrderByDescending(t => t.CreatedAt)
                .Take(5)
                .ToListAsync()
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}
