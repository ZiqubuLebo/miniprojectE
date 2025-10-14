using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace miniprojectE.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_Address_AddressID",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_order_User_ClerkID",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_order_User_CustomerID",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistoryLog_order_OrderID",
                table: "OrderHistoryLog");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_order_OrderID",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_stocks_Component_ComponentID",
                table: "stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_stocks_User_UserID",
                table: "stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_stocks",
                table: "stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_order",
                table: "order");

            migrationBuilder.RenameTable(
                name: "stocks",
                newName: "Stocks");

            migrationBuilder.RenameTable(
                name: "order",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_stocks_UserID",
                table: "Stocks",
                newName: "IX_Stocks_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_stocks_ComponentID",
                table: "Stocks",
                newName: "IX_Stocks_ComponentID");

            migrationBuilder.RenameIndex(
                name: "IX_order_OrderNumber",
                table: "Order",
                newName: "IX_Order_OrderNumber");

            migrationBuilder.RenameIndex(
                name: "IX_order_CustomerID",
                table: "Order",
                newName: "IX_Order_CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_order_ClerkID",
                table: "Order",
                newName: "IX_Order_ClerkID");

            migrationBuilder.RenameIndex(
                name: "IX_order_AddressID",
                table: "Order",
                newName: "IX_Order_AddressID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks",
                column: "StockID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Address_AddressID",
                table: "Order",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_ClerkID",
                table: "Order",
                column: "ClerkID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_CustomerID",
                table: "Order",
                column: "CustomerID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistoryLog_Order_OrderID",
                table: "OrderHistoryLog",
                column: "OrderID",
                principalTable: "Order",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderID",
                table: "OrderItem",
                column: "OrderID",
                principalTable: "Order",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Component_ComponentID",
                table: "Stocks",
                column: "ComponentID",
                principalTable: "Component",
                principalColumn: "ComponentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_User_UserID",
                table: "Stocks",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Address_AddressID",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_ClerkID",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_CustomerID",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistoryLog_Order_OrderID",
                table: "OrderHistoryLog");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderID",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Component_ComponentID",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_User_UserID",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Stocks",
                newName: "stocks");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "order");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_UserID",
                table: "stocks",
                newName: "IX_stocks_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_ComponentID",
                table: "stocks",
                newName: "IX_stocks_ComponentID");

            migrationBuilder.RenameIndex(
                name: "IX_Order_OrderNumber",
                table: "order",
                newName: "IX_order_OrderNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerID",
                table: "order",
                newName: "IX_order_CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ClerkID",
                table: "order",
                newName: "IX_order_ClerkID");

            migrationBuilder.RenameIndex(
                name: "IX_Order_AddressID",
                table: "order",
                newName: "IX_order_AddressID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_stocks",
                table: "stocks",
                column: "StockID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_order",
                table: "order",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_order_Address_AddressID",
                table: "order",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_User_ClerkID",
                table: "order",
                column: "ClerkID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_order_User_CustomerID",
                table: "order",
                column: "CustomerID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistoryLog_order_OrderID",
                table: "OrderHistoryLog",
                column: "OrderID",
                principalTable: "order",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_order_OrderID",
                table: "OrderItem",
                column: "OrderID",
                principalTable: "order",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_stocks_Component_ComponentID",
                table: "stocks",
                column: "ComponentID",
                principalTable: "Component",
                principalColumn: "ComponentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_stocks_User_UserID",
                table: "stocks",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
