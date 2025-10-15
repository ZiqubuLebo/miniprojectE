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
        private readonly IComponentService _componentService;

        public TemplateService(AppDB context, IComponentService componentService)
        {
            _context = context;
            _componentService = componentService;
        }

        // Implement all ITemplateService methods
        public async Task<List<FurnitureDTO>> GetTemplatesAsync(FurnitureType? furnitureType)
        {
            try
            {
                //in furniture check for template components and components from template component
                var query = _context.Furniture
                    .Include(t => t.TemplateComponents)
                        .ThenInclude(tc => tc.Component)
                    .Where(t => t.IsActive);

                //from the furniture returned, filter out the furnituretype required
                if (furnitureType.HasValue)
                    query = query.Where(t => t.FurnitureType == furnitureType.Value);

                //create new furniture from query to return
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

                //return the furniture 
                return templates;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<FurnitureDTO> GetTemplateAsync(int templateId)
        {
            try
            {
                //in furniture check for template components and components from template component
                var template = await _context.Furniture
                    .Include(t => t.TemplateComponents)
                        .ThenInclude(tc => tc.Component)
                    .FirstOrDefaultAsync(t => t.FurnitureID == templateId);

                if (template == null)
                    throw new NotFoundException("Template");

                //return furniture template with the provided id
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
                throw;
            }
        }

        public async Task<FurnitureDTO> CreateTemplateAsync(CreateTemplateDTO dto)
        {
            try
            {
                // Validate template name uniqueness
                // to do


                //create new furniture template
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

                //return new furniture template
                return await GetTemplateAsync(template.FurnitureID);
            }
            catch (Exception ex)
            {
               throw;
            }
        }

        public async Task<FurnitureDTO> UpdateTemplateAsync(int templateId, CreateTemplateDTO dto)
        {
            try
            {
                //check if template exists
                var template = await _context.Furniture.FindAsync(templateId);
                if (template == null)
                    throw new NotFoundException("Template");

                // Check if name is being changed and if it conflicts
                //to do

                //update template information
                template.Name = dto.Name;
                template.Description = dto.Description;
                template.FurnitureType = dto.FurnitureType;
                template.BasePrice = dto.BasePrice;

                await _context.SaveChangesAsync();

                //return updated template
                return await GetTemplateAsync(templateId);
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
               throw;
            }
        }

        public async Task<TemplateComponentDTO> AddTemplateComponentAsync(int templateId, CreateTemplateComponentDTO dto)
        {
            try
            {
                // check if template exists
                var template = await _context.Furniture.FindAsync(templateId);
                if (template == null)
                    throw new NotFoundException("Template");

                // check component exists
                var component = await _context.Component.FindAsync(dto.ComponentID);
                if (component == null)
                    throw new NotFoundException("Component");

                if (!component.IsActive)
                    throw new ValidationException("Cannot add inactive component to template");

                // Check if component is already in template
                var existingTemplateComponent = await _context.TemplateComponent
                    .FirstOrDefaultAsync(tc => tc.TemplateID == templateId && tc.ComponentID == dto.ComponentID);

                // check quantity constraints
                if (dto.MinQuantity < 1)
                    throw new ValidationException("Minimum quantity must be at least 1");

                if (dto.MaxQuantity < dto.MinQuantity)
                    throw new ValidationException("Maximum quantity cannot be less than minimum quantity");

                //add new template
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

                //return new furniture template
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
               throw;
            }
        }

        public async Task RemoveTemplateComponentAsync(int templateId, int componentId)
        {
            try
            {

                //check if template exists
                var templateComponent = await _context.TemplateComponent
                    .Include(tc => tc.Component)
                    .Include(tc => tc.Template)
                    .FirstOrDefaultAsync(tc => tc.TemplateID == templateId && tc.ComponentID == componentId);

                if (templateComponent == null)
                    throw new NotFoundException("Template Component, Component not found in template");

                // Check if template component is used in any existing orders
                var isUsedInOrders = await _context.ItemComponent
                    .Include(ic => ic.Item)
                    .AnyAsync(ic => ic.ComponentID == componentId && ic.Item.TemplateID == templateId);

                _context.TemplateComponent.Remove(templateComponent);
                await _context.SaveChangesAsync();

               }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
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
                //to do

                // Remove all template components first
                _context.TemplateComponent.RemoveRange(template.TemplateComponents);

                // Remove the template
                _context.Furniture.Remove(template);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                if (!(ex is NotFoundException))
                {
                   
                }
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
               throw;
            }
        }

        public async Task<List<FurnitureDTO>> GetAllTemplatesAsync()
        {
            try
            {
                //return all furniture components
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
                throw;
            }
        }
    }
}
