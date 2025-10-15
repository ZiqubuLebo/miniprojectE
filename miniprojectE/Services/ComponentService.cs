using Microsoft.EntityFrameworkCore;
using miniprojectE.Data;
using miniprojectE.DTO.ComponentDTOs;
using miniprojectE.DTO.OrderDTOs;
using miniprojectE.DTO.ResponseDTOs;
using miniprojectE.DTO.StockDTOs;
using miniprojectE.DTO.ValidationDTOs;
using miniprojectE.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace miniprojectE.Services
{
    public class ComponentService: IComponentService
    {
       private readonly AppDB _context;

        public ComponentService(AppDB context)
        {
            _context = context;
        }

        public async Task<ComponentDTO> GetComponentAsync(int componentId)
        {
            var component = await _context.Component
                .FirstOrDefaultAsync(c => c.ComponentID == componentId);

            if (component == null)
                throw new NotFoundException("Component not found");

            return new ComponentDTO
            {
                ComponentId = component.ComponentID,
                ComponentName = component.Name,
                Description = component.Description,
                UnitPrice = component.UnitPrice,
                StockQuantity = component.Level,
                MinimumStockLevel = component.MinimumLevel,
                ComponentType = component.Type,
                //Sku = component.Sku,
                ImageUrl = component.Image,
                IsActive = component.IsActive,
                IsLowStock = component.Level <= component.MinimumLevel
            };
        }

        public async Task<ComponentDTO> CreateComponentAsync(CreateComponentDTO dto, Guid userId)
        {
            var component = new Component
            {
                Name = dto.ComponentName,
                Description = dto.Description,
                UnitPrice = dto.UnitPrice,
                Level = dto.StockQuantity,
                MinimumLevel = dto.MinimumStockLevel,
                Type = dto.ComponentType,
                //Sku = dto.Sku,
                Image = dto.ImageUrl,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Component.Add(component);
            await _context.SaveChangesAsync();

            // Create initial stock movement record
            if (dto.StockQuantity > 0)
            {
                var stockMovement = new Stock
                {
                    ComponentID = component.ComponentID,
                    UserID = userId, 
                    Type = component.Type.ToString(),
                    MovementType = MovementType.Purchase,
                    QuantityChange = dto.StockQuantity,
                    QuantityBefore = 0,
                    QuantityAfter = dto.StockQuantity,
                    Notes = "Initial stock entry",
                    MovementDate = DateTime.UtcNow
                };

                _context.Stocks.Add(stockMovement);
                await _context.SaveChangesAsync();
            }

            return await GetComponentAsync(component.ComponentID);
        }

        public async Task<ComponentDTO> UpdateComponentAsync(int componentId, UpdateComponentDTO dto)
        {
            var component = await _context.Component.FindAsync(componentId);
            if (component == null)
                throw new NotFoundException("Component not found");

            component.Name = dto.Name;
            component.Description = dto.Description;
            component.UnitPrice = dto.UnitPrice;
            component.MinimumLevel = dto.MinimumStockLevel;
            component.Image = dto.Image;
            component.IsActive = dto.IsActive;
            component.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetComponentAsync(componentId);
        }

        public async Task<ComponentDTO> AdjustStockAsync(int componentId, StockAdjustmentDTO dto)
        {
            var component = await _context.Component.FindAsync(componentId);
            if (component == null)
                throw new NotFoundException("Component not found");

            var previousStock = component.Level;
            component.Level += dto.QuantityChange;
            component.UpdatedAt = DateTime.UtcNow;

            // Record stock movement
            var movement = new Stock
            {
                ComponentID = componentId,
                UserID = Guid.Parse("8f1b4c9a-2f44-45d1-bf6f-3c8c4d7b82e1"), 
                //Type = component.Type.ToString(),
                MovementType = dto.MovementType,
                Type = component.Type.ToString(),
                QuantityChange = dto.QuantityChange,
                QuantityBefore = previousStock,
                QuantityAfter = component.Level,
                Notes = dto.Notes,
                MovementDate = DateTime.UtcNow,
                ReferenceId = "Stock update"
            };

            _context.Stocks.Add(movement);
            await _context.SaveChangesAsync();

            return await GetComponentAsync(componentId);
        }

        public async Task<List<ComponentDTO>> GetAllComponentsAsync()
        {
            var components = await _context.Component.Where(c => c.IsActive)
                .Select(c => new ComponentDTO
                {
                    ComponentId = c.ComponentID,
                    ComponentName = c.Name,
                    Description = c.Description,
                    UnitPrice = c.UnitPrice,
                    StockQuantity = c.Level,
                    MinimumStockLevel = c.MinimumLevel,
                    ComponentType = c.Type,
                    //Sku = c.Sku,
                    ImageUrl = c.Image,
                    IsActive = c.IsActive,
                    IsLowStock = c.Level <= c.MinimumLevel
                })
                .ToListAsync();

            return components;
        }

        public async Task<List<ComponentCompatibilityDTO>> GetComponentCompatibilityAsync(int componentId)
        {
            try
            {
                var component = await _context.Component.FindAsync(componentId);
                if (component == null)
                    throw new NotFoundException("Component");

                var compatibilities = await _context.ComponentCompatibility
                    .Include(cc => cc.ComponentA)
                    .Include(cc => cc.ComponentB)
                    .Where(cc => cc.ComponentID1 == componentId || cc.ComponentID2 == componentId)
                    .Select(cc => new ComponentCompatibilityDTO
                    {
                        CompatibilityId = cc.CompatibiltyID,
                        ComponentAId = cc.ComponentID1,
                        ComponentBId = cc.ComponentID2,
                        ComponentAName = cc.ComponentA.Name,
                        ComponentBName = cc.ComponentB.Name,
                        ComponentAType = cc.ComponentA.Type,
                        ComponentBType = cc.ComponentB.Type,
                        IsCompatible = cc.IsCompatible,
                        CompatibilityNotes = cc.notes,
                        // = cc.CreatedAt
                    })
                    .OrderBy(cc => cc.ComponentAName)
                    .ThenBy(cc => cc.ComponentBName)
                    .ToListAsync();

                return compatibilities;
            }
            catch (Exception ex) when (!(ex is NotFoundException))
            {
                throw;
            }
        }

        public async Task<ComponentCompatibilityDTO> CreateCompatibilityAsync(CreateCompatibilityDTO dto)
        {
            try
            {
                // Validate components exist
                var componentA = await _context.Component.FindAsync(dto.ComponentAId);
                var componentB = await _context.Component.FindAsync(dto.ComponentBId);

                if (componentA == null)
                    throw new NotFoundException("Component A");

                if (componentB == null)
                    throw new NotFoundException("Component B");

                // Prevent self-compatibility
                if (dto.ComponentAId == dto.ComponentBId)
                    throw new ValidationException("A component cannot be compatible with itself");

                // Check if compatibility rule already exists (both directions)
                var existingRule = await _context.ComponentCompatibility
                    .FirstOrDefaultAsync(cc =>
                        (cc.ComponentID1 == dto.ComponentAId && cc.ComponentID2 == dto.ComponentBId) ||
                        (cc.ComponentID1 == dto.ComponentBId && cc.ComponentID2 == dto.ComponentAId));


                var compatibility = new ComponentCompatibility
                {
                    ComponentID1 = dto.ComponentAId,
                    ComponentID2 = dto.ComponentBId,
                    IsCompatible = dto.IsCompatible,
                    notes = dto.CompatibilityNotes,
                    CreatedAt = DateTime.UtcNow
                };

                _context.ComponentCompatibility.Add(compatibility);
                await _context.SaveChangesAsync();

                return new ComponentCompatibilityDTO
                {
                    CompatibilityId = compatibility.CompatibiltyID,
                    ComponentAId = compatibility.ComponentID1,
                    ComponentBId = compatibility.ComponentID2,
                    ComponentAName = componentA.Name,
                    ComponentBName = componentB.Name,
                    ComponentAType = componentA.Type,
                    ComponentBType = componentB.Type,
                    IsCompatible = compatibility.IsCompatible,
                    CompatibilityNotes = compatibility.notes,
                    //CreatedAt = compatibility.CreatedAt
                };
            }
            catch (Exception ex) when (!(ex is NotFoundException) && !(ex is ValidationException))
            {
                throw;
            }
        }
    }
}
