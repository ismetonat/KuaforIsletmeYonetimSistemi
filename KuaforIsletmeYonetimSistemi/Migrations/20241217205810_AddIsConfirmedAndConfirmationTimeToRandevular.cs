using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuaforIsletmeYonetimSistemi.Migrations
{
    public partial class AddIsConfirmedAndConfirmationTimeToRandevular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmationTime",
                table: "Appointments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Appointments",
                type: "bit",
                nullable: true,
                defaultValue: false);

        
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationTime",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Appointments");

    
        }
    }
}
