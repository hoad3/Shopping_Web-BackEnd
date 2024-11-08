using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_2.Migrations
{
    /// <inheritdoc />
    public partial class Update_22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phuongthucthanhtoan",
                schema: "Data",
                table: "ThanhToan",
                newName: "Trangthaithanhtoan");

            migrationBuilder.AddColumn<int>(
                name: "phuongthucthanhtoan",
                schema: "Data",
                table: "Donmua",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phuongthucthanhtoan",
                schema: "Data",
                table: "Donmua");

            migrationBuilder.RenameColumn(
                name: "Trangthaithanhtoan",
                schema: "Data",
                table: "ThanhToan",
                newName: "Phuongthucthanhtoan");
        }
    }
}
