using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreInventoryEcBackoffice.Web.Data;
using StoreInventoryEcBackoffice.Web.ViewModels;

namespace StoreInventoryEcBackoffice.Web.Controllers;

public class InventoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public InventoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(
        string? keyword,
        int? storeId,
        bool? isEcAvailable,
        string? stockStatus)
    {
        var query = _context.Inventory
            .Include(i => i.Store)
            .Include(i => i.Product)
                .ThenInclude(p => p!.ProductCategory)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(i =>
                i.Product != null &&
                (
                    i.Product.ProductName.Contains(keyword) ||
                    i.Product.ProductCode.Contains(keyword) ||
                    (i.Product.JanCode != null && i.Product.JanCode.Contains(keyword))
                ));
        }

        if (storeId.HasValue)
        {
            query = query.Where(i => i.StoreId == storeId.Value);
        }

        if (isEcAvailable.HasValue)
        {
            query = query.Where(i => i.IsEcAvailable == isEcAvailable.Value);
        }

        if (!string.IsNullOrWhiteSpace(stockStatus))
        {
            if (stockStatus == "inStock")
            {
                query = query.Where(i => i.Quantity > 0);
            }
            else if (stockStatus == "outOfStock")
            {
                query = query.Where(i => i.Quantity == 0);
            }
        }

        var inventories = await query
            .OrderBy(i => i.Store!.StoreCode)
            .ThenBy(i => i.Product!.ProductCode)
            .Select(i => new InventoryListItemViewModel
            {
                Id = i.Id,
                StoreCode = i.Store != null ? i.Store.StoreCode : "",
                StoreName = i.Store != null ? i.Store.StoreName : "",
                Area = i.Store != null && i.Store.Area != null ? i.Store.Area : "",
                ProductCode = i.Product != null ? i.Product.ProductCode : "",
                ProductName = i.Product != null ? i.Product.ProductName : "",
                CategoryName = i.Product != null && i.Product.ProductCategory != null
                    ? i.Product.ProductCategory.CategoryName
                    : "",
                ConditionRank = i.Product != null ? i.Product.ConditionRank : "",
                Quantity = i.Quantity,
                IsEcAvailable = i.IsEcAvailable,
                LastStockUpdatedAt = i.LastStockUpdatedAt
            })
            .ToListAsync();

        var viewModel = new InventoryListViewModel
        {
            Keyword = keyword,
            StoreId = storeId,
            IsEcAvailable = isEcAvailable,
            StockStatus = stockStatus,
            Stores = await _context.Stores
                .OrderBy(s => s.StoreCode)
                .Select(s => new InventoryStoreOptionViewModel
                {
                    Id = s.Id,
                    StoreName = s.StoreCode + " / " + s.StoreName
                })
                .ToListAsync(),
            Inventories = inventories
        };

        return View(viewModel);
    }
}
