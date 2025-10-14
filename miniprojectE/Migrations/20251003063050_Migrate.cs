using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace miniprojectE.Migrations
{
    /// <inheritdoc />
    public partial class Migrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "TemplateComponent",
            columns: table => new
            {
                TemplateID = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"), 
                FurnitureID = table.Column<int>(nullable: false),
                ComponentID = table.Column<int>(nullable: false),
                isRequired = table.Column<bool>(nullable: false),
                minLevel = table.Column<int>(nullable: false),
                maxLevel = table.Column<int>(nullable: false),
                ComponentRole = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TemplateComponent", x => x.TemplateID);
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
