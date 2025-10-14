using Microsoft.EntityFrameworkCore;
using miniprojectE.Data;
using miniprojectE.DTO.ComponentDTOs;
using miniprojectE.DTO.OrderDTOs;
using miniprojectE.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.Services
{
    public class TemplateService: ITemplateService
    {
        private readonly AppDB _context;
        private readonly ILogger<TemplateService> _logger;
        private readonly IComponentService _componentService;

        public TemplateService(AppDB context, ILogger<TemplateService> logger, IComponentService componentService)
        {
            _context = context;
            _logger = logger;
            _componentService = componentService;
        }

        // Implement all ITemplateService methods
        public async Task<List<FurnitureDTO>> GetTemplatesAsync(FurnitureType? furnitureType)
        {
            try
            {
                var query = _context.Furniture
                    .Include(t => t.TemplateComponents)
                        .ThenInclude(tc => tc.Component)
                    .Where(t => t.IsActive);

                if (furnitureType.HasValue)
                    query = query.Where(t => t.FurnitureType == furnitureType.Value);

                var templates = await query
                    .OrderBy(t => t.Name)
                    .Select(t => new FurnitureDTO
                    {
                        TemplateID = t.FurnitureID,
                        Name = t.Name,
                        Description = t.Description,
                        FurnitureType = t.FurnitureType,
                        Price = t.BasePrice,
                        //IsActive = t.IsActive,
                        TemplateComponents = t.TemplateComponents.Select(tc => new TemplateComponentDTO
                        {
                            TemplateID = tc.TemplateID,
                            ComponentID = tc.ComponentID,
                            Name = tc.Component.Name,
                            Type = tc.Component.Type,
                            isRequired = tc.isRequired,
                            minLevel = tc.minLevel,
                            maxLevel = tc.maxLevel,
                            ComponentRole = tc.ComponentRole,
                            UnitPrice = tc.Component.UnitPrice
                        }).OrderBy(tc => tc.ComponentRole).ThenBy(tc => tc.Name).ToList()
                    })
                    .ToListAsync();

                return templates;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting templates with furniture type filter: {FurnitureType}", furnitureType);
                throw;
            }
        }

        public async Task<FurnitureDTO> GetTemplateAsync(int templateId)
        {
            try
            {
                var template = await _context.Furniture
                    .Include(t => t.TemplateComponents)
                        .ThenInclude(tc => tc.Component)
                    .FirstOrDefaultAsync(t => t.FurnitureID == templateId);

                if (template == null)
                    throw new NotFoundException("Template");

                return new FurnitureDTO
                {
                    TemplateID = template.FurnitureID,
                    Name = template.Name,
                    Description = template.Description,
                    FurnitureType = template.FurnitureType,
                    Price = template.BasePrice,
                    //IsActive = template.IsActive,
                    TemplateComponents = template.TemplateComponents.Select(tc => new TemplateComponentDTO
                    {
                        TemplateID = tc.TemplateID,
                        ComponentID = tc.ComponentID,
                        Name = tc.Component.Name,
                        Type = tc.Component.Type,
                        isRequired = tc.isRequired,
                        minLevel = tc.minLevel,
                        maxLevel = tc.maxLevel,
                        ComponentRole = tc.ComponentRole,
                        UnitPrice = tc.Component.UnitPrice
                    }).OrderBy(tc => tc.ComponentRole).ThenBy(tc => tc.Name).ToList()
                };
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                _logger.LogError(ex, "Error getting template {TemplateId}", templateId);
                throw;
            }
        }

        public async Task<FurnitureDTO> CreateTemplateAsync(CreateTemplateDTO dto)
        {
            try
            {
                // Validate template name uniqueness
                if (await _context.Furniture.AnyAsync(t => t.Name == dto.Name))
                {
                    //throw new DuplicateResourceException("Template", dto.Name);
                }

                var template = new Furniture
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    FurnitureType = dto.FurnitureType,
                    BasePrice = dto.BasePrice,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Furniture.Add(template);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Template created successfully: {TemplateName} (ID: {TemplateId})",
                    dto.Name, template.FurnitureID);

                return await GetTemplateAsync(template.FurnitureID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating template: {TemplateName}", dto.Name);
                throw;
            }
        }

        public async Task<FurnitureDTO> UpdateTemplateAsync(int templateId, CreateTemplateDTO dto)
        {
            try
            {
                var template = await _context.Furniture.FindAsync(templateId);
                if (template == null)
                    throw new NotFoundException("Template");

                // Check if name is being changed and if it conflicts
                if (template.Name != dto.Name)
                {
                    var nameExists = await _context.Furniture
                        .AnyAsync(t => t.Name == dto.Name && t.FurnitureID != templateId);

                    //if (nameExists)
                        //throw new DuplicateResourceException("Template", dto.Name);
                }

                template.Name = dto.Name;
                template.Description = dto.Description;
                template.FurnitureType = dto.FurnitureType;
                template.BasePrice = dto.BasePrice;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Template updated successfully: {TemplateId} - {TemplateName}",
                    templateId, dto.Name);

                return await GetTemplateAsync(templateId);
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                _logger.LogError(ex, "Error updating template {TemplateId}", templateId);
                throw;
            }
        }

        public async Task<TemplateComponentDTO> AddTemplateComponentAsync(int templateId, CreateTemplateComponentDTO dto)
        {
            try
            {
                // Validate template exists
                var template = await _context.Furniture.FindAsync(templateId);
                if (template == null)
                    throw new NotFoundException("Template");

                // Validate component exists and is active
                var component = await _context.Component.FindAsync(dto.ComponentID);
                if (component == null)
                    throw new NotFoundException("Component");

                if (!component.IsActive)
                    throw new ValidationException("Cannot add inactive component to template");

                // Check if component is already in template
                var existingTemplateComponent = await _context.TemplateComponent
                    .FirstOrDefaultAsync(tc => tc.TemplateID == templateId && tc.ComponentID == dto.ComponentID);

               // if (existingTemplateComponent != null)
                 //   throw new DuplicateResourceException("Template Component",
                   //     $"Component '{component.ComponentName}' is already in template '{template.Name}'");

                // Validate quantity constraints
                if (dto.MinQuantity < 1)
                    throw new ValidationException("Minimum quantity must be at least 1");

                if (dto.MaxQuantity < dto.MinQuantity)
                    throw new ValidationException("Maximum quantity cannot be less than minimum quantity");

                var templateComponent = new TemplateComponent
                {
                    FurnitureID = templateId,
                    ComponentID = dto.ComponentID,
                    isRequired = dto.isRequired,
                    minLevel = dto.MinQuantity,
                    maxLevel = dto.MaxQuantity,
                    ComponentRole = dto.ComponentRole
                };

                _context.TemplateComponent.Add(templateComponent);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Component added to template: Template {TemplateId}, Component {ComponentId} ({ComponentName})",
                    templateId, dto.ComponentID, component.Name);

                return new TemplateComponentDTO
                {
                    TemplateID = templateComponent.TemplateID,
                    ComponentID = templateComponent.ComponentID,
                    Name = component.Name,
                    Type = component.Type,
                    isRequired = templateComponent.isRequired,
                    minLevel = templateComponent.minLevel,
                    maxLevel = templateComponent.maxLevel,
                    ComponentRole = templateComponent.ComponentRole,
                    UnitPrice = component.UnitPrice
                };
            }
            catch (Exception ex) when (!(ex is NotFoundException) && !(ex is ValidationException))
            {
                _logger.LogError(ex, "Error adding component {ComponentId} to template {TemplateId}", dto.ComponentID, templateId);
                throw;
            }
        }

        public async Task RemoveTemplateComponentAsync(int templateId, int componentId)
        {
            try
            {
                var templateComponent = await _context.TemplateComponent
                    .Include(tc => tc.Component)
                    .Include(tc => tc.Template)
                    .FirstOrDefaultAsync(tc => tc.TemplateID == templateId && tc.ComponentID == componentId);

                if (templateComponent == null)
                    throw new NotFoundException("Template Component, Component {componentId} not found in template {templateId}");

                // Check if template component is used in any existing orders
                var isUsedInOrders = await _context.ItemComponent
                    .Include(ic => ic.Item)
                    .AnyAsync(ic => ic.ComponentID == componentId && ic.Item.TemplateID == templateId);

                if (isUsedInOrders)
                {
                    //throw new BusinessRuleException(
                      //  $"Cannot remove component '{templateComponent.Component.ComponentName}' from template '{templateComponent.Template.Name}' " +
                        //"as it is used in existing orders");
                }

                _context.TemplateComponent.Remove(templateComponent);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Component removed from template: Template {TemplateId}, Component {ComponentId} ({ComponentName})",
                    templateId, componentId, templateComponent.Component.Name);
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                _logger.LogError(ex, "Error removing component {ComponentId} from template {TemplateId}", componentId, templateId);
                throw;
            }
        }

        public async Task<bool> DeleteTemplateAsync(int templateId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var template = await _context.Furniture
                    .Include(t => t.TemplateComponents)
                    .FirstOrDefaultAsync(t => t.FurnitureID == templateId);

                if (template == null)
                    throw new NotFoundException("Template");

                // Check if template is used in any orders
                //var isUsedInOrders = await _context.CustomFurnitureItem
                   //''' .AnyAsync(cfi => cfi.TemplateId == templateId);

                /*if (isUsedInOrders)
                {
                    //throw new BusinessRuleException($"Cannot delete template '{template.Name}' as it is used in existing orders. Consider deactivating it instead.");
                }*/

                // Remove all template components first
                _context.TemplateComponent.RemoveRange(template.TemplateComponents);

                // Remove the template
                _context.Furniture.Remove(template);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation("Template deleted successfully: {TemplateId} - {TemplateName}", templateId, template.Name);

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                if (!(ex is NotFoundException))
                {
                    _logger.LogError(ex, "Error deleting template {TemplateId}", templateId);
                }
                throw;
            }
        }

        public async Task<FurnitureDTO> DeactivateTemplateAsync(int templateId)
        {
            try
            {
                var template = await _context.Furniture.FindAsync(templateId);
                if (template == null)
                    throw new NotFoundException("Template");

                template.IsActive = false;
                await _context.SaveChangesAsync();

                _logger.LogInformation("Template deactivated: {TemplateId} - {TemplateName}", templateId, template.Name);

                return await GetTemplateAsync(templateId);
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                _logger.LogError(ex, "Error deactivating template {TemplateId}", templateId);
                throw;
            }
        }

        public async Task<FurnitureDTO> ReactivateTemplateAsync(int templateId)
        {
            try
            {
                var template = await _context.Furniture.FindAsync(templateId);
                if (template == null)
                    throw new NotFoundException("Template");

                template.IsActive = true;
                await _context.SaveChangesAsync();

                _logger.LogInformation("Template reactivated: {TemplateId} - {TemplateName}", templateId, template.Name);

                return await GetTemplateAsync(templateId);
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                _logger.LogError(ex, "Error reactivating template {TemplateId}", templateId);
                throw;
            }
        }

        public async Task<TemplateValidationResult> ValidateTemplateAsync(int templateId)
        {
            try
            {
                var template = await _context.Furniture
                    .Include(t => t.TemplateComponents)
                        .ThenInclude(tc => tc.Component)
                    .FirstOrDefaultAsync(t => t.FurnitureID == templateId);

                if (template == null)
                    throw new NotFoundException("Template");

                var result = new TemplateValidationResult
                {
                    TemplateId = templateId,
                    TemplateName = template.Name,
                    IsValid = true,
                    Issues = new List<string>(),
                    RequiredComponents = new List<TemplateComponentDTO>(),
                    OptionalComponents = new List<TemplateComponentDTO>(),
                    EstimatedMinPrice = template.BasePrice,
                    EstimatedMaxPrice = template.BasePrice
                };

                var hasRequiredComponents = false;

                foreach (var tc in template.TemplateComponents)
                {
                    var componentDto = new TemplateComponentDTO
                    {
                        TemplateID = tc.TemplateID,
                        ComponentID = tc.ComponentID,
                        Name = tc.Component.Name,
                        Type = tc.Component.Type,
                        isRequired = tc.isRequired,
                        minLevel = tc.minLevel,
                        maxLevel = tc.maxLevel,
                        ComponentRole = tc.ComponentRole,
                        UnitPrice = tc.Component.UnitPrice
                    };

                    if (tc.isRequired)
                    {
                        hasRequiredComponents = true;
                        result.RequiredComponents.Add(componentDto);
                        result.EstimatedMinPrice += tc.Component.UnitPrice * tc.minLevel;
                        result.EstimatedMaxPrice += tc.Component.UnitPrice * tc.maxLevel;
                    }
                    else
                    {
                        result.OptionalComponents.Add(componentDto);
                        result.EstimatedMaxPrice += tc.Component.UnitPrice * tc.maxLevel;
                    }

                    // Check component availability
                    if (!tc.Component.IsActive)
                    {
                        result.IsValid = false;
                        result.Issues.Add($"Component '{tc.Component.Name}' is inactive");
                    }

                    if (tc.Component.Level < tc.maxLevel)
                    {
                        result.Issues.Add($"Component '{tc.Component.Name}' has insufficient stock. Required: {tc.minLevel}, Available: {tc.Component.Level}");
                    }
                }

                if (!hasRequiredComponents)
                {
                    result.Issues.Add("Template has no required components");
                }

                if (template.TemplateComponents.Count == 0)
                {
                    result.IsValid = false;
                    result.Issues.Add("Template has no components defined");
                }

                // Check for component compatibility if there are multiple components
                if (template.TemplateComponents.Count > 1)
                {
                    var compatibilityIssues = await CheckTemplateComponentCompatibilityAsync(templateId);
                    if (compatibilityIssues.Any())
                    {
                        result.Issues.AddRange(compatibilityIssues);
                    }
                }

                return result;
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                _logger.LogError(ex, "Error validating template {TemplateId}", templateId);
                throw;
            }
        }

        public async Task<List<FurnitureDTO>> GetTemplatesByFurnitureTypeAsync(FurnitureType furnitureType, bool activeOnly = true)
        {
            try
            {
                var query = _context.Furniture
                    .Include(t => t.TemplateComponents)
                        .ThenInclude(tc => tc.Component)
                    .Where(t => t.FurnitureType == furnitureType);

                if (activeOnly)
                    query = query.Where(t => t.IsActive);

                var templates = await query
                    .OrderBy(t => t.Name)
                    .Select(t => new FurnitureDTO
                    {
                        TemplateID = t.FurnitureID,
                        Name = t.Name,
                        Description = t.Description,
                        FurnitureType = t.FurnitureType,
                        Price = t.BasePrice,
                       // IsActive = t.IsActive,
                        TemplateComponents = t.TemplateComponents.Select(tc => new TemplateComponentDTO
                        {
                            TemplateID = tc.TemplateID,
                            ComponentID = tc.ComponentID,
                            Name = tc.Component.Name,
                            Type = tc.Component.Type,
                            isRequired = tc.isRequired,
                            minLevel = tc.minLevel,
                            maxLevel = tc.maxLevel,
                            ComponentRole = tc.ComponentRole,
                            UnitPrice = tc.Component.UnitPrice
                        }).OrderBy(tc => tc.ComponentRole).ThenBy(tc => tc.Name).ToList()
                    })
                    .ToListAsync();

                return templates;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting templates by furniture type: {FurnitureType}", furnitureType);
                throw;
            }
        }

        public async Task<TemplateComponentDTO> UpdateTemplateComponentAsync(int templateId, int componentId, UpdateTemplateComponentDTO dto)
        {
            try
            {
                var templateComponent = await _context.TemplateComponent
                    .Include(tc => tc.Component)
                    .FirstOrDefaultAsync(tc => tc.TemplateID == templateId && tc.ComponentID == componentId);

                if (templateComponent == null)
                    throw new NotFoundException("Template Component, Component {componentId} in template {templateId}");

                // Validate quantity constraints
                if (dto.MinQuantity < 1)
                    throw new ValidationException("Minimum quantity must be at least 1");

                if (dto.MaxQuantity < dto.MinQuantity)
                    throw new ValidationException("Maximum quantity cannot be less than minimum quantity");

                templateComponent.isRequired = dto.IsRequired;
                templateComponent.minLevel = dto.MinQuantity;
                templateComponent.maxLevel = dto.MaxQuantity;
                templateComponent.ComponentRole = dto.ComponentRole;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Template component updated: Template {TemplateId}, Component {ComponentId}", templateId, componentId);

                return new TemplateComponentDTO
                {
                    TemplateID = templateComponent.TemplateID,
                    ComponentID = templateComponent.ComponentID,
                    Name = templateComponent.Component.Name,
                    Type = templateComponent.Component.Type,
                    isRequired = templateComponent.isRequired,
                    minLevel = templateComponent.minLevel,
                    maxLevel = templateComponent.maxLevel,
                    ComponentRole = templateComponent.ComponentRole,
                    UnitPrice = templateComponent.Component.UnitPrice
                };
            }
            catch (Exception ex) when (!(ex is NotFoundException) && !(ex is ValidationException))
            {
                _logger.LogError(ex, "Error updating template component: Template {TemplateId}, Component {ComponentId}", templateId, componentId);
                throw;
            }
        }

        public async Task<List<ComponentDTO>> GetAvailableComponentsForTemplateAsync(int templateId, ComponentType? componentType = null)
        {
            try
            {
                // Verify template exists
                var template = await _context.Furniture.FindAsync(templateId);
                if (template == null)
                    throw new NotFoundException("Template");

                // Get components already in the template
                var existingComponentIds = await _context.TemplateComponent
                    .Where(tc => tc.TemplateID == templateId)
                    .Select(tc => tc.ComponentID)
                    .ToListAsync();

                // Get available components not already in template
                var query = _context.Component
                    .Where(c => c.IsActive && !existingComponentIds.Contains(c.ComponentID));

                if (componentType.HasValue)
                    query = query.Where(c => c.Type == componentType.Value);

                var availableComponents = await query
                    .OrderBy(c => c.Type)
                    .ThenBy(c => c.Name)
                    .Select(c => new ComponentDTO
                    {
                        ComponentId = c.ComponentID,
                        ComponentName = c.Name,
                        Description = c.Description,
                        UnitPrice = c.UnitPrice,
                        StockQuantity = c.Level,
                        MinimumStockLevel = c.MinimumLevel,
                        ComponentType = c.Type,
                        ImageUrl = c.Image,
                        IsActive = c.IsActive,
                        IsLowStock = c.Level <= c.MinimumLevel
                    })
                    .ToListAsync();

                return availableComponents;
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                _logger.LogError(ex, "Error getting available components for template {TemplateId}", templateId);
                throw;
            }
        }

        public async Task<TemplatePricingDTO> CalculateTemplatePricingAsync(int templateId, List<CreateItemComponentDTO> selectedComponents)
        {
            try
            {
                var template = await _context.Furniture
                    .Include(t => t.TemplateComponents)
                        .ThenInclude(tc => tc.Component)
                    .FirstOrDefaultAsync(t => t.FurnitureID == templateId);

                if (template == null)
                    throw new NotFoundException("Template");

                var pricing = new TemplatePricingDTO
                {
                    TemplateId = templateId,
                    TemplateName = template.Name,
                    BasePrice = template.BasePrice,
                    ComponentPricing = new List<ComponentPricingDTO>(),
                    TotalPrice = template.BasePrice
                };

                foreach (var selectedComponent in selectedComponents)
                {
                    var component = await _context.Component.FindAsync(selectedComponent.ComponentId);
                    if (component == null)
                        throw new NotFoundException("Component");

                    var componentPricing = new ComponentPricingDTO
                    {
                        ComponentId = selectedComponent.ComponentId,
                        ComponentName = component.Name,
                        Quantity = selectedComponent.QuantityUsed,
                        UnitPrice = component.UnitPrice,
                        LineTotal = component.UnitPrice * selectedComponent.QuantityUsed
                    };

                    pricing.ComponentPricing.Add(componentPricing);
                    pricing.TotalPrice += componentPricing.LineTotal;
                }

                return pricing;
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                _logger.LogError(ex, "Error calculating template pricing for template {TemplateId}", templateId);
                throw;
            }
        }

        // Helper method for compatibility checking
        private async Task<List<string>> CheckTemplateComponentCompatibilityAsync(int templateId)
        {
            var issues = new List<string>();

            var componentIds = await _context.TemplateComponent
                .Where(tc => tc.TemplateID == templateId)
                .Select(tc => tc.ComponentID)
                .ToListAsync();

            for (int i = 0; i < componentIds.Count; i++)
            {
                for (int j = i + 1; j < componentIds.Count; j++)
                {
                    var componentA = componentIds[i];
                    var componentB = componentIds[j];

                    var compatibility = await _context.ComponentCompatibility
                        .Include(cc => cc.ComponentA)
                        .Include(cc => cc.ComponentB)
                        .FirstOrDefaultAsync(cc =>
                            (cc.ComponentID1 == componentA && cc.ComponentID2 == componentB) ||
                            (cc.ComponentID1 == componentB && cc.ComponentID2 == componentA));

                    if (compatibility != null && !compatibility.IsCompatible)
                    {
                        issues.Add($"Components '{compatibility.ComponentA.Name}' and '{compatibility.ComponentB.Name}' are not compatible in this template");
                    }
                }
            }

            return issues;
        }

        public async Task<List<FurnitureDTO>> GetAllTemplatesAsync()
        {
            try
            {
                var query = _context.Furniture
                    .Include(t => t.TemplateComponents)
                        .ThenInclude(tc => tc.Component)
                    .Where(t => t.IsActive);

                var templates = await query
                    .OrderBy(t => t.Name)
                    .Select(t => new FurnitureDTO
                    {
                        TemplateID = t.FurnitureID,
                        Name = t.Name,
                        Description = t.Description,
                        FurnitureType = t.FurnitureType,
                        Price = t.BasePrice,
                        //IsActive = t.IsActive,
                        TemplateComponents = t.TemplateComponents.Select(tc => new TemplateComponentDTO
                        {
                            TemplateID = tc.TemplateID,
                            ComponentID = tc.ComponentID,
                            Name = tc.Component.Name,
                            Type = tc.Component.Type,
                            isRequired = tc.isRequired,
                            minLevel = tc.minLevel,
                            maxLevel = tc.maxLevel,
                            ComponentRole = tc.ComponentRole,
                            UnitPrice = tc.Component.UnitPrice
                        }).OrderBy(tc => tc.ComponentRole).ThenBy(tc => tc.Name).ToList()
                    })
                    .ToListAsync();

                return templates;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting templates");
                throw;
            }
        }
    }
}
