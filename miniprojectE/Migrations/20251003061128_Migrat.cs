using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace miniprojectE.Migrations
{
    /// <inheritdoc />
    public partial class Migrat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemplateComponent_Component_ComponentID",
                table: "TemplateComponent");

            migrationBuilder.DropIndex(
                name: "IX_TemplateComponent_FurnitureID",
                table: "TemplateComponent");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateComponent_FurnitureID_ComponentID",
                table: "TemplateComponent",
                columns: new[] { "FurnitureID", "ComponentID" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TemplateComponent_Component_ComponentID",
                table: "TemplateComponent",
                column: "ComponentID",
                principalTable: "Component",
                principalColumn: "ComponentID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemplateComponent_Component_ComponentID",
                table: "TemplateComponent");

            migrationBuilder.DropIndex(
                name: "IX_TemplateComponent_FurnitureID_ComponentID",
                table: "TemplateComponent");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateComponent_FurnitureID",
                table: "TemplateComponent",
                column: "FurnitureID");

            migrationBuilder.AddForeignKey(
                name: "FK_TemplateComponent_Component_ComponentID",
                table: "TemplateComponent",
                column: "ComponentID",
                principalTable: "Component",
                principalColumn: "ComponentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
