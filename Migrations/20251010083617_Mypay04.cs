using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcFreelan.Migrations
{
    /// <inheritdoc />
    public partial class Mypay04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedMypaytypeId",
                table: "Mypays",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedMypaytypeId",
                table: "Mypays");
        }
    }
}
