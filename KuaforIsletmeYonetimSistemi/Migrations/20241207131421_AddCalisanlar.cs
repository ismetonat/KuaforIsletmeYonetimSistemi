using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuaforIsletmeYonetimSistemi.Migrations
{
    public partial class AddCalisanlar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calisanlar_Salonlar_SalonId",
                table: "Calisanlar");

            migrationBuilder.DropIndex(
                name: "IX_Calisanlar_SalonId",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "AdSoyad",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "SalonId",
                table: "Calisanlar");

            migrationBuilder.RenameColumn(
                name: "Uzmanlik",
                table: "Calisanlar",
                newName: "Telefon");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Salons",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Ad",
                table: "Calisanlar",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Soyad",
                table: "Calisanlar",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unvan",
                table: "Calisanlar",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ad",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "Soyad",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "Unvan",
                table: "Calisanlar");

            migrationBuilder.RenameColumn(
                name: "Telefon",
                table: "Calisanlar",
                newName: "Uzmanlik");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Salons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AddColumn<string>(
                name: "AdSoyad",
                table: "Calisanlar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SalonId",
                table: "Calisanlar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_SalonId",
                table: "Calisanlar",
                column: "SalonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calisanlar_Salonlar_SalonId",
                table: "Calisanlar",
                column: "SalonId",
                principalTable: "Salonlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
