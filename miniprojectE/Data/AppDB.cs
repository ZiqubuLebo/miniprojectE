using Microsoft.EntityFrameworkCore;
using miniprojectE.Models.Entities;

namespace miniprojectE.Data
{
    public class AppDB : DbContext
    {
        public AppDB(DbContextOptions<AppDB> options) : base(options)
        {

        }

        public DbSet<Users> User { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<ComponentCompatibility> ComponentCompatibility { get; set; }
        public DbSet<Component> Component { get; set; }
        public DbSet<ComponentPopularity> ComponentPopularity { get; set; }
        public DbSet<Orders> Order { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Furniture> Furniture { get; set; }
        public DbSet<ItemComponent> ItemComponent { get; set; }
        public DbSet<OrderHistoryLog> OrderHistoryLog { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<OrderProgress> OrderProgress { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItem { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<TemplateComponent> TemplateComponent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // configure self-referencing many-to-many
            
            modelBuilder.Entity<OrderHistoryLog>()
    .HasOne(h => h.Order)
    .WithMany(o => o.OrderHistoryLogs)
    .HasForeignKey(h => h.OrderID)
    .OnDelete(DeleteBehavior.Restrict); //  or DeleteBehavior.NoAction

            /*modelBuilder.Entity<OrderProgress>()
        .HasOne(p => p.Order)
        .WithMany(o => o.OrderID)
        .HasForeignKey(p => p.OrderId)
        .OnDelete(DeleteBehavior.Restrict); // or .NoAction*/

            /**modelBuilder.Entity<OrderProgress>()
    .HasOne(p => p.User)
    .WithMany()
    .HasForeignKey(p => p.UserID)
    .OnDelete(DeleteBehavior.Restrict); // 👈 important to avoid cascade loops*/

            modelBuilder.Entity<Users>()
                .HasIndex(u => u.UserEmail)
                .IsUnique();

            modelBuilder.Entity<Orders>()
                .HasIndex(o => o.OrderNumber)
                .IsUnique();

            // Configure decimal precision
            modelBuilder.Entity<Component>()
                .Property(c => c.UnitPrice)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Orders>()
                .Property(o => o.Subtotal)
                .HasColumnType("decimal(12,2)");

            modelBuilder.Entity<Orders>()
                .Property(o => o.ShippingCost)
                .HasColumnType("decimal(12,2)");

            modelBuilder.Entity<Orders>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(12,2)");

            // Configure foreign key relationships
            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Customer)
                .WithMany(u => u.CustomerOrders)
                .HasForeignKey(o => o.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Clerk)
                .WithMany(u => u.AssignedOrders)
                .HasForeignKey(o => o.ClerkID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComponentCompatibility>()
                .HasOne(cc => cc.ComponentA)
                .WithMany(c => c.CompatibilitiesAsA)
                .HasForeignKey(cc => cc.ComponentID1)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ComponentCompatibility>()
                .HasOne(cc => cc.ComponentB)
                .WithMany(c => c.CompatibilitiesAsB)
                .HasForeignKey(cc => cc.ComponentID2)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TemplateComponent>(entity =>
            {
                entity.HasKey(tc => tc.TemplateID);

                // Ensure auto-increment
                entity.Property(tc => tc.TemplateID)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn();

                // Configure relationships
                entity.HasOne(tc => tc.Template)
                    .WithMany(t => t.TemplateComponents)
                    .HasForeignKey(tc => tc.FurnitureID)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(tc => tc.Component)
                    .WithMany(c => c.TemplateComponents)
                    .HasForeignKey(tc => tc.ComponentID)
                    .OnDelete(DeleteBehavior.Restrict);

                // Unique constraint: Component can only be added once per template
                entity.HasIndex(tc => new { tc.FurnitureID, tc.ComponentID })
                    .IsUnique();
            });

            /** modelBuilder.Entity<Users>(entity =>
             {
                 entity.Property(u => u.RegistrationDate)
                       .HasDefaultValueSql("GETUTCDATE()");

                 entity.Property(u => u.UpdatedAt)
                       .HasDefaultValueSql("GETUTCDATE()");
             });

             // Seed data
             //SeedData(modelBuilder);*/
        }

      /**  private void SeedData(ModelBuilder modelBuilder)
        {
                       // Seed components
            modelBuilder.Entity<Component>().HasData(
                new Component
                {
                    ComponentID = 1,
                    Name = "Oak Table Top",
                    Description = "Premium oak wood table top, 60x30 inches",
                    UnitPrice = 299.99m,
                    Level = 15,
                    MinimumLevel = 5,
                    Type = ComponentType.Tabletop,
                    IsActive = true,
                },
                new Component
                {
                    ComponentID = 2,
                    Name = "Steel Legs (Set of 4)",
                    Description = "Heavy duty steel table legs, adjustable height",
                    UnitPrice = 89.99m,
                    Level = 8,
                    MinimumLevel = 10,
                    Type = ComponentType.Leg,
                    IsActive = true,
                },
                new Component
                {
                    ComponentID = 3,
                    Name = "Pine Drawer",
                    Description = "Solid pine drawer with soft-close mechanism",
                    UnitPrice = 45.99m,
                    Level = 25,
                    MinimumLevel = 10,
                    Type = ComponentType.Drawer,
                    IsActive = true,
                },
                new Component
                {
                    ComponentID = 4,
                    Name = "Brass Handles",
                    Description = "Antique brass cabinet handles, set of 2",
                    UnitPrice = 12.99m,
                    Level = 50,
                    MinimumLevel = 20,
                    Type = ComponentType.Handle,
                    IsActive = true,
                }
            );

            // Seed furniture templates
            modelBuilder.Entity<Furniture>().HasData(
                new Furniture
                {
                    FurnitureID = 1,
                    Name = "Modern Dining Table",
                    Description = "Contemporary dining table design",
                    FurnitureType = FurnitureType.Table,
                    BasePrice = 199.99m,
                    IsActive = true,
                },
                new Furniture
                {
                    FurnitureID = 2,
                    Name = "Office Desk",
                    Description = "Professional office desk with storage",
                    FurnitureType = FurnitureType.Desk,
                    BasePrice = 249.99m,
                    IsActive = true,
                }
            );

            // Seed component compatibility rules
            modelBuilder.Entity<ComponentCompatibility>().HasData(
                new ComponentCompatibility
                {
                    CompatibiltyID = 1,
                    ComponentID1 = 1, // Oak Table Top
                    ComponentID2 = 2, // Steel Legs
                    IsCompatible = true,
                    notes = "Oak top works well with steel legs",
                },
                new ComponentCompatibility
                {
                    CompatibiltyID = 2,
                    ComponentID1 = 3, // Pine Drawer
                    ComponentID2 = 4, // Brass Handles
                    IsCompatible = true,
                    notes = "Pine drawers compatible with brass handles",
                }
            );
        }*/
    }
}

