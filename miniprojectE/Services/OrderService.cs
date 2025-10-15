using Microsoft.EntityFrameworkCore;
using miniprojectE.Data;
using miniprojectE.DTO.AddressDTOs;
using miniprojectE.DTO.ComponentDTOs;
using miniprojectE.DTO.OrderDTOs;
using miniprojectE.DTO.ResponseDTOs;
using miniprojectE.Models.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace miniprojectE.Services
{
    public class OrderService: IOrderService
    {
        private readonly AppDB _context;
        public OrderService(AppDB context, ILogger<OrderService> logger)
        {
            _context = context;
        }

        public async Task<OrderDTO> GetOrderAsync(int orderId)
        {
            try
            {
                var order = await _context.Order
                    .Include(o => o.Customer)
                    .Include(o => o.Clerk)
                    .Include(o => o.ShippingAddress)
                    .Include(o => o.CustomFurnitureItems)
                        .ThenInclude(item => item.Template)
                    .Include(o => o.CustomFurnitureItems)
                        .ThenInclude(item => item.ItemComponents)
                            .ThenInclude(ic => ic.Component)
                    .FirstOrDefaultAsync(o => o.OrderID == orderId);

                if (order == null)
                {
                    throw new NotFoundException("Order");                 }

                return new OrderDTO
                {
                    OrderId = order.OrderID,
                    OrderNumber = order.OrderNumber,
                    CustomerId = order.CustomerID,
                    CustomerName = $"{order.Customer.Name} {order.Customer.LastName}",
                    CustomerEmail = order.Customer.UserEmail,
                    AssignedClerkId = order.ClerkID,
                    AssignedClerkName = order.Clerk != null ? $"{order.Clerk.Name} {order.Clerk.LastName}" : null,
                    OrderStatus = order.OrderStatus,
                    Subtotal = order.Subtotal,
                    ShippingCost = order.ShippingCost,
                    TotalAmount = order.TotalAmount,
                    ShippingAddress = new AddressDTO
                    {
                        AddressID = order.ShippingAddress.AddressID,
                        Street = order.ShippingAddress.Street,
                        City = order.ShippingAddress.City,
                        Province = order.ShippingAddress.Province,
                        Code = order.ShippingAddress.Code,
                        Country = order.ShippingAddress.Country,
                    },
                    SpecialInstructions = order.SpecialInstructions,
                    OrderDate = order.OrderDate,
                    ExpectedCompletionDate = order.ExpectedCompletionDate,
                    CompletedDate = order.CompletedDate,
                    Items = order.CustomFurnitureItems.Select(item => new CustomFurnitureItemDTO
                    {
                        ItemId = item.OrderItemID,
                        TemplateId = item.TemplateID,
                        TemplateName = item.Template.Name,
                        FurnitureType = item.Template.FurnitureType,
                        ItemName = item.ItemName,
                        CustomDescription = item.ItemDescription,
                        ItemTotalPrice = item.ItemTotalPrice,
                        Quantity = (int)item.Quantity,
                        AssemblyStatus = item.AssemblyStatus,
                        AssemblyStartedAt = item.AssemblyStartedAt,
                        AssemblyCompletedAt = item.AssemblyCompletedAt,
                        Components = item.ItemComponents.Select(ic => new ItemComponentDTO
                        {
                            ComponentId = ic.ComponentID,
                            ComponentName = ic.Component.Name,
                            ComponentType = ic.Component.Type,
                            QuantityUsed = ic.QuantityUsed,
                            UnitPrice = ic.UnitPriceAtOrder,
                            LineTotal = ic.LineTotal,
                        }).ToList()
                    }).ToList()
                };
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                throw;
            }
        }
        public async Task<List<OrderDTO>> GetCustomerOrdersAsync(Guid customerId)
        {
            try
            {
                // Verify customer exists
                var customerExists = await _context.User.AnyAsync(u => u.UserID == customerId && u.Role == UserType.Customer);
                if (!customerExists)
                {
                    throw new NotFoundException("Customer");
                }

                var query = _context.Order
                    .Include(o => o.CustomerID == customerId)
                    .Include(o => o.Clerk)
                    .Include(o => o.ShippingAddress)
                    .Include(o => o.CustomFurnitureItems);
                    

                var orders = await query
                    .OrderByDescending(o => o.CreatedAt)
                    .Select(o => new OrderDTO
                    {
                        OrderId = o.OrderID,
                        OrderNumber = o.OrderNumber,
                        CustomerId = o.CustomerID,
                        CustomerName = $"{o.Customer.Name} {o.Customer.LastName}",
                        CustomerEmail = o.Customer.UserEmail,
                        AssignedClerkId = o.ClerkID,
                        AssignedClerkName = o.Clerk != null ? $"{o.Clerk.Name} {o.Clerk.LastName}" : null,
                        OrderStatus = o.OrderStatus,
                        Subtotal = o.Subtotal,
                        ShippingCost = o.ShippingCost,
                        TotalAmount = o.TotalAmount,
                        ShippingAddress = new AddressDTO
                        {
                            AddressID = o.ShippingAddress.AddressID,
                            Street = o.ShippingAddress.Street,
                            City = o.ShippingAddress.City,
                            Province = o.ShippingAddress.Province,
                            Code = o.ShippingAddress.Code,
                            Country = o.ShippingAddress.Country,
                        },

                        SpecialInstructions = o.SpecialInstructions,
                        OrderDate = o.OrderDate,
                        ExpectedCompletionDate = o.ExpectedCompletionDate,
                        CompletedDate = o.CompletedDate,
                        Items = o.CustomFurnitureItems.Select(item => new CustomFurnitureItemDTO
                        {
                            ItemId = item.OrderItemID,
                            TemplateId = item.TemplateID,
                            TemplateName = item.Template.Name,
                            FurnitureType = item.Template.FurnitureType,
                            ItemName = item.ItemName,
                            CustomDescription = item.ItemDescription,
                            ItemTotalPrice = item.ItemTotalPrice,
                            Quantity = (int)item.Quantity,
                            AssemblyStatus = item.AssemblyStatus,
                            AssemblyStartedAt = item.AssemblyStartedAt,
                            AssemblyCompletedAt = item.AssemblyCompletedAt
                        }).ToList()
                    })
                    .ToListAsync();

                return orders;
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                throw;
            }
        }
        public async Task<OrderDTO> CreateOrderAsync(CreateOrderDTO dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Validate addresses exist and belong to the user (you might want to add user validation)
                var shippingAddress = await _context.Address.FindAsync(dto.ShippingAddressId);

                if (shippingAddress == null)
                    throw new NotFoundException("Shipping Address");

                // Calculate pricing
                var pricing = await CalculateOrderPriceAsync(dto);

                // Generate order number
                var orderNumber = await GenerateOrderNumberAsync();

                // Create order
                var order = new Orders
                {
                    OrderNumber = orderNumber,
                    CustomerID = shippingAddress.UserID, // Get customer from address
                    OrderStatus = OrderStatus.Pending,
                    Subtotal = pricing.Subtotal,
                    ShippingCost = pricing.ShippingCost,
                    TotalAmount = pricing.TotalAmount,
                    AddressID = dto.ShippingAddressId,
                    SpecialInstructions = dto.SpecialInstructions,
                    OrderDate = DateTime.UtcNow,
                    ExpectedCompletionDate = CalculateExpectedCompletion(dto.Items),
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Order.Add(order);
                await _context.SaveChangesAsync();

                // Create furniture items and components
                foreach (var itemDto in dto.Items)
                {
                    var template = await _context.Furniture.FindAsync(itemDto.TemplateId);
                    if (template == null)
                        throw new NotFoundException("Furniture Template");

                    var itemCalculation = pricing.ItemCalculations.First(ic => ic.ItemName == itemDto.ItemName);

                    var furnitureItem = new OrderItem
                    {
                        OrderID = order.OrderID,
                        TemplateID = itemDto.TemplateId,
                        ItemName = itemDto.ItemName,
                        ItemDescription = itemDto.CustomDescription,
                        ItemTotalPrice = itemCalculation.ItemTotalPrice,
                        Quantity = itemDto.Quantity,
                        AssemblyStatus = AssemblyStatus.Pending
                    };

                    _context.OrderItem.Add(furnitureItem);
                    await _context.SaveChangesAsync();

                    // Create item components
                    foreach (var componentDto in itemDto.Components)
                    {
                        var component = await _context.Component.FindAsync(componentDto.ComponentId);
                        if (component == null)
                            throw new NotFoundException("Component");

                        // Check stock availability
                        var totalNeeded = componentDto.QuantityUsed * itemDto.Quantity;
                        if (component.Level < totalNeeded)
                        {
                            //throw new InsufficientStockException(componentDto.ComponentId, totalNeeded, component.StockQuantity);
                        }

                        var itemComponent = new ItemComponent
                        {
                            ItemID = furnitureItem.OrderItemID,
                            ComponentID = componentDto.ComponentId,
                            QuantityUsed = totalNeeded,
                            UnitPriceAtOrder = component.UnitPrice,
                            LineTotal = component.UnitPrice * totalNeeded
                        };

                        _context.ItemComponent.Add(itemComponent);

                        // Reserve stock
                        component.Level -= totalNeeded;

                        // Create stock movement
                        var stockMovement = new Stock
                        {
                            ComponentID = componentDto.ComponentId,
                            UserID = order.CustomerID,
                            MovementType = MovementType.Sale,
                            Type = component.Type.ToString(),
                            QuantityChange = -totalNeeded,
                            QuantityBefore = component.Level + totalNeeded,
                            QuantityAfter = component.Level,
                            ReferenceId = order.OrderID.ToString(),
                            Notes = $"Reserved for order {orderNumber}",
                            MovementDate = DateTime.UtcNow
                        };

                        _context.Stocks.Add(stockMovement);
                    }
                }

                await _context.SaveChangesAsync();

                // Log order creation
                var historyLog = new OrderHistoryLog
                {
                    OrderID = order.OrderID,
                    UserID = order.CustomerID,
                    StatusFrom = OrderStatus.Pending,
                    StatusTo = OrderStatus.Pending,
                    Notes = "Order created",
                    ChangedAt = DateTime.UtcNow
                };

                _context.OrderHistoryLog.Add(historyLog);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await GetOrderAsync(order.OrderID);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<OrderDTO> UpdateOrderStatusAsync(int orderId, UpdateOrderStatusDTO dto)
        {
            try
            {
                var order = await _context.Order.FindAsync(orderId);
                if (order == null)
                {
                    throw new NotFoundException("Order");
                }

                var oldStatus = order.OrderStatus;
                order.OrderStatus = dto.NewStatus;
                order.UpdatedAt = DateTime.UtcNow;

                // Set completion date if completed
                if (dto.NewStatus == OrderStatus.Completed && !order.CompletedDate.HasValue)
                {
                    order.CompletedDate = DateTime.UtcNow;
                }

                // Log status change
                var historyLog = new OrderHistoryLog
                {
                    OrderID = orderId,
                    UserID = (Guid)order.ClerkID, // You'll need to get current user ID from claims
                    StatusFrom = oldStatus,
                    StatusTo = dto.NewStatus,
                    Notes = dto.Notes,
                    ChangedAt = DateTime.UtcNow
                };

                _context.OrderHistoryLog.Add(historyLog);
                await _context.SaveChangesAsync();

                return await GetOrderAsync(orderId);
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                throw;
            }
        }
        public async Task<OrderDTO> AssignClerkAsync(int orderId, Guid clerkId)
        {
            try
            {
                var order = await _context.Order.FindAsync(orderId);
                if (order == null)
                {
                    throw new NotFoundException("Order");
                }

                // Verify clerk exists and has correct role
                var clerk = await _context.User.FindAsync(clerkId);
                if (clerk == null || (clerk.Role != UserType.Clerk && clerk.Role != UserType.Manager && clerk.Role != UserType.Admin))
                {
                    throw new NotFoundException("Clerk");
                }

                order.ClerkID = clerkId;
                order.UpdatedAt = DateTime.UtcNow;

                // Log assignment
                var historyLog = new OrderHistoryLog
                {
                    OrderID = orderId,
                    UserID = clerkId,
                    StatusFrom = order.OrderStatus,
                    StatusTo = order.OrderStatus,
                    Notes = $"Order assigned to {clerk.Name} {clerk.LastName}",
                    ChangedAt = DateTime.UtcNow
                };

                _context.OrderHistoryLog.Add(historyLog);
                await _context.SaveChangesAsync();

                return await GetOrderAsync(orderId);
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                throw;
            }
        }
        public async Task<OrderDTO> StartAssemblyAsync(int orderId)
        {
            try
            {
                var order = await _context.Order
                    .Include(o => o.CustomFurnitureItems)
                    .FirstOrDefaultAsync(o => o.OrderID == orderId);

                if (order == null)
                {
                    throw new NotFoundException("Order");
                }

                order.OrderStatus = OrderStatus.Assembling;
                order.UpdatedAt = DateTime.UtcNow;

                // Start assembly for all items
                foreach (var item in order.CustomFurnitureItems)
                {
                    if (item.AssemblyStatus == AssemblyStatus.Pending)
                    {
                        item.AssemblyStatus = AssemblyStatus.InProgress;
                        item.AssemblyStartedAt = DateTime.UtcNow;
                    }
                }

                await _context.SaveChangesAsync();

                return await GetOrderAsync(orderId);
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                throw;
            }
        }
        public async Task<OrderDTO> CompleteAssemblyAsync(int orderId)
        {
            try
            {
                var order = await _context.Order
                    .Include(o => o.CustomFurnitureItems)
                    .FirstOrDefaultAsync(o => o.OrderID == orderId);

                if (order == null)
                {
                    throw new NotFoundException("Order");
                }

                // Complete assembly for all items
                foreach (var item in order.CustomFurnitureItems)
                {
                    if (item.AssemblyStatus == AssemblyStatus.InProgress)
                    {
                        item.AssemblyStatus = AssemblyStatus.Completed;
                        item.AssemblyCompletedAt = DateTime.UtcNow;
                    }
                }

                order.OrderStatus = OrderStatus.Assembled;
                order.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return await GetOrderAsync(orderId);
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                throw;
            }
        }
        public async Task<OrderDTO> ShipOrderAsync(int orderId)
        {
            try
            {
                var order = await _context.Order.FindAsync(orderId);
                if (order == null)
                {
                    throw new NotFoundException("Order");
                }

                order.OrderStatus = OrderStatus.Shipped;
                order.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return await GetOrderAsync(orderId);
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                throw;
            }
        }
        public async Task<OrderDTO> CompleteOrderAsync(int orderId)
        {
            try
            {
                var order = await _context.Order.FindAsync(orderId);
                if (order == null)
                {
                    throw new NotFoundException("Order");
                }

                order.OrderStatus = OrderStatus.Completed;
                order.CompletedDate = DateTime.UtcNow;
                order.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return await GetOrderAsync(orderId);
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                throw;
            }
        }
        public async Task CancelOrderAsync(int orderId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var order = await _context.Order
                    .Include(o => o.CustomFurnitureItems)
                        .ThenInclude(item => item.ItemComponents)
                    .FirstOrDefaultAsync(o => o.OrderID == orderId);

                if (order == null)
                {
                    throw new NotFoundException("Order");
                }

                // Return stock for all components
                foreach (var item in order.CustomFurnitureItems)
                {
                    foreach (var itemComponent in item.ItemComponents)
                    {
                        var component = await _context.Component.FindAsync(itemComponent.ComponentID);
                        if (component != null)
                        {
                            component.Level += itemComponent.QuantityUsed;

                            // Create stock movement for return
                            var stockMovement = new Stock
                            {
                                ComponentID = itemComponent.ComponentID,
                                UserID = (Guid)order.ClerkID, // Current user ID
                                MovementType = MovementType.Adjustment,
                                Type = component.Type.ToString(),
                                QuantityChange = itemComponent.QuantityUsed,
                                QuantityBefore = component.Level - itemComponent.QuantityUsed,
                                QuantityAfter = component.Level,
                                ReferenceId = orderId.ToString(),
                                Notes = $"Stock returned from cancelled order {order.OrderNumber}",
                                MovementDate = DateTime.UtcNow
                            };

                            _context.Stocks.Add(stockMovement);
                        }
                    }
                }

                order.OrderStatus = OrderStatus.Cancelled;
                order.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
}
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<OrderCalculationDTO> CalculateOrderPriceAsync(CreateOrderDTO dto)
        {
            try
            {
                var calculation = new OrderCalculationDTO
                {
                    ItemCalculations = new List<ItemCalculationDTO>()
                };

                decimal subtotal = 0;

                foreach (var itemDto in dto.Items)
                {
                    var template = await _context.Furniture.FindAsync(itemDto.TemplateId);
                    if (template == null)
                        throw new NotFoundException("Furniture Template");

                    var itemCalculation = new ItemCalculationDTO
                    {
                        ItemName = itemDto.ItemName,
                        Quantity = itemDto.Quantity,
                        ComponentCalculations = new List<ComponentCalculationDTO>()
                    };

                    decimal itemUnitPrice = template.BasePrice;

                    foreach (var componentDto in itemDto.Components)
                    {
                        var component = await _context.Component.FindAsync(componentDto.ComponentId);
                        if (component == null)
                            throw new NotFoundException("Component");

                        var componentCalc = new ComponentCalculationDTO
                        {
                            ComponentId = componentDto.ComponentId,
                            ComponentName = component.Name,
                            QuantityUsed = componentDto.QuantityUsed,
                            UnitPrice = component.UnitPrice,
                            LineTotal = component.UnitPrice * componentDto.QuantityUsed
                        };

                        itemCalculation.ComponentCalculations.Add(componentCalc);
                        itemUnitPrice += componentCalc.LineTotal;
                    }

                    itemCalculation.ItemUnitPrice = itemUnitPrice;
                    itemCalculation.ItemTotalPrice = itemUnitPrice * itemDto.Quantity;
                    calculation.ItemCalculations.Add(itemCalculation);

                    subtotal += itemCalculation.ItemTotalPrice;
                }

                calculation.Subtotal = subtotal;
                calculation.ShippingCost = subtotal > 500 ? 0 : 50; // Free shipping over R500
                calculation.TotalAmount = calculation.Subtotal + calculation.ShippingCost;

                return calculation;
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
               throw;
            }
        }

        //helper method to generate order number
        private async Task<string> GenerateOrderNumberAsync()
        {
            var today = DateTime.UtcNow;
            var prefix = $"ORD-{today:yyyy-MM}-";

            var lastOrder = await _context.Order
                .Where(o => o.OrderNumber.StartsWith(prefix))
                .OrderByDescending(o => o.OrderNumber)
                .FirstOrDefaultAsync();

            int nextNumber = 1;
            if (lastOrder != null)
            {
                var numberPart = lastOrder.OrderNumber.Substring(prefix.Length);
                if (int.TryParse(numberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"{prefix}{nextNumber:D4}";
        }

        //helper method to calculate expected completion date
        private DateTime? CalculateExpectedCompletion(List<CreateCustomFurnitureItemDTO> items)
        {
            // 2 days per item + 3 days base processing
            var estimatedDays = 3 + (items.Count * 2);
            return DateTime.UtcNow.AddDays(estimatedDays);
        }

        public async Task<List<OrderHistoryLog>> GetOrderHistoryAsync(int orderId)
        {
            try
            {
                var history = await _context.OrderHistoryLog
                    .Include(h => h.User)
                    .Where(h => h.OrderID == orderId)
                    .OrderByDescending(h => h.ChangedAt)
                    .ToListAsync();

                return history;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<OrderDTO>> GetAllOrdersAsync()
        {
            try
            {
                var query = _context.Order
                    .Include(o => o.Customer)
                    .Include(o => o.Clerk)
                    .Include(o => o.ShippingAddress)
                    .Include(o => o.CustomFurnitureItems)
                    .AsQueryable();
                
                // 
                var orders = await query
                    .OrderByDescending(o => o.CreatedAt)
                    .Select(o => new OrderDTO
                    {
                        OrderId = o.OrderID,
                        OrderNumber = o.OrderNumber,
                        CustomerId = o.CustomerID,
                        CustomerName = $"{o.Customer.Name} {o.Customer.LastName}",
                        CustomerEmail = o.Customer.UserEmail,
                        AssignedClerkId = o.ClerkID,
                        AssignedClerkName = o.Clerk != null ? $"{o.Clerk.Name} {o.Clerk.LastName}" : null,
                        OrderStatus = o.OrderStatus,
                        Subtotal = o.Subtotal,
                        ShippingCost = o.ShippingCost,
                        TotalAmount = o.TotalAmount,
                        ShippingAddress = new AddressDTO
                        {
                            AddressID = o.ShippingAddress.AddressID,
                            Street = o.ShippingAddress.Street,
                            City = o.ShippingAddress.City,
                            Province = o.ShippingAddress.Province,
                            Code = o.ShippingAddress.Code,
                            Country = o.ShippingAddress.Country,
                        },

                        SpecialInstructions = o.SpecialInstructions,
                        OrderDate = o.OrderDate,
                        ExpectedCompletionDate = o.ExpectedCompletionDate,
                        CompletedDate = o.CompletedDate,
                        Items = o.CustomFurnitureItems.Select(item => new CustomFurnitureItemDTO
                        {
                            ItemId = item.OrderItemID,
                            TemplateId = item.TemplateID,
                            TemplateName = item.Template.Name,
                            FurnitureType = item.Template.FurnitureType,
                            ItemName = item.ItemName,
                            CustomDescription = item.ItemDescription,
                            ItemTotalPrice = item.ItemTotalPrice,
                            Quantity = (int)item.Quantity,
                            AssemblyStatus = item.AssemblyStatus,
                            AssemblyStartedAt = item.AssemblyStartedAt,
                            AssemblyCompletedAt = item.AssemblyCompletedAt
                        }).ToList()
                    })
                    .ToListAsync();

                return orders;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}