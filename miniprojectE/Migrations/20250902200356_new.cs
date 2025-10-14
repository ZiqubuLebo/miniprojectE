using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace miniprojectE.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Component",
                columns: table => new
                {
                    ComponentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    MinimumLevel = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastIpdate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Component", x => x.ComponentID);
                });

            migrationBuilder.CreateTable(
                name: "Furniture",
                columns: table => new
                {
                    FurnitureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FurnitureType = table.Column<int>(type: "int", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furniture", x => x.FurnitureID);
                });

            migrationBuilder.CreateTable(
                name: "OrderProgress",
                columns: table => new
                {
                    OrderProgressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProgress", x => x.OrderProgressID);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "ComponentCompatibility",
                columns: table => new
                {
                    CompatibiltyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponentID1 = table.Column<int>(type: "int", nullable: false),
                    ComponentID2 = table.Column<int>(type: "int", nullable: false),
                    IsCompatible = table.Column<bool>(type: "bit", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentCompatibility", x => x.CompatibiltyID);
                    table.ForeignKey(
                        name: "FK_ComponentCompatibility_Component_ComponentID1",
                        column: x => x.ComponentID1,
                        principalTable: "Component",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComponentCompatibility_Component_ComponentID2",
                        column: x => x.ComponentID2,
                        principalTable: "Component",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComponentPopularity",
                columns: table => new
                {
                    PopularityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponentID = table.Column<int>(type: "int", nullable: false),
                    PeriodYear = table.Column<int>(type: "int", nullable: false),
                    PeriodMonth = table.Column<int>(type: "int", nullable: false),
                    TimesOrdered = table.Column<int>(type: "int", nullable: true),
                    TotalQuantitySold = table.Column<int>(type: "int", nullable: true),
                    TotalRevenue = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    CalculatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentPopularity", x => x.PopularityID);
                    table.ForeignKey(
                        name: "FK_ComponentPopularity_Component_ComponentID",
                        column: x => x.ComponentID,
                        principalTable: "Component",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemplateComponent",
                columns: table => new
                {
                    TemplateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FurnitureID = table.Column<int>(type: "int", nullable: false),
                    ComponentID = table.Column<int>(type: "int", nullable: false),
                    isRequired = table.Column<bool>(type: "bit", nullable: false),
                    minLevel = table.Column<int>(type: "int", nullable: false),
                    maxLevel = table.Column<int>(type: "int", nullable: false),
                    ComponentRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateComponent", x => x.TemplateID);
                    table.ForeignKey(
                        name: "FK_TemplateComponent_Component_ComponentID",
                        column: x => x.ComponentID,
                        principalTable: "Component",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateComponent_Furniture_FurnitureID",
                        column: x => x.FurnitureID,
                        principalTable: "Furniture",
                        principalColumn: "FurnitureID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK_Address_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrder",
                columns: table => new
                {
                    PurchaseOrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrder", x => x.PurchaseOrderID);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_User_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stocks",
                columns: table => new
                {
                    StockID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponentID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovementType = table.Column<int>(type: "int", nullable: false),
                    QuantityChange = table.Column<int>(type: "int", nullable: false),
                    QuantityBefore = table.Column<int>(type: "int", nullable: false),
                    QuantityAfter = table.Column<int>(type: "int", nullable: false),
                    ReferenceId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MovementDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stocks", x => x.StockID);
                    table.ForeignKey(
                        name: "FK_stocks_Component_ComponentID",
                        column: x => x.ComponentID,
                        principalTable: "Component",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stocks_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClerkID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    AddressID = table.Column<int>(type: "int", nullable: false),
                    SpecialInstructions = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectedCompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsersUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UsersUserID1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_order_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_User_ClerkID",
                        column: x => x.ClerkID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_order_User_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_order_User_UsersUserID",
                        column: x => x.UsersUserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_order_User_UsersUserID1",
                        column: x => x.UsersUserID1,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderItem",
                columns: table => new
                {
                    PurchaseItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderID = table.Column<int>(type: "int", nullable: false),
                    ComponentID = table.Column<int>(type: "int", nullable: false),
                    QuantityOrdered = table.Column<int>(type: "int", nullable: false),
                    QuantityReceived = table.Column<int>(type: "int", nullable: true),
                    UnitCost = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderItem", x => x.PurchaseItemID);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItem_Component_ComponentID",
                        column: x => x.ComponentID,
                        principalTable: "Component",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItem_PurchaseOrder_PurchaseOrderID",
                        column: x => x.PurchaseOrderID,
                        principalTable: "PurchaseOrder",
                        principalColumn: "PurchaseOrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderHistoryLog",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusFrom = table.Column<int>(type: "int", nullable: false),
                    StatusTo = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistoryLog", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_OrderHistoryLog_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderHistoryLog_order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    TemplateID = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    ItemTotalPrice = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssemblyStatus = table.Column<int>(type: "int", nullable: false),
                    AssemblyStartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssemblyCompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.OrderItemID);
                    table.ForeignKey(
                        name: "FK_OrderItem_Furniture_TemplateID",
                        column: x => x.TemplateID,
                        principalTable: "Furniture",
                        principalColumn: "FurnitureID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemComponent",
                columns: table => new
                {
                    ItemComponentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    ComponentID = table.Column<int>(type: "int", nullable: false),
                    QuantityUsed = table.Column<int>(type: "int", nullable: false),
                    UnitPriceAtOrder = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemComponent", x => x.ItemComponentID);
                    table.ForeignKey(
                        name: "FK_ItemComponent_Component_ComponentID",
                        column: x => x.ComponentID,
                        principalTable: "Component",
                        principalColumn: "ComponentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemComponent_OrderItem_ItemID",
                        column: x => x.ItemID,
                        principalTable: "OrderItem",
                        principalColumn: "OrderItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserID",
                table: "Address",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentCompatibility_ComponentID1",
                table: "ComponentCompatibility",
                column: "ComponentID1");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentCompatibility_ComponentID2",
                table: "ComponentCompatibility",
                column: "ComponentID2");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentPopularity_ComponentID",
                table: "ComponentPopularity",
                column: "ComponentID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemComponent_ComponentID",
                table: "ItemComponent",
                column: "ComponentID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemComponent_ItemID",
                table: "ItemComponent",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_order_AddressID",
                table: "order",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_order_ClerkID",
                table: "order",
                column: "ClerkID");

            migrationBuilder.CreateIndex(
                name: "IX_order_CustomerID",
                table: "order",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_order_UsersUserID",
                table: "order",
                column: "UsersUserID");

            migrationBuilder.CreateIndex(
                name: "IX_order_UsersUserID1",
                table: "order",
                column: "UsersUserID1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryLog_OrderID",
                table: "OrderHistoryLog",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryLog_UserID",
                table: "OrderHistoryLog",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderID",
                table: "OrderItem",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_TemplateID",
                table: "OrderItem",
                column: "TemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_ManagerID",
                table: "PurchaseOrder",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItem_ComponentID",
                table: "PurchaseOrderItem",
                column: "ComponentID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItem_PurchaseOrderID",
                table: "PurchaseOrderItem",
                column: "PurchaseOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_ComponentID",
                table: "stocks",
                column: "ComponentID");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_UserID",
                table: "stocks",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateComponent_ComponentID",
                table: "TemplateComponent",
                column: "ComponentID");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateComponent_FurnitureID",
                table: "TemplateComponent",
                column: "FurnitureID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentCompatibility");

            migrationBuilder.DropTable(
                name: "ComponentPopularity");

            migrationBuilder.DropTable(
                name: "ItemComponent");

            migrationBuilder.DropTable(
                name: "OrderHistoryLog");

            migrationBuilder.DropTable(
                name: "OrderProgress");

            migrationBuilder.DropTable(
                name: "PurchaseOrderItem");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "stocks");

            migrationBuilder.DropTable(
                name: "TemplateComponent");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "PurchaseOrder");

            migrationBuilder.DropTable(
                name: "Component");

            migrationBuilder.DropTable(
                name: "Furniture");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
