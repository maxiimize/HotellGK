using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotellGK.Migrations
{
    
    public partial class AddMaxExtraBedsToRoom : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxExtraBeds",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 1,
                columns: new[] { "IsAvailable", "MaxExtraBeds" },
                values: new object[] { true, 0 });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 2,
                columns: new[] { "IsAvailable", "MaxExtraBeds" },
                values: new object[] { true, 2 });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 3,
                columns: new[] { "IsAvailable", "MaxExtraBeds" },
                values: new object[] { true, 1 });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 4,
                columns: new[] { "IsAvailable", "MaxExtraBeds" },
                values: new object[] { true, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxExtraBeds",
                table: "Rooms");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 1,
                column: "IsAvailable",
                value: false);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 2,
                column: "IsAvailable",
                value: false);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 3,
                column: "IsAvailable",
                value: false);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 4,
                column: "IsAvailable",
                value: false);
        }
    }
}
