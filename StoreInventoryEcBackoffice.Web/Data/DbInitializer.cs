using StoreInventoryEcBackoffice.Web.Models.Entities;

namespace StoreInventoryEcBackoffice.Web.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Users.Any())
        {
            return;
        }

        var users = new User[]
        {
            new User
            {
                UserName = "管理者",
                Email = "admin@example.com",
                Role = "Admin"
            },
            new User
            {
                UserName = "EC担当者",
                Email = "ec-user@example.com",
                Role = "EcStaff"
            },
            new User
            {
                UserName = "店舗担当者",
                Email = "store-user@example.com",
                Role = "StoreStaff"
            }
        };

        context.Users.AddRange(users);
        context.SaveChanges();

        var stores = new Store[]
        {
            new Store
            {
                StoreCode = "S001",
                StoreName = "名古屋大須店",
                Area = "中部",
                Address = "愛知県名古屋市中区",
                PhoneNumber = "052-000-0001"
            },
            new Store
            {
                StoreCode = "S002",
                StoreName = "池袋東口店",
                Area = "関東",
                Address = "東京都豊島区",
                PhoneNumber = "03-0000-0002"
            },
            new Store
            {
                StoreCode = "S003",
                StoreName = "大阪日本橋店",
                Area = "関西",
                Address = "大阪府大阪市浪速区",
                PhoneNumber = "06-0000-0003"
            }
        };

        context.Stores.AddRange(stores);
        context.SaveChanges();

        var categories = new ProductCategory[]
        {
            new ProductCategory { CategoryName = "ゲーム" },
            new ProductCategory { CategoryName = "スマートフォン" },
            new ProductCategory { CategoryName = "家電" },
            new ProductCategory { CategoryName = "ブランド品" },
            new ProductCategory { CategoryName = "古着" }
        };

        context.ProductCategories.AddRange(categories);
        context.SaveChanges();

        var products = new Product[]
        {
            new Product
            {
                ProductCode = "P0001",
                JanCode = "490000000001",
                ProductName = "中古ゲームソフト A",
                ProductCategoryId = categories[0].Id,
                ConditionRank = "A",
                PurchasePrice = 1200,
                SellingPrice = 2480,
                EcStatus = "掲載中"
            },
            new Product
            {
                ProductCode = "P0002",
                JanCode = "490000000002",
                ProductName = "中古スマートフォン B",
                ProductCategoryId = categories[1].Id,
                ConditionRank = "B",
                PurchasePrice = 12000,
                SellingPrice = 19800,
                EcStatus = "掲載中"
            },
            new Product
            {
                ProductCode = "P0003",
                JanCode = "490000000003",
                ProductName = "中古ヘッドホン C",
                ProductCategoryId = categories[2].Id,
                ConditionRank = "A",
                PurchasePrice = 3000,
                SellingPrice = 5980,
                EcStatus = "未掲載"
            },
            new Product
            {
                ProductCode = "P0004",
                JanCode = "490000000004",
                ProductName = "ブランドバッグ D",
                ProductCategoryId = categories[3].Id,
                ConditionRank = "B",
                PurchasePrice = 18000,
                SellingPrice = 29800,
                EcStatus = "掲載中"
            },
            new Product
            {
                ProductCode = "P0005",
                JanCode = "490000000005",
                ProductName = "古着ジャケット E",
                ProductCategoryId = categories[4].Id,
                ConditionRank = "C",
                PurchasePrice = 800,
                SellingPrice = 1980,
                EcStatus = "掲載停止"
            }
        };

        context.Products.AddRange(products);
        context.SaveChanges();

        var inventories = new Inventory[]
        {
            new Inventory
            {
                StoreId = stores[0].Id,
                ProductId = products[0].Id,
                Quantity = 3,
                IsEcAvailable = true
            },
            new Inventory
            {
                StoreId = stores[0].Id,
                ProductId = products[1].Id,
                Quantity = 1,
                IsEcAvailable = true
            },
            new Inventory
            {
                StoreId = stores[0].Id,
                ProductId = products[2].Id,
                Quantity = 5,
                IsEcAvailable = false
            },
            new Inventory
            {
                StoreId = stores[1].Id,
                ProductId = products[0].Id,
                Quantity = 2,
                IsEcAvailable = true
            },
            new Inventory
            {
                StoreId = stores[1].Id,
                ProductId = products[3].Id,
                Quantity = 1,
                IsEcAvailable = true
            },
            new Inventory
            {
                StoreId = stores[2].Id,
                ProductId = products[4].Id,
                Quantity = 8,
                IsEcAvailable = false
            }
        };

        context.Inventory.AddRange(inventories);
        context.SaveChanges();

        var orders = new Order[]
        {
            new Order
            {
                OrderNumber = "ORD-20260603-001",
                CustomerName = "山田 太郎",
                OrderDate = new DateTime(2026, 6, 3, 10, 0, 0),
                OrderStatus = "受注",
                TotalAmount = 2480,
                ShippingDueDate = new DateTime(2026, 6, 5)
            },
            new Order
            {
                OrderNumber = "ORD-20260603-002",
                CustomerName = "佐藤 花子",
                OrderDate = new DateTime(2026, 6, 3, 11, 30, 0),
                OrderStatus = "在庫確認中",
                TotalAmount = 19800,
                ShippingDueDate = new DateTime(2026, 6, 5)
            },
            new Order
            {
                OrderNumber = "ORD-20260602-001",
                CustomerName = "鈴木 一郎",
                OrderDate = new DateTime(2026, 6, 2, 15, 20, 0),
                OrderStatus = "出荷済み",
                TotalAmount = 29800,
                ShippingDueDate = new DateTime(2026, 6, 4),
                ShippedAt = new DateTime(2026, 6, 3, 9, 30, 0)
            }
        };

        context.Orders.AddRange(orders);
        context.SaveChanges();

        var orderItems = new OrderItem[]
        {
            new OrderItem
            {
                OrderId = orders[0].Id,
                ProductId = products[0].Id,
                Quantity = 1,
                UnitPrice = 2480
            },
            new OrderItem
            {
                OrderId = orders[1].Id,
                ProductId = products[1].Id,
                Quantity = 1,
                UnitPrice = 19800
            },
            new OrderItem
            {
                OrderId = orders[2].Id,
                ProductId = products[3].Id,
                Quantity = 1,
                UnitPrice = 29800
            }
        };

        context.OrderItems.AddRange(orderItems);
        context.SaveChanges();

        var tickets = new SupportTicket[]
        {
            new SupportTicket
            {
                Title = "注文商品の在庫確認依頼",
                Department = "EC運営部",
                TicketType = "問い合わせ",
                Priority = "高",
                Status = "調査中",
                AssignedUserId = users[1].Id,
                RelatedOrderId = orders[1].Id,
                RelatedProductId = products[1].Id,
                Description = "注文後に店舗在庫との不一致が発生したため、在庫状況を確認する。"
            },
            new SupportTicket
            {
                Title = "商品一覧画面の検索条件追加依頼",
                Department = "店舗運営部",
                TicketType = "改修依頼",
                Priority = "中",
                Status = "未対応",
                AssignedUserId = users[0].Id,
                Description = "商品一覧画面に状態ランクでの絞り込み条件を追加したい。"
            },
            new SupportTicket
            {
                Title = "出荷済み注文のステータス確認",
                Department = "カスタマーサポート",
                TicketType = "問い合わせ",
                Priority = "低",
                Status = "完了",
                AssignedUserId = users[1].Id,
                RelatedOrderId = orders[2].Id,
                RelatedProductId = products[3].Id,
                Description = "顧客から出荷状況について問い合わせがあったため確認。"
            }
        };

        context.SupportTickets.AddRange(tickets);
        context.SaveChanges();

        var comments = new TicketComment[]
        {
            new TicketComment
            {
                SupportTicketId = tickets[0].Id,
                UserId = users[1].Id,
                Comment = "店舗在庫とEC掲載情報を確認中。"
            },
            new TicketComment
            {
                SupportTicketId = tickets[2].Id,
                UserId = users[1].Id,
                Comment = "出荷済みであることを確認し、サポートセンターへ回答済み。"
            }
        };

        context.TicketComments.AddRange(comments);
        context.SaveChanges();

        var logs = new OperationLog[]
        {
            new OperationLog
            {
                UserId = users[0].Id,
                ActionType = "Create",
                TargetTable = "Products",
                TargetId = products[0].Id,
                Description = "商品マスタを登録しました。"
            },
            new OperationLog
            {
                UserId = users[1].Id,
                ActionType = "Update",
                TargetTable = "Orders",
                TargetId = orders[1].Id,
                Description = "注文ステータスを受注から在庫確認中に変更しました。"
            },
            new OperationLog
            {
                UserId = users[1].Id,
                ActionType = "Update",
                TargetTable = "SupportTickets",
                TargetId = tickets[0].Id,
                Description = "問い合わせステータスを未対応から調査中に変更しました。"
            }
        };

        context.OperationLogs.AddRange(logs);
        context.SaveChanges();
    }
}
