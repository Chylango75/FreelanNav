using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcFreelan.Migrations
{
    /// <inheritdoc />
    public partial class Mypay03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MypayName",
                table: "MypayTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_MypayTypes_MypayName",
                table: "MypayTypes",
                column: "MypayName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MypayTypes_MypayName",
                table: "MypayTypes");

            migrationBuilder.AlterColumn<string>(
                name: "MypayName",
                table: "MypayTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
