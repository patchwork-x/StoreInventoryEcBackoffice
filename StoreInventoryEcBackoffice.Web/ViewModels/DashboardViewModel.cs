using StoreInventoryEcBackoffice.Web.Models.Entities;

namespace StoreInventoryEcBackoffice.Web.ViewModels;

public class DashboardViewModel
{
    public int TodayOrderCount { get; set; }

    public int ShippingWaitingCount { get; set; }

    public int OpenTicketCount { get; set; }

    public int EcPublishedProductCount { get; set; }

    public int TotalInventoryQuantity { get; set; }

    public List<Order> RecentOrders { get; set; } = new();

    public List<SupportTicket> RecentTickets { get; set; } = new();
}
