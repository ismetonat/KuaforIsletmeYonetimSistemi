using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuaforIsletmeYonetimSistemi.Migrations
{
    public partial class AddGorselYoluToHizmetler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GorselYolu",
                table: "Hizmetler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GorselYolu",
                table: "Hizmetler");
        }
    }
}
