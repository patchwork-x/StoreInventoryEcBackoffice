using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreInventoryEcBackoffice.Web.Data;
using StoreInventoryEcBackoffice.Web.ViewModels;

namespace StoreInventoryEcBackoffice.Web.Controllers;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? keyword, int? categoryId, string? ecStatus)
    {
        var query = _context.Products
            .Include(p => p.ProductCategory)
            .Include(p => p.Inventories)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(p =>
                p.ProductName.Contains(keyword) ||
                p.ProductCode.Contains(keyword) ||
                (p.JanCode != null && p.JanCode.Contains(keyword)));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.ProductCategoryId == categoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(ecStatus))
        {
            query = query.Where(p => p.EcStatus == ecStatus);
        }

        var products = await query
            .OrderBy(p => p.ProductCode)
            .Select(p => new ProductListItemViewModel
            {
                Id = p.Id,
                ProductCode = p.ProductCode,
                JanCode = p.JanCode,
                ProductName = p.ProductName,
                CategoryName = p.ProductCategory != null ? p.ProductCategory.CategoryName : "",
                ConditionRank = p.ConditionRank,
                PurchasePrice = p.PurchasePrice,
                SellingPrice = p.SellingPrice,
                EcStatus = p.EcStatus,
                TotalStockQuantity = p.Inventories.Sum(i => i.Quantity)
            })
            .ToListAsync();

        var viewModel = new ProductListViewModel
        {
            Keyword = keyword,
            CategoryId = categoryId,
            EcStatus = ecStatus,
            Categories = await _context.ProductCategories
                .OrderBy(c => c.Id)
                .ToListAsync(),
            EcStatuses = await _context.Products
                .Select(p => p.EcStatus)
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync(),
            Products = products
        };

        return View(viewModel);
    }
}
