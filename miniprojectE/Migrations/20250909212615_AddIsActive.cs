using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace miniprojectE.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_User_UsersUserID",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_order_User_UsersUserID1",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_UsersUserID",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_UsersUserID1",
                table: "order");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "User");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "User");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UsersUserID",
                table: "order");

            migrationBuilder.DropColumn(
                name: "UsersUserID1",
                table: "order");

            migrationBuilder.RenameColumn(
                name: "TwoFactorEnabled",
                table: "User",
                newName: "IsActive");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "User",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "order",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Component",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserEmail",
                table: "User",
                column: "UserEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_OrderNumber",
                table: "order",
                column: "OrderNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_UserEmail",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_order_OrderNumber",
                table: "order");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Component");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "User",
                newName: "TwoFactorEnabled");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "User",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "order",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "UsersUserID",
                table: "order",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsersUserID1",
                table: "order",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_UsersUserID",
                table: "order",
                column: "UsersUserID");

            migrationBuilder.CreateIndex(
                name: "IX_order_UsersUserID1",
                table: "order",
                column: "UsersUserID1");

            migrationBuilder.AddForeignKey(
                name: "FK_order_User_UsersUserID",
                table: "order",
                column: "UsersUserID",
                principalTable: "User",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_order_User_UsersUserID1",
                table: "order",
                column: "UsersUserID1",
                principalTable: "User",
                principalColumn: "UserID");
        }
    }
}
