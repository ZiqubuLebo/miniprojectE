/** 
    public class SalesMetricsDto
    {
        public decimal TodayRevenue { get; set; }
        public decimal WeekRevenue { get; set; }
        public decimal MonthRevenue { get; set; }
        public decimal YearRevenue { get; set; }
        public int TodayOrders { get; set; }
        public int WeekOrders { get; set; }
        public int MonthOrders { get; set; }
        public decimal AverageOrderValue { get; set; }
    }

    public class InventoryMetricsDto
    {
        public int TotalComponents { get; set; }
        public int LowStockCount { get; set; }
        public int OutOfStockCount { get; set; }
        public decimal TotalInventoryValue { get; set; }
        public List<ComponentType> ComponentTypeCounts { get; set; } = new List<ComponentType>();
    }

    public class OrderMetricsDto
    {
        public int PendingOrders { get; set; }
        public int ConfirmedOrders { get; set; }
        public int AssemblingOrders { get; set; }
        public int ShippedOrders { get; set; }
        public int CompletedTodayOrders { get; set; }
        public double AverageProcessingTime { get; set; }
    }

// Additional DTOs for advanced features
namespace FurnitureStore.DTOs
{
    public class BulkStockUpdateDto
    {
        [Required]
        public List<ComponentStockUpdateDto> Updates { get; set; } = new List<ComponentStockUpdateDto>();

        public string Notes { get; set; }
    }

    public class ComponentStockUpdateDto
    {
        [Required]
        public int ComponentId { get; set; }

        [Required]
        public int NewStockLevel { get; set; }
    }

    public class OrderStatusHistoryDto
    {
        public int LogId { get; set; }
        public OrderStatus StatusFrom { get; set; }
        public OrderStatus StatusTo { get; set; }
        public string UserName { get; set; }
        public string Notes { get; set; }
        public DateTime ChangedAt { get; set; }
    }

    public class ComponentUsageAnalyticsDto
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public ComponentType ComponentType { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalQuantitySold { get; set; }
        public int TimesOrdered { get; set; }
        public decimal AverageQuantityPerOrder { get; set; }
        public List<MonthlyUsageDto> MonthlyUsage { get; set; } = new List<MonthlyUsageDto>();
    }

    public class MonthlyUsageDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int TimesOrdered { get; set; }
        public int TotalQuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class FurnitureCustomizationDto
    {
        public int TemplateId { get; set; }
        public FurnitureTemplateDto Template { get; set; }
        public List<ComponentGroupDto> ComponentGroups { get; set; } = new List<ComponentGroupDto>();
        public decimal BasePrice { get; set; }
        public decimal EstimatedTotalPrice { get; set; }
        public bool CanCustomize { get; set; }
        public List<string> CustomizationRestrictions { get; set; } = new List<string>();
    }

    public class ComponentGroupDto
    {
        public ComponentType ComponentType { get; set; }
        public ComponentRole Role { get; set; }
        public bool IsRequired { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public List<ComponentDto> AvailableComponents { get; set; } = new List<ComponentDto>();
    }

    public class OrderTrackingDto
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public OrderStatus CurrentStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedCompletionDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public List<OrderStatusHistoryDto> StatusHistory { get; set; } = new List<OrderStatusHistoryDto>();
        public List<CustomFurnitureItemDto> Items { get; set; } = new List<CustomFurnitureItemDto>();
        public AddressDto ShippingAddress { get; set; }
        public string TrackingInformation { get; set; }
    }
}

namespace FurnitureStore.DTOs
{
    // Validation and Pricing DTOs
    public class ComponentValidationRequest
    {
        [Required]
        public List<CreateItemComponentDto> Components { get; set; } = new List<CreateItemComponentDto>();
    }

    public class ComponentValidationResponse
    {
        public bool IsValid { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
        public List<string> CompatibilityIssues { get; set; } = new List<string>();
        public List<string> StockIssues { get; set; } = new List<string>();
        public decimal EstimatedPrice { get; set; }
    }

    // Reporting DTOs
    public class StockReportDto
    {
        public List<StockLevelDto> LowStockItems { get; set; } = new List<StockLevelDto>();
        public List<StockLevelDto> AllComponents { get; set; } = new List<StockLevelDto>();
        public int TotalComponents { get; set; }
        public int LowStockCount { get; set; }
        public DateTime GeneratedAt { get; set; }
    }

    public class PopularityReportDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public List<ComponentPopularityDto> ComponentPopularity { get; set; } = new List<ComponentPopularityDto>();
        public decimal TotalRevenue { get; set; }
        public int TotalOrdersCount { get; set; }
        public DateTime GeneratedAt { get; set; }
    }

    public class SalesReportDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageOrderValue { get; set; }
        public List<OrderSummaryDto> RecentOrders { get; set; } = new List<OrderSummaryDto>();
        public List<ComponentPopularityDto> TopComponents { get; set; } = new List<ComponentPopularityDto>();
    }

    // Purchase Order DTOs
    public class PurchaseOrderDto
    {
        public int PurchaseOrderId { get; set; }
        public string SupplierName { get; set; }
        public PurchaseOrderStatus Status { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string ManagerName { get; set; }
        public List<PurchaseOrderItemDto> Items { get; set; } = new List<PurchaseOrderItemDto>();
    }

    public class CreatePurchaseOrderDto
    {
        [Required]
        public string SupplierName { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        [Required]
        public List<CreatePurchaseOrderItemDto> Items { get; set; } = new List<CreatePurchaseOrderItemDto>();
    }

    public class PurchaseOrderItemDto
    {
        public int PurchaseItemId { get; set; }
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string ComponentSku { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantityReceived { get; set; }
        public decimal UnitCost { get; set; }
        public decimal LineTotal { get; set; }
    }

    public class CreatePurchaseOrderItemDto
    {
        [Required]
        public int ComponentId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int QuantityOrdered { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal UnitCost { get; set; }
    }

    public class ReceivePurchaseOrderDto
    {
        [Required]
        public List<ReceivePurchaseOrderItemDto> Items { get; set; } = new List<ReceivePurchaseOrderItemDto>();
    }

    public class ReceivePurchaseOrderItemDto
    {
        [Required]
        public int PurchaseItemId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int QuantityReceived { get; set; }
    }

    // Common Response DTOs
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class PaginatedResponse<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }
}

namespace FurnitureStore.Controllers
{
    // User Management Controller
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<UserProfileDto>>> Register([FromBody] UserRegistrationDto dto)
        {
            try
            {
                var result = await _userService.RegisterUserAsync(dto);
                return Ok(new ApiResponse<UserProfileDto> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<UserProfileDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<string>>> Login([FromBody] UserLoginDto dto)
        {
            try
            {
                var token = await _userService.LoginAsync(dto);
                return Ok(new ApiResponse<string> { Success = true, Data = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new ApiResponse<string> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<UserProfileDto>>> GetProfile(int id)
        {
            try
            {
                var profile = await _userService.GetUserProfileAsync(id);
                return Ok(new ApiResponse<UserProfileDto> { Success = true, Data = profile });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<UserProfileDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<UserProfileDto>>> UpdateProfile(int id, [FromBody] UserUpdateDto dto)
        {
            try
            {
                var result = await _userService.UpdateUserAsync(id, dto);
                return Ok(new ApiResponse<UserProfileDto> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<UserProfileDto> { Success = false, Message = ex.Message });
            }
        }
    }

    // Address Management Controller
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ApiResponse<List<AddressDto>>>> GetUserAddresses(int userId)
        {
            try
            {
                var addresses = await _addressService.GetUserAddressesAsync(userId);
                return Ok(new ApiResponse<List<AddressDto>> { Success = true, Data = addresses });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<List<AddressDto>> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("user/{userId}")]
        public async Task<ActionResult<ApiResponse<AddressDto>>> CreateAddress(int userId, [FromBody] CreateAddressDto dto)
        {
            try
            {
                var result = await _addressService.CreateAddressAsync(userId, dto);
                return Ok(new ApiResponse<AddressDto> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AddressDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<AddressDto>>> UpdateAddress(int id, [FromBody] CreateAddressDto dto)
        {
            try
            {
                var result = await _addressService.UpdateAddressAsync(id, dto);
                return Ok(new ApiResponse<AddressDto> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<AddressDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAddress(int id)
        {
            try
            {
                await _addressService.DeleteAddressAsync(id);
                return Ok(new ApiResponse<bool> { Success = true, Data = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }
    }

    // Component Management Controller
    [ApiController]
    [Route("api/[controller]")]
    public class ComponentsController : ControllerBase
    {
        private readonly IComponentService _componentService;

        public ComponentsController(IComponentService componentService)
        {
            _componentService = componentService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<ComponentDto>>> GetComponents(
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 50,
            [FromQuery] ComponentType? componentType = null,
            [FromQuery] bool? lowStockOnly = null)
        {
            try
            {
                var result = await _componentService.GetComponentsAsync(page, pageSize, componentType, lowStockOnly);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new PaginatedResponse<ComponentDto> { Data = new List<ComponentDto>() });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ComponentDto>>> GetComponent(int id)
        {
            try
            {
                var component = await _componentService.GetComponentAsync(id);
                return Ok(new ApiResponse<ComponentDto> { Success = true, Data = component });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<ComponentDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<ApiResponse<ComponentDto>>> CreateComponent([FromBody] CreateComponentDto dto)
        {
            try
            {
                var result = await _componentService.CreateComponentAsync(dto);
                return CreatedAtAction(nameof(GetComponent), new { id = result.ComponentId }, 
                    new ApiResponse<ComponentDto> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<ComponentDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<ApiResponse<ComponentDto>>> UpdateComponent(int id, [FromBody] UpdateComponentDto dto)
        {
            try
            {
                var result = await _componentService.UpdateComponentAsync(id, dto);
                return Ok(new ApiResponse<ComponentDto> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<ComponentDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("{id}/stock")]
        [Authorize(Roles = "Admin,Manager,Clerk")]
        public async Task<ActionResult<ApiResponse<ComponentDto>>> AdjustStock(int id, [FromBody] StockAdjustmentDto dto)
        {
            try
            {
                var result = await _componentService.AdjustStockAsync(id, dto);
                return Ok(new ApiResponse<ComponentDto> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<ComponentDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("{id}/compatibility")]
        public async Task<ActionResult<ApiResponse<List<ComponentCompatibilityDto>>>> GetCompatibility(int id)
        {
            try
            {
                var compatibility = await _componentService.GetComponentCompatibilityAsync(id);
                return Ok(new ApiResponse<List<ComponentCompatibilityDto>> { Success = true, Data = compatibility });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<List<ComponentCompatibilityDto>> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("compatibility")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<ApiResponse<ComponentCompatibilityDto>>> CreateCompatibility([FromBody] CreateCompatibilityDto dto)
        {
            try
            {
                var result = await _componentService.CreateCompatibilityAsync(dto);
                return Ok(new ApiResponse<ComponentCompatibilityDto> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<ComponentCompatibilityDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("validate")]
        public async Task<ActionResult<ComponentValidationResponse>> ValidateComponents([FromBody] ComponentValidationRequest request)
        {
            try
            {
                var result = await _componentService.ValidateComponentsAsync(request.Components);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ComponentValidationResponse { IsValid = false, ValidationErrors = { ex.Message } });
            }
        }
    }

    // Furniture Template Controller
    [ApiController]
    [Route("api/[controller]")]
    public class TemplatesController : ControllerBase
    {
        private readonly ITemplateService _templateService;

        public TemplatesController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<FurnitureTemplateDto>>>> GetTemplates(
            [FromQuery] FurnitureType? furnitureType = null)
        {
            try
            {
                var templates = await _templateService.GetTemplatesAsync(furnitureType);
                return Ok(new ApiResponse<List<FurnitureTemplateDto>> { Success = true, Data = templates });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<List<FurnitureTemplateDto>> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<FurnitureTemplateDto>>> GetTemplate(int id)
        {
            try
            {
                var template = await _templateService.GetTemplateAsync(id);
                return Ok(new ApiResponse<FurnitureTemplateDto> { Success = true, Data = template });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<FurnitureTemplateDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<ApiResponse<FurnitureTemplateDto>>> CreateTemplate([FromBody] CreateTemplateDto dto)
        {
            try
            {
                var result = await _templateService.CreateTemplateAsync(dto);
                return CreatedAtAction(nameof(GetTemplate), new { id = result.TemplateId },
                    new ApiResponse<FurnitureTemplateDto> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<FurnitureTemplateDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("{id}/components")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<ApiResponse<TemplateComponentDto>>> AddTemplateComponent(int id, [FromBody] CreateTemplateComponentDto dto)
        {
            try
            {
                var result = await _templateService.AddTemplateComponentAsync(id, dto);
                return Ok(new ApiResponse<TemplateComponentDto> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<TemplateComponentDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete("{templateId}/components/{componentId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<ApiResponse<bool>>> RemoveTemplateComponent(int templateId, int componentId)
        {
            try
            {
                await _templateService.RemoveTemplateComponentAsync(templateId, componentId);
                return Ok(new ApiResponse<bool> { Success = true, Data = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }
    }

    // Dashboard Controller
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("customer/{customerId}")]
        [Authorize(Roles = "Customer,Manager,Admin")]
        public async Task<ActionResult<ApiResponse<CustomerDashboardDto>>> GetCustomerDashboard(int customerId)
        {
            try
            {
                var dashboard = await _dashboardService.GetCustomerDashboardAsync(customerId);
                return Ok(new ApiResponse<CustomerDashboardDto> { Success = true, Data = dashboard });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<CustomerDashboardDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("clerk/{clerkId}")]
        [Authorize(Roles = "Clerk,Manager,Admin")]
        public async Task<ActionResult<ApiResponse<ClerkDashboardDto>>> GetClerkDashboard(int clerkId)
        {
            try
            {
                var dashboard = await _dashboardService.GetClerkDashboardAsync(clerkId);
                return Ok(new ApiResponse<ClerkDashboardDto> { Success = true, Data = dashboard });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<ClerkDashboardDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("manager")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<ActionResult<ApiResponse<ManagerDashboardDto>>> GetManagerDashboard()
        {
            try
            {
                var dashboard = await _dashboardService.GetManagerDashboardAsync();
                return Ok(new ApiResponse<ManagerDashboardDto> { Success = true, Data = dashboard });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<ManagerDashboardDto> { Success = false, Message = ex.Message });
            }
        }
    }


// Additional DTOs for Dashboard and Complex Operations
namespace FurnitureStore.DTOs
{
    // Dashboard DTOs
    public class CustomerDashboardDto
    {
        public UserProfileDto Profile { get; set; }
        public List<OrderSummaryDto> RecentOrders { get; set; } = new List<OrderSummaryDto>();
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public OrderSummaryDto CurrentOrder { get; set; }
    }

    public class ClerkDashboardDto
    {
        public UserProfileDto Profile { get; set; }
        public List<OrderSummaryDto> AssignedOrders { get; set; } = new List<OrderSummaryDto>();
        public int PendingAssemblyCount { get; set; }
        public int InProgressAssemblyCount { get; set; }
        public int CompletedTodayCount { get; set; }
        public List<OrderSummaryDto> UrgentOrders { get; set; } = new List<OrderSummaryDto>();
    }

    public class ManagerDashboardDto
    {
        public SalesMetricsDto SalesMetrics { get; set; }
        public InventoryMetricsDto InventoryMetrics { get; set; }
        public OrderMetricsDto OrderMetrics { get; set; }
        public List<StockLevelDto> LowStockAlerts { get; set; } = new List<StockLevelDto>();
        public List<ComponentPopularityDto> TopComponents { get; set; } = new List<ComponentPopularityDto>();
    }

    public class SalesMetricsDto
    {
        public decimal TodayRevenue { get; set; }
        public decimal WeekRevenue { get; set; }
        public decimal MonthRevenue { get; set; }
        public decimal YearRevenue { get; set; }
        public int TodayOrders { get; set; }
        public int WeekOrders { get; set; }
        public int MonthOrders { get; set; }
        public decimal AverageOrderValue { get; set; }
    }

    public class InventoryMetricsDto
    {
        public int TotalComponents { get; set; }
        public int LowStockCount { get; set; }
        public int OutOfStockCount { get; set; }
        public decimal TotalInventoryValue { get; set; }
        public List<ComponentType> ComponentTypeCounts { get; set; } = new List<ComponentType>();
    }

    public class OrderMetricsDto
    {
        public int PendingOrders { get; set; }
        public int ConfirmedOrders { get; set; }
        public int AssemblingOrders { get; set; }
        public int ShippedOrders { get; set; }
        public int CompletedTodayOrders { get; set; }
        public double AverageProcessingTime { get; set; }
    }

    // Stock Movement DTOs
    public class StockMovementDto
    {
        public int MovementId { get; set; }
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string ComponentSku { get; set; }
        public string UserName { get; set; }
        public MovementType MovementType { get; set; }
        public int QuantityChange { get; set; }
        public int QuantityBefore { get; set; }
        public int QuantityAfter { get; set; }
        public string ReferenceId { get; set; }
        public string Notes { get; set; }
        public DateTime MovementDate { get; set; }
    }

    // Validation DTOs
    public class ComponentAvailabilityDto
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public int RequestedQuantity { get; set; }
        public int AvailableQuantity { get; set; }
        public bool IsAvailable { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class CompatibilityCheckDto
    {
        public bool AllComponentsCompatible { get; set; }
        public List<CompatibilityIssueDto> Issues { get; set; } = new List<CompatibilityIssueDto>();
    }

    public class CompatibilityIssueDto
    {
        public int ComponentAId { get; set; }
        public int ComponentBId { get; set; }
        public string ComponentAName { get; set; }
        public string ComponentBName { get; set; }
        public string Issue { get; set; }
    }

    // Order Processing DTOs
    public class OrderCalculationDto
    {
        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalAmount { get; set; }
        public List<ItemCalculationDto> ItemCalculations { get; set; } = new List<ItemCalculationDto>();
    }

    public class ItemCalculationDto
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal ItemUnitPrice { get; set; }
        public decimal ItemTotalPrice { get; set; }
        public List<ComponentCalculationDto> ComponentCalculations { get; set; } = new List<ComponentCalculationDto>();
    }

    public class ComponentCalculationDto
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public int QuantityUsed { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

    // Search and Filter DTOs
    public class ComponentSearchDto
    {
        public string SearchTerm { get; set; }
        public ComponentType? ComponentType { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? InStock { get; set; }
        public bool? IsActive { get; set; }
    }

    public class OrderSearchDto
    {
        public string SearchTerm { get; set; }
        public OrderStatus? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CustomerId { get; set; }
        public int? ClerkId { get; set; }
    }
}

// Service Interfaces
namespace FurnitureStore.Services
{
    public interface IUserService
    {
        Task<UserProfileDto> RegisterUserAsync(UserRegistrationDto dto);
        Task<string> LoginAsync(UserLoginDto dto);
        Task<UserProfileDto> GetUserProfileAsync(int userId);
        Task<UserProfileDto> UpdateUserAsync(int userId, UserUpdateDto dto);
        Task<List<UserProfileDto>> GetUsersByRoleAsync(UserType userType);
    }

    public interface IAddressService
    {
        Task<List<AddressDto>> GetUserAddressesAsync(int userId);
        Task<AddressDto> GetAddressAsync(int addressId);
        Task<AddressDto> CreateAddressAsync(int userId, CreateAddressDto dto);
        Task<AddressDto> UpdateAddressAsync(int addressId, CreateAddressDto dto);
        Task DeleteAddressAsync(int addressId);
    }

    public interface IComponentService
    {
        Task<PaginatedResponse<ComponentDto>> GetComponentsAsync(int page, int pageSize, ComponentType? type, bool? lowStockOnly);
        Task<ComponentDto> GetComponentAsync(int componentId);
        Task<ComponentDto> CreateComponentAsync(CreateComponentDto dto);
        Task<ComponentDto> UpdateComponentAsync(int componentId, UpdateComponentDto dto);
        Task<ComponentDto> AdjustStockAsync(int componentId, StockAdjustmentDto dto);
        Task<List<ComponentCompatibilityDto>> GetComponentCompatibilityAsync(int componentId);
        Task<ComponentCompatibilityDto> CreateCompatibilityAsync(CreateCompatibilityDto dto);
        Task<ComponentValidationResponse> ValidateComponentsAsync(List<CreateItemComponentDto> components);
        Task<List<ComponentDto>> SearchComponentsAsync(ComponentSearchDto searchDto);
    }

    public interface IDashboardService
    {
        Task<CustomerDashboardDto> GetCustomerDashboardAsync(int customerId);
        Task<ClerkDashboardDto> GetClerkDashboardAsync(int clerkId);
        Task<ManagerDashboardDto> GetManagerDashboardAsync();
    }
}

// Additional DTOs for advanced features
namespace FurnitureStore.DTOs
{
    public class BulkStockUpdateDto
    {
        [Required]
        public List<ComponentStockUpdateDto> Updates { get; set; } = new List<ComponentStockUpdateDto>();

        public string Notes { get; set; }
    }

    public class ComponentStockUpdateDto
    {
        [Required]
        public int ComponentId { get; set; }

        [Required]
        public int NewStockLevel { get; set; }
    }

    public class OrderStatusHistoryDto
    {
        public int LogId { get; set; }
        public OrderStatus StatusFrom { get; set; }
        public OrderStatus StatusTo { get; set; }
        public string UserName { get; set; }
        public string Notes { get; set; }
        public DateTime ChangedAt { get; set; }
    }

    public class ComponentUsageAnalyticsDto
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public ComponentType ComponentType { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalQuantitySold { get; set; }
        public int TimesOrdered { get; set; }
        public decimal AverageQuantityPerOrder { get; set; }
        public List<MonthlyUsageDto> MonthlyUsage { get; set; } = new List<MonthlyUsageDto>();
    }

    public class MonthlyUsageDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int TimesOrdered { get; set; }
        public int TotalQuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class FurnitureCustomizationDto
    {
        public int TemplateId { get; set; }
        public FurnitureTemplateDto Template { get; set; }
        public List<ComponentGroupDto> ComponentGroups { get; set; } = new List<ComponentGroupDto>();
        public decimal BasePrice { get; set; }
        public decimal EstimatedTotalPrice { get; set; }
        public bool CanCustomize { get; set; }
        public List<string> CustomizationRestrictions { get; set; } = new List<string>();
    }

    public class ComponentGroupDto
    {
        public ComponentType ComponentType { get; set; }
        public ComponentRole Role { get; set; }
        public bool IsRequired { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public List<ComponentDto> AvailableComponents { get; set; } = new List<ComponentDto>();
    }

    public class OrderTrackingDto
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public OrderStatus CurrentStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedCompletionDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public List<OrderStatusHistoryDto> StatusHistory { get; set; } = new List<OrderStatusHistoryDto>();
        public List<CustomFurnitureItemDto> Items { get; set; } = new List<CustomFurnitureItemDto>();
        public AddressDto ShippingAddress { get; set; }
        public string TrackingInformation { get; set; }
    }
}

// Additional Controllers for Complete System
namespace FurnitureStore.Controllers
{
    // Analytics Controller for Advanced Reporting
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("component-usage/{componentId}")]
        public async Task<ActionResult<ApiResponse<ComponentUsageAnalyticsDto>>> GetComponentUsageAnalytics(int componentId)
        {
            try
            {
                var analytics = await _analyticsService.GetComponentUsageAnalyticsAsync(componentId);
                return Ok(new ApiResponse<ComponentUsageAnalyticsDto> { Success = true, Data = analytics });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<ComponentUsageAnalyticsDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("revenue-trends")]
        public async Task<ActionResult<ApiResponse<List<RevenueTrendDto>>>> GetRevenueTrends(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] string groupBy = "month")
        {
            try
            {
                var trends = await _analyticsService.GetRevenueTrendsAsync(startDate, endDate, groupBy);
                return Ok(new ApiResponse<List<RevenueTrendDto>> { Success = true, Data = trends });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<List<RevenueTrendDto>> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("customer-insights/{customerId}")]
        public async Task<ActionResult<ApiResponse<CustomerInsightsDto>>> GetCustomerInsights(int customerId)
        {
            try
            {
                var insights = await _analyticsService.GetCustomerInsightsAsync(customerId);
                return Ok(new ApiResponse<CustomerInsightsDto> { Success = true, Data = insights });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<CustomerInsightsDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("top-performing-templates")]
        public async Task<ActionResult<ApiResponse<List<TemplatePerformanceDto>>>> GetTopPerformingTemplates(
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int topCount = 10)
        {
            try
            {
                startDate ??= DateTime.Now.AddMonths(-3);
                endDate ??= DateTime.Now;

                var performance = await _analyticsService.GetTopPerformingTemplatesAsync(startDate.Value, endDate.Value, topCount);
                return Ok(new ApiResponse<List<TemplatePerformanceDto>> { Success = true, Data = performance });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<List<TemplatePerformanceDto>> { Success = false, Message = ex.Message });
            }
        }
    }

    // Order Tracking Controller for Customer-Facing Tracking
    [ApiController]
    [Route("api/order-tracking")]
    public class OrderTrackingController : ControllerBase
    {
        private readonly IOrderTrackingService _orderTrackingService;

        public OrderTrackingController(IOrderTrackingService orderTrackingService)
        {
            _orderTrackingService = orderTrackingService;
        }

        [HttpGet("{orderNumber}")]
        public async Task<ActionResult<ApiResponse<OrderTrackingDto>>> TrackOrder(string orderNumber)
        {
            try
            {
                var tracking = await _orderTrackingService.GetOrderTrackingAsync(orderNumber);
                return Ok(new ApiResponse<OrderTrackingDto> { Success = true, Data = tracking });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<OrderTrackingDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("customer/{customerId}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<List<OrderTrackingDto>>>> GetCustomerOrderTracking(int customerId)
        {
            try
            {
                var trackingList = await _orderTrackingService.GetCustomerOrderTrackingAsync(customerId);
                return Ok(new ApiResponse<List<OrderTrackingDto>> { Success = true, Data = trackingList });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<List<OrderTrackingDto>> { Success = false, Message = ex.Message });
            }
        }
    }

    // Furniture Customization Controller
    [ApiController]
    [Route("api/customization")]
    public class CustomizationController : ControllerBase
    {
        private readonly ICustomizationService _customizationService;

        public CustomizationController(ICustomizationService customizationService)
        {
            _customizationService = customizationService;
        }

        [HttpGet("template/{templateId}")]
        public async Task<ActionResult<ApiResponse<FurnitureCustomizationDto>>> GetCustomizationOptions(int templateId)
        {
            try
            {
                var customization = await _customizationService.GetCustomizationOptionsAsync(templateId);
                return Ok(new ApiResponse<FurnitureCustomizationDto> { Success = true, Data = customization });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<FurnitureCustomizationDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("validate-configuration")]
        public async Task<ActionResult<ComponentValidationResponse>> ValidateConfiguration([FromBody] CustomizationValidationRequest request)
        {
            try
            {
                var validation = await _customizationService.ValidateCustomizationAsync(request);
                return Ok(validation);
            }
            catch (Exception ex)
            {
                return BadRequest(new ComponentValidationResponse { IsValid = false, ValidationErrors = { ex.Message } });
            }
        }

        [HttpPost("calculate-price")]
        public async Task<ActionResult<ApiResponse<CustomizationPricingDto>>> CalculateCustomizationPrice([FromBody] CustomizationPricingRequest request)
        {
            try
            {
                var pricing = await _customizationService.CalculateCustomizationPriceAsync(request);
                return Ok(new ApiResponse<CustomizationPricingDto> { Success = true, Data = pricing });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<CustomizationPricingDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("compatibility/{componentId}")]
        public async Task<ActionResult<ApiResponse<List<ComponentDto>>>> GetCompatibleComponents(int componentId, [FromQuery] int templateId)
        {
            try
            {
                var compatible = await _customizationService.GetCompatibleComponentsAsync(componentId, templateId);
                return Ok(new ApiResponse<List<ComponentDto>> { Success = true, Data = compatible });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<List<ComponentDto>> { Success = false, Message = ex.Message });
            }
        }
    }

    // Notification Controller
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ApiResponse<List<NotificationDto>>>> GetUserNotifications(int userId, [FromQuery] bool unreadOnly = false)
        {
            try
            {
                var notifications = await _notificationService.GetUserNotificationsAsync(userId, unreadOnly);
                return Ok(new ApiResponse<List<NotificationDto>> { Success = true, Data = notifications });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<List<NotificationDto>> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("{notificationId}/read")]
        public async Task<ActionResult<ApiResponse<bool>>> MarkAsRead(int notificationId)
        {
            try
            {
                await _notificationService.MarkAsReadAsync(notificationId);
                return Ok(new ApiResponse<bool> { Success = true, Data = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("user/{userId}/read-all")]
        public async Task<ActionResult<ApiResponse<bool>>> MarkAllAsRead(int userId)
        {
            try
            {
                await _notificationService.MarkAllAsReadAsync(userId);
                return Ok(new ApiResponse<bool> { Success = true, Data = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<bool> { Success = false, Message = ex.Message });
            }
        }
    }

    // Bulk Operations Controller
    [ApiController]
    [Route("api/bulk-operations")]
    [Authorize(Roles = "Manager,Admin")]
    public class BulkOperationsController : ControllerBase
    {
        private readonly IBulkOperationsService _bulkOperationsService;

        public BulkOperationsController(IBulkOperationsService bulkOperationsService)
        {
            _bulkOperationsService = bulkOperationsService;
        }

        [HttpPost("stock-update")]
        public async Task<ActionResult<ApiResponse<BulkOperationResultDto>>> BulkStockUpdate([FromBody] BulkStockUpdateDto dto)
        {
            try
            {
                var result = await _bulkOperationsService.BulkStockUpdateAsync(dto);
                return Ok(new ApiResponse<BulkOperationResultDto> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<BulkOperationResultDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("price-update")]
        public async Task<ActionResult<ApiResponse<BulkOperationResultDto>>> BulkPriceUpdate([FromBody] BulkPriceUpdateDto dto)
        {
            try
            {
                var result = await _bulkOperationsService.BulkPriceUpdateAsync(dto);
                return Ok(new ApiResponse<BulkOperationResultDto> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<BulkOperationResultDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("component-activation")]
        public async Task<ActionResult<ApiResponse<BulkOperationResultDto>>> BulkComponentActivation([FromBody] BulkActivationDto dto)
        {
            try
            {
                var result = await _bulkOperationsService.BulkComponentActivationAsync(dto);
                return Ok(new ApiResponse<BulkOperationResultDto> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<BulkOperationResultDto> { Success = false, Message = ex.Message });
            }
        }
    }

    // Import/Export Controller
    [ApiController]
    [Route("api/import-export")]
    [Authorize(Roles = "Manager,Admin")]
    public class ImportExportController : ControllerBase
    {
        private readonly IImportExportService _importExportService;

        public ImportExportController(IImportExportService importExportService)
        {
            _importExportService = importExportService;
        }

        [HttpPost("import-components")]
        public async Task<ActionResult<ApiResponse<ImportResultDto>>> ImportComponents(IFormFile file)
        {
            try
            {
                var result = await _importExportService.ImportComponentsAsync(file);
                return Ok(new ApiResponse<ImportResultDto> { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<ImportResultDto> { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("export-components")]
        public async Task<ActionResult> ExportComponents([FromQuery] ComponentType? componentType = null)
        {
            try
            {
                var fileContent = await _importExportService.ExportComponentsAsync(componentType);
                return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "components.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("export-orders")]
        public async Task<ActionResult> ExportOrders([FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            try
            {
                startDate ??= DateTime.Now.AddMonths(-1);
                endDate ??= DateTime.Now;

                var fileContent = await _importExportService.ExportOrdersAsync(startDate.Value, endDate.Value);
                return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "orders.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("export-inventory-report")]
        public async Task<ActionResult> ExportInventoryReport()
        {
            try
            {
                var fileContent = await _importExportService.ExportInventoryReportAsync();
                return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "inventory_report.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}

// Extended DTOs for Additional Controllers
namespace FurnitureStore.DTOs
{
    // Analytics DTOs
    public class RevenueT RendDto
    {
        public DateTime Period { get; set; }
        public decimal Revenue { get; set; }
        public int OrderCount { get; set; }
        public decimal AverageOrderValue { get; set; }
    }

    public class CustomerInsightsDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime FirstOrderDate { get; set; }
        public DateTime LastOrderDate { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal AverageOrderValue { get; set; }
        public List<FurnitureType> PreferredFurnitureTypes { get; set; } = new List<FurnitureType>();
        public List<ComponentType> PreferredComponentTypes { get; set; } = new List<ComponentType>();
        public CustomerSegment Segment { get; set; }
    }

    public class TemplatePerformanceDto
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public FurnitureType FurnitureType { get; set; }
        public int TimesOrdered { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AveragePrice { get; set; }
        public double AverageCustomerRating { get; set; }
    }

    // Customization DTOs
    public class CustomizationValidationRequest
    {
        [Required]
        public int TemplateId { get; set; }

        [Required]
        public List<CreateItemComponentDto> SelectedComponents { get; set; } = new List<CreateItemComponentDto>();
    }

    public class CustomizationPricingRequest
    {
        [Required]
        public int TemplateId { get; set; }

        [Required]
        public List<CreateItemComponentDto> SelectedComponents { get; set; } = new List<CreateItemComponentDto>();

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }

    public class CustomizationPricingDto
    {
        public decimal BasePrice { get; set; }
        public decimal ComponentsTotal { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ComponentPricingDto> ComponentBreakdown { get; set; } = new List<ComponentPricingDto>();
        public List<string> PricingNotes { get; set; } = new List<string>();
    }

    
    // Notification DTOs
    public class NotificationDto
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public NotificationType Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string ActionUrl { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RelatedEntityId { get; set; }
    }

    // Bulk Operations DTOs
    public class BulkPriceUpdateDto
    {
        [Required]
        public List<ComponentPriceUpdateDto> Updates { get; set; } = new List<ComponentPriceUpdateDto>();

        public string Notes { get; set; }
    }

    public class ComponentPriceUpdateDto
    {
        [Required]
        public int ComponentId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal NewPrice { get; set; }
    }

    public class BulkActivationDto
    {
        [Required]
        public List<int> ComponentIds { get; set; } = new List<int>();

        [Required]
        public bool IsActive { get; set; }

        public string Reason { get; set; }
    }

    public class BulkOperationResultDto
    {
        public int TotalProcessed { get; set; }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
        public List<BulkOperationErrorDto> Errors { get; set; } = new List<BulkOperationErrorDto>();
        public DateTime ProcessedAt { get; set; }
    }

    public class BulkOperationErrorDto
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ErrorMessage { get; set; }
    }

    // Import/Export DTOs
    public class ImportResultDto
    {
        public int TotalRows { get; set; }
        public int SuccessfulImports { get; set; }
        public int FailedImports { get; set; }
        public List<ImportErrorDto> Errors { get; set; } = new List<ImportErrorDto>();
        public DateTime ImportedAt { get; set; }
    }

    public class ImportErrorDto
    {
        public int RowNumber { get; set; }
        public string FieldName { get; set; }
        public string ErrorMessage { get; set; }
        public string RowData { get; set; }
    }

    // Additional Enum Types
    public enum CustomerSegment
    {
        New,
        Regular,
        Premium,
        VIP,
        Inactive
    }

    public enum NotificationType
    {
        OrderStatusUpdate,
        LowStock,
        NewOrder,
        SystemAlert,
        Promotion,
        Reminder
    }
}

// Extended Service Interfaces
namespace FurnitureStore.Services
{
    public interface IAnalyticsService
    {
        Task<ComponentUsageAnalyticsDto> GetComponentUsageAnalyticsAsync(int componentId);
        Task<List<RevenueT RendDto>> GetRevenueTrendsAsync(DateTime startDate, DateTime endDate, string groupBy);
        Task<CustomerInsightsDto> GetCustomerInsightsAsync(int customerId);
        Task<List<TemplatePerformanceDto>> GetTopPerformingTemplatesAsync(DateTime startDate, DateTime endDate, int topCount);
        Task<List<CustomerInsightsDto>> GetCustomerSegmentationAsync();
    }

    public interface IOrderTrackingService
    {
        Task<OrderTrackingDto> GetOrderTrackingAsync(string orderNumber);
        Task<List<OrderTrackingDto>> GetCustomerOrderTrackingAsync(int customerId);
        Task UpdateTrackingInformationAsync(int orderId, string trackingInfo);
    }

    public interface ICustomizationService
    {
        Task<FurnitureCustomizationDto> GetCustomizationOptionsAsync(int templateId);
        Task<ComponentValidationResponse> ValidateCustomizationAsync(CustomizationValidationRequest request);
        Task<CustomizationPricingDto> CalculateCustomizationPriceAsync(CustomizationPricingRequest request);
        Task<List<ComponentDto>> GetCompatibleComponentsAsync(int componentId, int templateId);
        Task<List<ComponentDto>> GetRecommendedComponentsAsync(int templateId, List<int> selectedComponentIds);
    }

    public interface INotificationService
    {
        Task<List<NotificationDto>> GetUserNotificationsAsync(int userId, bool unreadOnly);
        Task<NotificationDto> CreateNotificationAsync(int userId, NotificationType type, string title, string message, string actionUrl = null, string relatedEntityId = null);
        Task MarkAsReadAsync(int notificationId);
        Task MarkAllAsReadAsync(int userId);
        Task DeleteNotificationAsync(int notificationId);
        Task SendLowStockAlertsAsync();
        Task SendOrderStatusNotificationsAsync(int orderId, OrderStatus newStatus);
    }

    public interface IBulkOperationsService
    {
        Task<BulkOperationResultDto> BulkStockUpdateAsync(BulkStockUpdateDto dto);
        Task<BulkOperationResultDto> BulkPriceUpdateAsync(BulkPriceUpdateDto dto);
        Task<BulkOperationResultDto> BulkComponentActivationAsync(BulkActivationDto dto);
        Task<BulkOperationResultDto> BulkDeleteComponentsAsync(List<int> componentIds);
    }

    public interface IImportExportService
    {
        Task<ImportResultDto> ImportComponentsAsync(IFormFile file);
        Task<ImportResultDto> ImportCompatibilityRulesAsync(IFormFile file);
        Task<byte[]> ExportComponentsAsync(ComponentType? componentType);
        Task<byte[]> ExportOrdersAsync(DateTime startDate, DateTime endDate);
        Task<byte[]> ExportInventoryReportAsync();
        Task<byte[]> ExportSalesReportAsync(DateTime startDate, DateTime endDate);
    }

    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
        Task RemoveAsync(string key);
        Task RemovePatternAsync(string pattern);
        Task<bool> ExistsAsync(string key);
    }

    public interface IEmailService
    {
        Task SendOrderConfirmationEmailAsync(int orderId);
        Task SendOrderStatusUpdateEmailAsync(int orderId, OrderStatus newStatus);
        Task SendLowStockAlertEmailAsync(List<StockLevelDto> lowStockItems);
        Task SendWelcomeEmailAsync(int userId);
        Task SendPasswordResetEmailAsync(string email, string resetToken);
    }
}

    // Basic implementations for other services (you can expand these)
    public class AddressService : IAddressService
    {
        private readonly FurnitureStoreDbContext _context;

        public AddressService(FurnitureStoreDbContext context)
        {
            _context = context;
        }

        // Implement all IAddressService methods
        public Task<List<AddressDto>> GetUserAddressesAsync(int userId) => throw new NotImplementedException();
        public Task<AddressDto> GetAddressAsync(int addressId) => throw new NotImplementedException();
        public Task<AddressDto> CreateAddressAsync(int userId, CreateAddressDto dto) => throw new NotImplementedException();
        public Task<AddressDto> UpdateAddressAsync(int addressId, CreateAddressDto dto) => throw new NotImplementedException();
        public Task DeleteAddressAsync(int addressId) => throw new NotImplementedException();
    }


    public class DashboardService : IDashboardService
    {
        private readonly FurnitureStoreDbContext _context;
        public DashboardService(FurnitureStoreDbContext context) { _context = context; }

        public Task<CustomerDashboardDto> GetCustomerDashboardAsync(int customerId) => throw new NotImplementedException();
        public Task<ClerkDashboardDto> GetClerkDashboardAsync(int clerkId) => throw new NotImplementedException();
        public Task<ManagerDashboardDto> GetManagerDashboardAsync() => throw new NotImplementedException();
    }

    public class AnalyticsService : IAnalyticsService
    {
        private readonly FurnitureStoreDbContext _context;
        public AnalyticsService(FurnitureStoreDbContext context) { _context = context; }

        public Task<ComponentUsageAnalyticsDto> GetComponentUsageAnalyticsAsync(int componentId) => throw new NotImplementedException();
        public Task<List<RevenueTrendDto>> GetRevenueTrendsAsync(DateTime startDate, DateTime endDate, string groupBy) => throw new NotImplementedException();
        public Task<CustomerInsightsDto> GetCustomerInsightsAsync(int customerId) => throw new NotImplementedException();
        public Task<List<TemplatePerformanceDto>> GetTopPerformingTemplatesAsync(DateTime startDate, DateTime endDate, int topCount) => throw new NotImplementedException();
        public Task<List<CustomerInsightsDto>> GetCustomerSegmentationAsync() => throw new NotImplementedException();
    }

    public class OrderTrackingService : IOrderTrackingService
    {
        private readonly FurnitureStoreDbContext _context;
        public OrderTrackingService(FurnitureStoreDbContext context) { _context = context; }

        public Task<OrderTrackingDto> GetOrderTrackingAsync(string orderNumber) => throw new NotImplementedException();
        public Task<List<OrderTrackingDto>> GetCustomerOrderTrackingAsync(int customerId) => throw new NotImplementedException();
        public Task UpdateTrackingInformationAsync(int orderId, string trackingInfo) => throw new NotImplementedException();
    }

    public class CustomizationService : ICustomizationService
    {
        private readonly FurnitureStoreDbContext _context;
        public CustomizationService(FurnitureStoreDbContext context) { _context = context; }

        public Task<FurnitureCustomizationDto> GetCustomizationOptionsAsync(int templateId) => throw new NotImplementedException();
        public Task<ComponentValidationResponse> ValidateCustomizationAsync(CustomizationValidationRequest request) => throw new NotImplementedException();
        public Task<CustomizationPricingDto> CalculateCustomizationPriceAsync(CustomizationPricingRequest request) => throw new NotImplementedException();
        public Task<List<ComponentDto>> GetCompatibleComponentsAsync(int componentId, int templateId) => throw new NotImplementedException();
        public Task<List<ComponentDto>> GetRecommendedComponentsAsync(int templateId, List<int> selectedComponentIds) => throw new NotImplementedException();
    }

    public class NotificationService : INotificationService
    {
        private readonly FurnitureStoreDbContext _context;
        public NotificationService(FurnitureStoreDbContext context) { _context = context; }

        public Task<List<NotificationDto>> GetUserNotificationsAsync(int userId, bool unreadOnly) => throw new NotImplementedException();
        public Task<NotificationDto> CreateNotificationAsync(int userId, NotificationType type, string title, string message, string actionUrl = null, string relatedEntityId = null) => throw new NotImplementedException();
        public Task MarkAsReadAsync(int notificationId) => throw new NotImplementedException();
        public Task MarkAllAsReadAsync(int userId) => throw new NotImplementedException();
        public Task DeleteNotificationAsync(int notificationId) => throw new NotImplementedException();
        public Task SendLowStockAlertsAsync() => throw new NotImplementedException();
        public Task SendOrderStatusNotificationsAsync(int orderId, OrderStatus newStatus) => throw new NotImplementedException();
    }

    public class BulkOperationsService : IBulkOperationsService
    {
        private readonly FurnitureStoreDbContext _context;
        public BulkOperationsService(FurnitureStoreDbContext context) { _context = context; }

        public Task<BulkOperationResultDto> BulkStockUpdateAsync(BulkStockUpdateDto dto) => throw new NotImplementedException();
        public Task<BulkOperationResultDto> BulkPriceUpdateAsync(BulkPriceUpdateDto dto) => throw new NotImplementedException();
        public Task<BulkOperationResultDto> BulkComponentActivationAsync(BulkActivationDto dto) => throw new NotImplementedException();
        public Task<BulkOperationResultDto> BulkDeleteComponentsAsync(List<int> componentIds) => throw new NotImplementedException();
    }

    public class ImportExportService : IImportExportService
    {
        private readonly FurnitureStoreDbContext _context;
        public ImportExportService(FurnitureStoreDbContext context) { _context = context; }

        public Task<ImportResultDto> ImportComponentsAsync(IFormFile file) => throw new NotImplementedException();
        public Task<ImportResultDto> ImportCompatibilityRulesAsync(IFormFile file) => throw new NotImplementedException();
        public Task<byte[]> ExportComponentsAsync(ComponentType? componentType) => throw new NotImplementedException();
        public Task<byte[]> ExportOrdersAsync(DateTime startDate, DateTime endDate) => throw new NotImplementedException();
        public Task<byte[]> ExportInventoryReportAsync() => throw new NotImplementedException();
        public Task<byte[]> ExportSalesReportAsync(DateTime startDate, DateTime endDate) => throw new NotImplementedException();
    }

    public class CacheService : ICacheService
    {
        public Task<T> GetAsync<T>(string key) => throw new NotImplementedException();
        public Task SetAsync<T>(string key, T value, TimeSpan? expiry = null) => throw new NotImplementedException();
        public Task RemoveAsync(string key) => throw new NotImplementedException();
        public Task RemovePatternAsync(string pattern) => throw new NotImplementedException();
        public Task<bool> ExistsAsync(string key) => throw new NotImplementedException();
    }

    public class EmailService : IEmailService
    {
        public Task SendOrderConfirmationEmailAsync(int orderId) => throw new NotImplementedException();
        public Task SendOrderStatusUpdateEmailAsync(int orderId, OrderStatus newStatus) => throw new NotImplementedException();
        public Task SendLowStockAlertEmailAsync(List<StockLevelDto> lowStockItems) => throw new NotImplementedException();
        public Task SendWelcomeEmailAsync(int userId) => throw new NotImplementedException();
        public Task SendPasswordResetEmailAsync(string email, string resetToken) => throw new NotImplementedException();
    }
}

// Exception classes
namespace FurnitureStore.Services
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}

// ===========================================
// SERVICE IMPLEMENTATIONS
// ===========================================

namespace FurnitureStore.Services
{
    // Basic implementations for other services (you can expand these)
    public class AddressService : IAddressService
    {
        private readonly FurnitureStoreDbContext _context;

        public AddressService(FurnitureStoreDbContext context)
        {
            _context = context;
        }

        // Implement all IAddressService methods
        public Task<List<AddressDto>> GetUserAddressesAsync(int userId) => throw new NotImplementedException();
        public Task<AddressDto> GetAddressAsync(int addressId) => throw new NotImplementedException();
        public Task<AddressDto> CreateAddressAsync(int userId, CreateAddressDto dto) => throw new NotImplementedException();
        public Task<AddressDto> UpdateAddressAsync(int addressId, CreateAddressDto dto) => throw new NotImplementedException();
        public Task DeleteAddressAsync(int addressId) => throw new NotImplementedException();
    }

    public class DashboardService : IDashboardService
    {
        private readonly FurnitureStoreDbContext _context;
        public DashboardService(FurnitureStoreDbContext context) { _context = context; }

        public Task<CustomerDashboardDto> GetCustomerDashboardAsync(int customerId) => throw new NotImplementedException();
        public Task<ClerkDashboardDto> GetClerkDashboardAsync(int clerkId) => throw new NotImplementedException();
        public Task<ManagerDashboardDto> GetManagerDashboardAsync() => throw new NotImplementedException();
    }

    public class AnalyticsService : IAnalyticsService
    {
        private readonly FurnitureStoreDbContext _context;
        public AnalyticsService(FurnitureStoreDbContext context) { _context = context; }

        public Task<ComponentUsageAnalyticsDto> GetComponentUsageAnalyticsAsync(int componentId) => throw new NotImplementedException();
        public Task<List<RevenueTrendDto>> GetRevenueTrendsAsync(DateTime startDate, DateTime endDate, string groupBy) => throw new NotImplementedException();
        public Task<CustomerInsightsDto> GetCustomerInsightsAsync(int customerId) => throw new NotImplementedException();
        public Task<List<TemplatePerformanceDto>> GetTopPerformingTemplatesAsync(DateTime startDate, DateTime endDate, int topCount) => throw new NotImplementedException();
        public Task<List<CustomerInsightsDto>> GetCustomerSegmentationAsync() => throw new NotImplementedException();
    }

    public class OrderTrackingService : IOrderTrackingService
    {
        private readonly FurnitureStoreDbContext _context;
        public OrderTrackingService(FurnitureStoreDbContext context) { _context = context; }

        public Task<OrderTrackingDto> GetOrderTrackingAsync(string orderNumber) => throw new NotImplementedException();
        public Task<List<OrderTrackingDto>> GetCustomerOrderTrackingAsync(int customerId) => throw new NotImplementedException();
        public Task UpdateTrackingInformationAsync(int orderId, string trackingInfo) => throw new NotImplementedException();
    }

    public class CustomizationService : ICustomizationService
    {
        private readonly FurnitureStoreDbContext _context;
        public CustomizationService(FurnitureStoreDbContext context) { _context = context; }

        public Task<FurnitureCustomizationDto> GetCustomizationOptionsAsync(int templateId) => throw new NotImplementedException();
        public Task<ComponentValidationResponse> ValidateCustomizationAsync(CustomizationValidationRequest request) => throw new NotImplementedException();
        public Task<CustomizationPricingDto> CalculateCustomizationPriceAsync(CustomizationPricingRequest request) => throw new NotImplementedException();
        public Task<List<ComponentDto>> GetCompatibleComponentsAsync(int componentId, int templateId) => throw new NotImplementedException();
        public Task<List<ComponentDto>> GetRecommendedComponentsAsync(int templateId, List<int> selectedComponentIds) => throw new NotImplementedException();
    }

    public class NotificationService : INotificationService
    {
        private readonly FurnitureStoreDbContext _context;
        public NotificationService(FurnitureStoreDbContext context) { _context = context; }

        public Task<List<NotificationDto>> GetUserNotificationsAsync(int userId, bool unreadOnly) => throw new NotImplementedException();
        public Task<NotificationDto> CreateNotificationAsync(int userId, NotificationType type, string title, string message, string actionUrl = null, string relatedEntityId = null) => throw new NotImplementedException();
        public Task MarkAsReadAsync(int notificationId) => throw new NotImplementedException();
        public Task MarkAllAsReadAsync(int userId) => throw new NotImplementedException();
        public Task DeleteNotificationAsync(int notificationId) => throw new NotImplementedException();
        public Task SendLowStockAlertsAsync() => throw new NotImplementedException();
        public Task SendOrderStatusNotificationsAsync(int orderId, OrderStatus newStatus) => throw new NotImplementedException();
    }

    public class BulkOperationsService : IBulkOperationsService
    {
        private readonly FurnitureStoreDbContext _context;
        public BulkOperationsService(FurnitureStoreDbContext context) { _context = context; }

        public Task<BulkOperationResultDto> BulkStockUpdateAsync(BulkStockUpdateDto dto) => throw new NotImplementedException();
        public Task<BulkOperationResultDto> BulkPriceUpdateAsync(BulkPriceUpdateDto dto) => throw new NotImplementedException();
        public Task<BulkOperationResultDto> BulkComponentActivationAsync(BulkActivationDto dto) => throw new NotImplementedException();
        public Task<BulkOperationResultDto> BulkDeleteComponentsAsync(List<int> componentIds) => throw new NotImplementedException();
    }

    public class ImportExportService : IImportExportService
    {
        private readonly FurnitureStoreDbContext _context;
        public ImportExportService(FurnitureStoreDbContext context) { _context = context; }

        public Task<ImportResultDto> ImportComponentsAsync(IFormFile file) => throw new NotImplementedException();
        public Task<ImportResultDto> ImportCompatibilityRulesAsync(IFormFile file) => throw new NotImplementedException();
        public Task<byte[]> ExportComponentsAsync(ComponentType? componentType) => throw new NotImplementedException();
        public Task<byte[]> ExportOrdersAsync(DateTime startDate, DateTime endDate) => throw new NotImplementedException();
        public Task<byte[]> ExportInventoryReportAsync() => throw new NotImplementedException();
        public Task<byte[]> ExportSalesReportAsync(DateTime startDate, DateTime endDate) => throw new NotImplementedException();
    }

    public class CacheService : ICacheService
    {
        public Task<T> GetAsync<T>(string key) => throw new NotImplementedException();
        public Task SetAsync<T>(string key, T value, TimeSpan? expiry = null) => throw new NotImplementedException();
        public Task RemoveAsync(string key) => throw new NotImplementedException();
        public Task RemovePatternAsync(string pattern) => throw new NotImplementedException();
        public Task<bool> ExistsAsync(string key) => throw new NotImplementedException();
    }

    public class EmailService : IEmailService
    {
        public Task SendOrderConfirmationEmailAsync(int orderId) => throw new NotImplementedException();
        public Task SendOrderStatusUpdateEmailAsync(int orderId, OrderStatus newStatus) => throw new NotImplementedException();
        public Task SendLowStockAlertEmailAsync(List<StockLevelDto> lowStockItems) => throw new NotImplementedException();
        public Task SendWelcomeEmailAsync(int userId) => throw new NotImplementedException();
        public Task SendPasswordResetEmailAsync(string email, string resetToken) => throw new NotImplementedException();
    }
}

// Exception classes
namespace FurnitureStore.Services
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}

// DbContext
namespace FurnitureStore.Models
{
    public class FurnitureStoreDbContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Component>()
                .HasIndex(c => c.Sku)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderNumber)
                .IsUnique();

            // Configure decimal precision
            modelBuilder.Entity<Component>()
                .Property(c => c.UnitPrice)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.Subtotal)
                .HasColumnType("decimal(12,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.TaxAmount)
                .HasColumnType("decimal(12,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.ShippingCost)
                .HasColumnType("decimal(12,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(12,2)");

            // Configure foreign key relationships
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(u => u.CustomerOrders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.AssignedClerk)
                .WithMany(u => u.AssignedOrders)
                .HasForeignKey(o => o.AssignedClerkId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ComponentCompatibility>()
                .HasOne(cc => cc.ComponentA)
                .WithMany(c => c.CompatibilitiesAsA)
                .HasForeignKey(cc => cc.ComponentAId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComponentCompatibility>()
                .HasOne(cc => cc.ComponentB)
                .WithMany(c => c.CompatibilitiesAsB)
                .HasForeignKey(cc => cc.ComponentBId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed furniture templates
            modelBuilder.Entity<FurnitureTemplate>().HasData(
                new FurnitureTemplate
                {
                    TemplateId = 1,
                    TemplateName = "Modern Dining Table",
                    Description = "Contemporary dining table design",
                    FurnitureType = FurnitureType.Table,
                    BasePrice = 199.99m,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new FurnitureTemplate
                {
                    TemplateId = 2,
                    TemplateName = "Office Desk",
                    Description = "Professional office desk with storage",
                    FurnitureType = FurnitureType.Desk,
                    BasePrice = 249.99m,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed component compatibility rules
            modelBuilder.Entity<ComponentCompatibility>().HasData(
                new ComponentCompatibility
                {
                    CompatibilityId = 1,
                    ComponentAId = 1, // Oak Table Top
                    ComponentBId = 2, // Steel Legs
                    IsCompatible = true,
                    CompatibilityNotes = "Oak top works well with steel legs",
                    CreatedAt = DateTime.UtcNow
                },
                new ComponentCompatibility
                {
                    CompatibilityId = 2,
                    ComponentAId = 3, // Pine Drawer
                    ComponentBId = 4, // Brass Handles
                    IsCompatible = true,
                    CompatibilityNotes = "Pine drawers compatible with brass handles",
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
*/
/**


    public class AnalyticsService : IAnalyticsService
    {
        private readonly FurnitureStoreDbContext _context;
        public AnalyticsService(FurnitureStoreDbContext context) { _context = context; }

        public Task<ComponentUsageAnalyticsDto> GetComponentUsageAnalyticsAsync(int componentId) => throw new NotImplementedException();
        public Task<List<RevenueTrendDto>> GetRevenueTrendsAsync(DateTime startDate, DateTime endDate, string groupBy) => throw new NotImplementedException();
        public Task<CustomerInsightsDto> GetCustomerInsightsAsync(int customerId) => throw new NotImplementedException();
        public Task<List<TemplatePerformanceDto>> GetTopPerformingTemplatesAsync(DateTime startDate, DateTime endDate, int topCount) => throw new NotImplementedException();
        public Task<List<CustomerInsightsDto>> GetCustomerSegmentationAsync() => throw new NotImplementedException();
    }

    public class OrderTrackingService : IOrderTrackingService
    {
        private readonly FurnitureStoreDbContext _context;
        public OrderTrackingService(FurnitureStoreDbContext context) { _context = context; }

        public Task<OrderTrackingDto> GetOrderTrackingAsync(string orderNumber) => throw new NotImplementedException();
        public Task<List<OrderTrackingDto>> GetCustomerOrderTrackingAsync(int customerId) => throw new NotImplementedException();
        public Task UpdateTrackingInformationAsync(int orderId, string trackingInfo) => throw new NotImplementedException();
    }

    public class NotificationService : INotificationService
    {
        private readonly FurnitureStoreDbContext _context;
        public NotificationService(FurnitureStoreDbContext context) { _context = context; }

        public Task<List<NotificationDto>> GetUserNotificationsAsync(int userId, bool unreadOnly) => throw new NotImplementedException();
        public Task<NotificationDto> CreateNotificationAsync(int userId, NotificationType type, string title, string message, string actionUrl = null, string relatedEntityId = null) => throw new NotImplementedException();
        public Task MarkAsReadAsync(int notificationId) => throw new NotImplementedException();
        public Task MarkAllAsReadAsync(int userId) => throw new NotImplementedException();
        public Task DeleteNotificationAsync(int notificationId) => throw new NotImplementedException();
        public Task SendLowStockAlertsAsync() => throw new NotImplementedException();
        public Task SendOrderStatusNotificationsAsync(int orderId, OrderStatus newStatus) => throw new NotImplementedException();
    }

    public class BulkOperationsService : IBulkOperationsService
    {
        private readonly FurnitureStoreDbContext _context;
        public BulkOperationsService(FurnitureStoreDbContext context) { _context = context; }

        public Task<BulkOperationResultDto> BulkStockUpdateAsync(BulkStockUpdateDto dto) => throw new NotImplementedException();
        public Task<BulkOperationResultDto> BulkPriceUpdateAsync(BulkPriceUpdateDto dto) => throw new NotImplementedException();
        public Task<BulkOperationResultDto> BulkComponentActivationAsync(BulkActivationDto dto) => throw new NotImplementedException();
        public Task<BulkOperationResultDto> BulkDeleteComponentsAsync(List<int> componentIds) => throw new NotImplementedException();
    }

    public class ImportExportService : IImportExportService
    {
        private readonly FurnitureStoreDbContext _context;
        public ImportExportService(FurnitureStoreDbContext context) { _context = context; }

        public Task<ImportResultDto> ImportComponentsAsync(IFormFile file) => throw new NotImplementedException();
        public Task<ImportResultDto> ImportCompatibilityRulesAsync(IFormFile file) => throw new NotImplementedException();
        public Task<byte[]> ExportComponentsAsync(ComponentType? componentType) => throw new NotImplementedException();
        public Task<byte[]> ExportOrdersAsync(DateTime startDate, DateTime endDate) => throw new NotImplementedException();
        public Task<byte[]> ExportInventoryReportAsync() => throw new NotImplementedException();
        public Task<byte[]> ExportSalesReportAsync(DateTime startDate, DateTime endDate) => throw new NotImplementedException();
    }

    public class CacheService : ICacheService
    {
        public Task<T> GetAsync<T>(string key) => throw new NotImplementedException();
        public Task SetAsync<T>(string key, T value, TimeSpan? expiry = null) => throw new NotImplementedException();
        public Task RemoveAsync(string key) => throw new NotImplementedException();
        public Task RemovePatternAsync(string pattern) => throw new NotImplementedException();
        public Task<bool> ExistsAsync(string key) => throw new NotImplementedException();
    }

    public class EmailService : IEmailService
    {
        public Task SendOrderConfirmationEmailAsync(int orderId) => throw new NotImplementedException();
        public Task SendOrderStatusUpdateEmailAsync(int orderId, OrderStatus newStatus) => throw new NotImplementedException();
        public Task SendLowStockAlertEmailAsync(List<StockLevelDto> lowStockItems) => throw new NotImplementedException();
        public Task SendWelcomeEmailAsync(int userId) => throw new NotImplementedException();
        public Task SendPasswordResetEmailAsync(string email, string resetToken) => throw new NotImplementedException();
    }
}

// Exception classes
namespace FurnitureStore.Services
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}


// ===========================================
// SERVICE IMPLEMENTATIONS
// ===========================================

namespace FurnitureStore.Services
{
    // Basic implementations for other services (you can expand these)
    public class AddressService : IAddressService
    {
        private readonly FurnitureStoreDbContext _context;

        public AddressService(FurnitureStoreDbContext context)
        {
            _context = context;
        }

        // Implement all IAddressService methods
        public Task<List<AddressDto>> GetUserAddressesAsync(int userId) => throw new NotImplementedException();
        public Task<AddressDto> GetAddressAsync(int addressId) => throw new NotImplementedException();
        public Task<AddressDto> CreateAddressAsync(int userId, CreateAddressDto dto) => throw new NotImplementedException();
        public Task<AddressDto> UpdateAddressAsync(int addressId, CreateAddressDto dto) => throw new NotImplementedException();
        public Task DeleteAddressAsync(int addressId) => throw new NotImplementedException();
    }

    public class DashboardService : IDashboardService
    {
        private readonly FurnitureStoreDbContext _context;
        public DashboardService(FurnitureStoreDbContext context) { _context = context; }

        public Task<CustomerDashboardDto> GetCustomerDashboardAsync(int customerId) => throw new NotImplementedException();
        public Task<ClerkDashboardDto> GetClerkDashboardAsync(int clerkId) => throw new NotImplementedException();
        public Task<ManagerDashboardDto> GetManagerDashboardAsync() => throw new NotImplementedException();
    }

    public class AnalyticsService : IAnalyticsService
    {
        private readonly FurnitureStoreDbContext _context;
        public AnalyticsService(FurnitureStoreDbContext context) { _context = context; }

        public Task<ComponentUsageAnalyticsDto> GetComponentUsageAnalyticsAsync(int componentId) => throw new NotImplementedException();
        public Task<List<RevenueTrendDto>> GetRevenueTrendsAsync(DateTime startDate, DateTime endDate, string groupBy) => throw new NotImplementedException();
        public Task<CustomerInsightsDto> GetCustomerInsightsAsync(int customerId) => throw new NotImplementedException();
        public Task<List<TemplatePerformanceDto>> GetTopPerformingTemplatesAsync(DateTime startDate, DateTime endDate, int topCount) => throw new NotImplementedException();
        public Task<List<CustomerInsightsDto>> GetCustomerSegmentationAsync() => throw new NotImplementedException();
    }

    public class OrderTrackingService : IOrderTrackingService
    {
        private readonly FurnitureStoreDbContext _context;
        public OrderTrackingService(FurnitureStoreDbContext context) { _context = context; }

        public Task<OrderTrackingDto> GetOrderTrackingAsync(string orderNumber) => throw new NotImplementedException();
        public Task<List<OrderTrackingDto>> GetCustomerOrderTrackingAsync(int customerId) => throw new NotImplementedException();
        public Task UpdateTrackingInformationAsync(int orderId, string trackingInfo) => throw new NotImplementedException();
    }

    public class CustomizationService : ICustomizationService
    {
        private readonly FurnitureStoreDbContext _context;
        public CustomizationService(FurnitureStoreDbContext context) { _context = context; }

        public Task<FurnitureCustomizationDto> GetCustomizationOptionsAsync(int templateId) => throw new NotImplementedException();
        public Task<ComponentValidationResponse> ValidateCustomizationAsync(CustomizationValidationRequest request) => throw new NotImplementedException();
        public Task<CustomizationPricingDto> CalculateCustomizationPriceAsync(CustomizationPricingRequest request) => throw new NotImplementedException();
        public Task<List<ComponentDto>> GetCompatibleComponentsAsync(int componentId, int templateId) => throw new NotImplementedException();
        public Task<List<ComponentDto>> GetRecommendedComponentsAsync(int templateId, List<int> selectedComponentIds) => throw new NotImplementedException();
    }

    public class NotificationService : INotificationService
    {
        private readonly FurnitureStoreDbContext _context;
        public NotificationService(FurnitureStoreDbContext context) { _context = context; }

        public Task<List<NotificationDto>> GetUserNotificationsAsync(int userId, bool unreadOnly) => throw new NotImplementedException();
        public Task<NotificationDto> CreateNotificationAsync(int userId, NotificationType type, string title, string message, string actionUrl = null, string relatedEntityId = null) => throw new NotImplementedException();
        public Task MarkAsReadAsync(int notificationId) => throw new NotImplementedException();
        public Task MarkAllAsReadAsync(int userId) => throw new NotImplementedException();
        public Task DeleteNotificationAsync(int notificationId) => throw new NotImplementedException();
        public Task SendLowStockAlertsAsync() => throw new NotImplementedException();
        public Task SendOrderStatusNotificationsAsync(int orderId, OrderStatus newStatus) => throw new NotImplementedException();
    }

    public class BulkOperationsService : IBulkOperationsService
    {
        private readonly FurnitureStoreDbContext _context;
        public BulkOperationsService(FurnitureStoreDbContext context) { _context = context; }

        public Task<BulkOperationResultDto> BulkStockUpdateAsync(BulkStockUpdateDto dto) => throw new NotImplementedException();
        public Task<BulkOperationResultDto> BulkPriceUpdateAsync(BulkPriceUpdateDto dto) => throw new NotImplementedException();
        public Task<BulkOperationResultDto> BulkComponentActivationAsync(BulkActivationDto dto) => throw new NotImplementedException();
        public Task<BulkOperationResultDto> BulkDeleteComponentsAsync(List<int> componentIds) => throw new NotImplementedException();
    }

    public class ImportExportService : IImportExportService
    {
        private readonly FurnitureStoreDbContext _context;
        public ImportExportService(FurnitureStoreDbContext context) { _context = context; }

        public Task<ImportResultDto> ImportComponentsAsync(IFormFile file) => throw new NotImplementedException();
        public Task<ImportResultDto> ImportCompatibilityRulesAsync(IFormFile file) => throw new NotImplementedException();
        public Task<byte[]> ExportComponentsAsync(ComponentType? componentType) => throw new NotImplementedException();
        public Task<byte[]> ExportOrdersAsync(DateTime startDate, DateTime endDate) => throw new NotImplementedException();
        public Task<byte[]> ExportInventoryReportAsync() => throw new NotImplementedException();
        public Task<byte[]> ExportSalesReportAsync(DateTime startDate, DateTime endDate) => throw new NotImplementedException();
    }

    public class CacheService : ICacheService
    {
        public Task<T> GetAsync<T>(string key) => throw new NotImplementedException();
        public Task SetAsync<T>(string key, T value, TimeSpan? expiry = null) => throw new NotImplementedException();
        public Task RemoveAsync(string key) => throw new NotImplementedException();
        public Task RemovePatternAsync(string pattern) => throw new NotImplementedException();
        public Task<bool> ExistsAsync(string key) => throw new NotImplementedException();
    }

    public class EmailService : IEmailService
    {
        public Task SendOrderConfirmationEmailAsync(int orderId) => throw new NotImplementedException();
        public Task SendOrderStatusUpdateEmailAsync(int orderId, OrderStatus newStatus) => throw new NotImplementedException();
        public Task SendLowStockAlertEmailAsync(List<StockLevelDto> lowStockItems) => throw new NotImplementedException();
        public Task SendWelcomeEmailAsync(int userId) => throw new NotImplementedException();
        public Task SendPasswordResetEmailAsync(string email, string resetToken) => throw new NotImplementedException();
    }
}

*/