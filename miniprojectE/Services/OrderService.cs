using Microsoft.EntityFrameworkCore;
using miniprojectE.Data;
using miniprojectE.DTO.AddressDTOs;
using miniprojectE.DTO.ComponentDTOs;
using miniprojectE.DTO.OrderDTOs;
using miniprojectE.DTO.ResponseDTOs;
using miniprojectE.Models.Entities;

namespace miniprojectE.Services
{
    public class OrderService: IOrderService
    {
        private readonly AppDB _context;
        private readonly ILogger<OrderService> _logger;
        private readonly IInventoryService _inventoryService;
        public OrderService(AppDB context, ILogger<OrderService> logger, IInventoryService inventoryService)
        {
            _context = context;
            _logger = logger;
            _inventoryService = inventoryService;
        }

        public async Task<CalculationResponseDTO<OrderDTO>> GetOrdersAsync(int page, int pageSize, OrderStatus? status, Guid? customerId, Guid? clerkId)
        {
            try
            {
                var query = _context.Order
                    .Include(o => o.Customer)
                    .Include(o => o.Clerk)
                    .Include(o => o.ShippingAddress)
                    .Include(o => o.CustomFurnitureItems)
                    .AsQueryable();

                // Apply filters
                if (status.HasValue)
                    query = query.Where(o => o.OrderStatus == status.Value);

                if (customerId.HasValue)
                    query = query.Where(o => o.CustomerID == customerId.Value);

                if (clerkId.HasValue)
                    query = query.Where(o => o.ClerkID == clerkId.Value);

                // Get total count
                var totalCount = await query.CountAsync();
                var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

                // Get paginated orders
                var orders = await query
                    .OrderByDescending(o => o.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
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

                return new CalculationResponseDTO<OrderDTO>
                {
                    Data = orders,
                    TotalCount = totalCount,
                    PageNumber = page,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    HasNextPage = page < totalPages,
                    HasPreviousPage = page > 1
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting orders with filters - Status: {Status}, CustomerId: {CustomerId}, ClerkId: {ClerkId}", status, customerId, clerkId);
                throw;
            }
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
                _logger.LogError(ex, "Error getting order {OrderId}", orderId);
                throw;
            }
        }
        public async Task<CalculationResponseDTO<OrderDTO>> GetCustomerOrdersAsync(Guid customerId)
        {
            try
            {
                // Verify customer exists
                var customerExists = await _context.User.AnyAsync(u => u.UserID == customerId && u.Role == UserType.Customer);
                if (!customerExists)
                {
                    throw new NotFoundException("Customer");
                }

                return await GetOrdersAsync(1, 10, null, customerId, null);
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                _logger.LogError(ex, "Error getting orders for customer {CustomerId}", customerId);
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

                _logger.LogInformation("Order {OrderNumber} created successfully for customer {CustomerId}", orderNumber, order.CustomerID);

                return await GetOrderAsync(order.OrderID);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error creating order");
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

                // Validate status transition
                if (!IsValidStatusTransition(order.OrderStatus, dto.NewStatus))
                {
                    //throw new InvalidOrderStatusTransitionException(order.OrderStatus, dto.NewStatus);
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

                _logger.LogInformation("Order {OrderId} status updated from {OldStatus} to {NewStatus}", orderId, oldStatus, dto.NewStatus);

                return await GetOrderAsync(orderId);
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                _logger.LogError(ex, "Error updating order status for order {OrderId}", orderId);
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

                _logger.LogInformation("Order {OrderId} assigned to clerk {ClerkId}", orderId, clerkId);

                return await GetOrderAsync(orderId);
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                _logger.LogError(ex, "Error assigning clerk to order {OrderId}", orderId);
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

                if (order.OrderStatus != OrderStatus.Confirmed)
                {
                    //throw new InvalidOrderStatusTransitionException(order.OrderStatus, OrderStatus.Assembling);
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

                _logger.LogInformation("Assembly started for order {OrderId}", orderId);

                return await GetOrderAsync(orderId);
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                _logger.LogError(ex, "Error starting assembly for order {OrderId}", orderId);
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

                if (order.OrderStatus != OrderStatus.Assembling)
                {
                    //throw new InvalidOrderStatusTransitionException(order.OrderStatus, OrderStatus.Assembled);
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

                _logger.LogInformation("Assembly completed for order {OrderId}", orderId);

                return await GetOrderAsync(orderId);
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                _logger.LogError(ex, "Error completing assembly for order {OrderId}", orderId);
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

                if (order.OrderStatus != OrderStatus.Assembled)
                {
                    //throw new InvalidOrderStatusTransitionException(order.OrderStatus, OrderStatus.Shipped);
                }

                order.OrderStatus = OrderStatus.Shipped;
                order.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Order {OrderId} shipped", orderId);

                return await GetOrderAsync(orderId);
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                _logger.LogError(ex, "Error shipping order {OrderId}", orderId);
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

                if (order.OrderStatus != OrderStatus.Shipped && order.OrderStatus != OrderStatus.Delivered)
                {
                    //throw new InvalidOrderStatusTransitionException(order.OrderStatus, OrderStatus.Completed);
                }

                order.OrderStatus = OrderStatus.Completed;
                order.CompletedDate = DateTime.UtcNow;
                order.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Order {OrderId} completed", orderId);

                return await GetOrderAsync(orderId);
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                _logger.LogError(ex, "Error completing order {OrderId}", orderId);
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

                if (order.OrderStatus == OrderStatus.Completed || order.OrderStatus == OrderStatus.Shipped)
                {
                   // throw new InvalidOrderStatusTransitionException(order.OrderStatus, OrderStatus.Cancelled);
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

                _logger.LogInformation("Order {OrderId} cancelled and stock returned", orderId);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error cancelling order {OrderId}", orderId);
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
                calculation.ShippingCost = subtotal > 500 ? 0 : 50; // Free shipping over $500
                calculation.TotalAmount = calculation.Subtotal + calculation.ShippingCost;

                return calculation;
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                _logger.LogError(ex, "Error calculating order price");
                throw;
            }
        }

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

        private DateTime? CalculateExpectedCompletion(List<CreateCustomFurnitureItemDTO> items)
        {
            // Simple calculation: 2 days per item + 3 days base processing
            var estimatedDays = 3 + (items.Count * 2);
            return DateTime.UtcNow.AddDays(estimatedDays);
        }

        private bool IsValidStatusTransition(OrderStatus from, OrderStatus to)
        {
            return from switch
            {
                OrderStatus.Pending => to == OrderStatus.Confirmed || to == OrderStatus.Cancelled,
                OrderStatus.Confirmed => to == OrderStatus.Assembling || to == OrderStatus.Cancelled,
                OrderStatus.Assembling => to == OrderStatus.Assembled || to == OrderStatus.Cancelled,
                OrderStatus.Assembled => to == OrderStatus.Shipped || to == OrderStatus.Cancelled,
                OrderStatus.Shipped => to == OrderStatus.Delivered || to == OrderStatus.Completed,
                OrderStatus.Delivered => to == OrderStatus.Completed,
                OrderStatus.Completed => false, // Cannot transition from completed
                OrderStatus.Cancelled => false, // Cannot transition from cancelled
                _ => false
            };
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
                _logger.LogError(ex, "Error getting order history for order {OrderId}", orderId);
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
                
                // Get paginated orders
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