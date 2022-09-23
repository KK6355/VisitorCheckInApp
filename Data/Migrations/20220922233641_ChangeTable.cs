using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitorCheckInApp.Data.Migrations
{
    public partial class ChangeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitorCount",
                table: "Staff");

            migrationBuilder.RenameColumn(
                name: "SatffName",
                table: "Staff",
                newName: "StaffName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StaffName",
                table: "Staff",
                newName: "SatffName");

            migrationBuilder.AddColumn<int>(
                name: "VisitorCount",
                table: "Staff",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
