using Microsoft.EntityFrameworkCore;
using miniprojectE.Data;
using miniprojectE.DTO.OrderDTOs;
using miniprojectE.DTO.ReportingDTOs;
using miniprojectE.DTO.StockDTOs;
using miniprojectE.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.Services
{
    public class ReportService: IReportService
    {
        private readonly AppDB _context;

        public ReportService(AppDB context)
        {
            _context = context;
        }

        public async Task<StockReportDTO> GetStockReportAsync()
        {
            try
            {
                var allComponents = await _context.Component
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.Name)
                    .Select(c => new StockLevelDTO
                    {
                        ComponentId = c.ComponentID,
                        ComponentName = c.Name,
                        ComponentType = c.Type,
                        CurrentStock = c.Level,
                        MinimumStockLevel = c.MinimumLevel,
                        ReorderPoint = c.MinimumLevel + 5, // Reorder when 5 above minimum
                        ReorderQuantity = c.MinimumLevel * 2, // Order double the minimum
                        UnitPrice = c.UnitPrice,
                        IsActive = c.IsActive,
                        PrimarySupplier = "Default Supplier", // You can add this to Component model if needed
                        LeadTimeDays = 7, // Default lead time
                        AverageMonthlyUsage = 0 // Will be calculated separately if needed
                    })
                    .ToListAsync();

                var lowStockItems = allComponents
                    .Where(c => c.IsLowStock)
                    .OrderBy(c => c.CurrentStock)
                    .ToList();

                var report = new StockReportDTO
                {
                    AllComponents = allComponents,
                    LowStockItems = lowStockItems,
                    TotalComponents = allComponents.Count,
                    LowStockCount = lowStockItems.Count,
                    GeneratedAt = DateTime.UtcNow
                };

                return report;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<StockLevelDTO>> GetLowStockComponentsAsync()
        {
            try
            {
                var lowStockComponents = await _context.Component
                    .Where(c => c.IsActive && c.Level <= c.MinimumLevel)
                    .OrderBy(c => c.Level)
                    .ThenBy(c => c.Name)
                    .Select(c => new StockLevelDTO
                    {
                        ComponentId = c.ComponentID,
                        ComponentName = c.Name,
                        ComponentType = c.Type,
                        CurrentStock = c.Level,
                        MinimumStockLevel = c.MinimumLevel,
                        ReorderPoint = c.MinimumLevel + 5,
                        ReorderQuantity = c.MinimumLevel * 2,
                        UnitPrice = c.UnitPrice,
                        IsActive = c.IsActive,
                        PrimarySupplier = "Default Supplier",
                        LeadTimeDays = 7,
                        AverageMonthlyUsage = 0
                    })
                    .ToListAsync();

                return lowStockComponents;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PopularityReportDTO> GetPopularityReportAsync(int year, int month)
        {
            try
            {
                // Validate date parameters
                if (month < 1 || month > 12)
                    throw new ValidationException("Month must be between 1 and 12");

                if (year < 2000 || year > DateTime.UtcNow.Year + 1)
                    throw new ValidationException($"Year must be between 2000 and {DateTime.UtcNow.Year + 1}");

                // Check if we have popularity data for this period
                var existingData = await _context.ComponentPopularity
                    .Where(cp => cp.PeriodYear == year && cp.PeriodMonth == month)
                    .ToListAsync();

                // If no data exists, generate it
                if (!existingData.Any())
                {
                    await GeneratePopularityDataForPeriodAsync(year, month);
                    existingData = await _context.ComponentPopularity
                        .Where(cp => cp.PeriodYear == year && cp.PeriodMonth == month)
                        .ToListAsync();
                }

                var componentPopularity = await _context.ComponentPopularity
                    .Include(cp => cp.Component)
                    .Where(cp => cp.PeriodYear == year && cp.PeriodMonth == month)
                    .OrderByDescending(cp => cp.TotalRevenue)
                    .Select(cp => new ComponentPopularityDTO
                    {
                        ComponentId = cp.ComponentID,
                        ComponentName = cp.Component.Name,
                        ComponentType = cp.Component.Type,
                        TimesOrdered = (int)cp.TimesOrdered,
                        TotalQuantitySold = (int)cp.TotalQuantitySold,
                        TotalRevenue = (decimal)cp.TotalRevenue,
                        PeriodYear = cp.PeriodYear,
                        PeriodMonth = cp.PeriodMonth,
                        CurrentUnitPrice = cp.Component.UnitPrice,
                        AverageSellingPrice = (decimal)(cp.TimesOrdered > 0 ? cp.TotalRevenue / cp.TotalQuantitySold : 0),
                        PopularityRank = 0, // Will be set after ordering
                        IsActive = cp.Component.IsActive
                    })
                    .ToListAsync();

                // Assign popularity ranks
                for (int i = 0; i < componentPopularity.Count; i++)
                {
                    componentPopularity[i].PopularityRank = i + 1;
                }

                // Calculate previous month for percentage change
                var previousMonth = month - 1;
                var previousYear = year;
                if (previousMonth < 1)
                {
                    previousMonth = 12;
                    previousYear--;
                }

                var previousData = await _context.ComponentPopularity
                    .Where(cp => cp.PeriodYear == previousYear && cp.PeriodMonth == previousMonth)
                    .ToListAsync();

                // Calculate percentage changes
                foreach (var item in componentPopularity)
                {
                    var previousItem = previousData.FirstOrDefault(p => p.ComponentID == item.ComponentId);
                    if (previousItem != null && previousItem.TotalRevenue > 0)
                    {
                        item.PercentageChange = ((item.TotalRevenue - previousItem.TotalRevenue) / previousItem.TotalRevenue) * 100;
                    }
                }

                var report = new PopularityReportDTO
                {
                    Year = year,
                    Month = month,
                    ComponentPopularity = componentPopularity,
                    TotalRevenue = componentPopularity.Sum(cp => cp.TotalRevenue),
                    TotalOrdersCount = componentPopularity.Sum(cp => cp.TimesOrdered),
                    GeneratedAt = DateTime.UtcNow
                };

                return report;
            }
            catch (Exception ex) when (!(ex is ValidationException))
            {
                throw;
            }
        }

        public async Task<SalesReportDTO> GetSalesReportAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                // Validate date range
                if (startDate > endDate)
                    throw new ValidationException("Start date must be before or equal to end date");

                if (endDate > DateTime.UtcNow)
                    endDate = DateTime.UtcNow;

                var orders = await _context.Order
                    .Where(o => o.OrderDate >= startDate &&
                               o.OrderDate <= endDate &&
                               o.OrderStatus != OrderStatus.Cancelled)
                    .Include(o => o.Customer)
                    .Include(o => o.CustomFurnitureItems)
                        .ThenInclude(i => i.ItemComponents)
                            .ThenInclude(ic => ic.Component)
                    .ToListAsync();

                var totalOrders = orders.Count;
                var totalRevenue = orders.Sum(o => o.TotalAmount);
                var averageOrderValue = totalOrders > 0 ? totalRevenue / totalOrders : 0;

                // Get recent orders (last 10)
                var recentOrders = orders
                    .OrderByDescending(o => o.OrderDate)
                    .Take(10)
                    .Select(o => new OrderSummaryDTO
                    {
                        OrderId = o.OrderID,
                        OrderNumber = o.OrderNumber,
                        CustomerId = o.CustomerID,
                        CustomerName = $"{o.Customer.Name} {o.Customer.LastName}",
                        CustomerEmail = o.Customer.UserEmail,
                        Status = o.OrderStatus,
                        StatusDisplay = o.OrderStatus.ToString(),
                        TotalAmount = o.TotalAmount,
                        OrderDate = o.OrderDate,
                        ItemCount = o.CustomFurnitureItems.Count
                    })
                    .ToList();

                // Calculate top components
                var componentStats = orders
                    .SelectMany(o => o.CustomFurnitureItems)
                    .SelectMany(i => i.ItemComponents)
                    .GroupBy(ic => new { ic.ComponentID, ic.Component.Name, ic.Component.Type})
                    .Select(g => new ComponentPopularityDTO
                    {
                        ComponentId = g.Key.ComponentID,
                        ComponentName = g.Key.Name,
                        ComponentType = g.Key.Type,
                        TimesOrdered = g.Select(ic => ic.Item.OrderID).Distinct().Count(),
                        TotalQuantitySold = g.Sum(ic => ic.QuantityUsed),
                        TotalRevenue = g.Sum(ic => ic.LineTotal),
                        CurrentUnitPrice = g.First().UnitPriceAtOrder
                    })
                    .OrderByDescending(c => c.TotalRevenue)
                    .Take(10)
                    .ToList();

                // Assign ranks
                for (int i = 0; i < componentStats.Count; i++)
                {
                    componentStats[i].PopularityRank = i + 1;
                }

                var report = new SalesReportDTO
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    TotalOrders = totalOrders,
                    TotalRevenue = totalRevenue,
                    AverageOrderValue = averageOrderValue,
                    RecentOrders = recentOrders,
                    TopComponents = componentStats
                };

                return report;
            }
            catch (Exception ex) when (!(ex is ValidationException))
            {
               throw;
            }
        }

        public async Task<List<ComponentPopularityDTO>> GetComponentUsageHistoryAsync(int componentId, int months)
        {
            try
            {
                // Validate component exists
                var component = await _context.Component.FindAsync(componentId);
                if (component == null)
                    throw new NotFoundException("Component");

                // Validate months parameter
                if (months < 1 || months > 36)
                    throw new ValidationException("Months must be between 1 and 36");

                var endDate = DateTime.UtcNow;
                var startDate = endDate.AddMonths(-months);

                var usageHistory = await _context.ComponentPopularity
                    .Where(cp => cp.ComponentID == componentId &&
                                cp.PeriodYear >= startDate.Year)
                    .OrderByDescending(cp => cp.PeriodYear)
                    .ThenByDescending(cp => cp.PeriodMonth)
                    .Take(months)
                    .Select(cp => new ComponentPopularityDTO
                    {
                        ComponentId = cp.ComponentID,
                        ComponentName = component.Name,
                        ComponentType = component.Type,
                        TimesOrdered = (int)cp.TimesOrdered,
                        TotalQuantitySold = (int)cp.TotalQuantitySold,
                        TotalRevenue = (decimal)cp.TotalRevenue,
                        PeriodYear = cp.PeriodYear,
                        PeriodMonth = cp.PeriodMonth,
                        CurrentUnitPrice = component.UnitPrice
                    })
                    .ToListAsync();

                return usageHistory;
            }
            catch (Exception ex) when (!(ex is NotFoundException) && !(ex is ValidationException))
            {
               throw;
            }
        }

        public async Task GenerateMonthlyPopularityDataAsync()
        {
            try
            {
                var now = DateTime.UtcNow;
                var currentYear = now.Year;
                var currentMonth = now.Month;

                // Generate for current month
                await GeneratePopularityDataForPeriodAsync(currentYear, currentMonth);

                // Generate for previous month if it's the beginning of the month
                if (now.Day <= 5)
                {
                    var previousMonth = currentMonth - 1;
                    var previousYear = currentYear;
                    if (previousMonth < 1)
                    {
                        previousMonth = 12;
                        previousYear--;
                    }
                    await GeneratePopularityDataForPeriodAsync(previousYear, previousMonth);
                }
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        // Helper method to generate popularity data for a specific period
        private async Task GeneratePopularityDataForPeriodAsync(int year, int month)
        {
            try
            {
                var startDate = new DateTime(year, month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                // Delete existing data for this period
                var existingData = await _context.ComponentPopularity
                    .Where(cp => cp.PeriodYear == year && cp.PeriodMonth == month)
                    .ToListAsync();

                if (existingData.Any())
                {
                    _context.ComponentPopularity.RemoveRange(existingData);
                    await _context.SaveChangesAsync();
                }

                // Get all orders in this period
                var orders = await _context.Order
                    .Where(o => o.OrderDate >= startDate &&
                               o.OrderDate <= endDate &&
                               o.OrderStatus != OrderStatus.Cancelled)
                    .Include(o => o.CustomFurnitureItems)
                        .ThenInclude(i => i.ItemComponents)
                    .ToListAsync();

                // Calculate statistics per component
                var componentStats = orders
                    .SelectMany(o => o.CustomFurnitureItems)
                    .SelectMany(i => i.ItemComponents)
                    .GroupBy(ic => ic.ComponentID)
                    .Select(g => new ComponentPopularity
                    {
                        ComponentID = g.Key,
                        PeriodYear = year,
                        PeriodMonth = month,
                        TimesOrdered = g.Select(ic => ic.Item.OrderID).Distinct().Count(),
                        TotalQuantitySold = g.Sum(ic => ic.QuantityUsed),
                        TotalRevenue = g.Sum(ic => ic.LineTotal),
                        CalculatedAt = DateTime.UtcNow
                    })
                    .ToList();

                if (componentStats.Any())
                {
                    _context.ComponentPopularity.AddRange(componentStats);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
              throw;
            }
        }
    }
}
