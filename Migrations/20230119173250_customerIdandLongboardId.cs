using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardRental.Migrations
{
    public partial class customerIdandLongboardId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookedBoards_Customers_CustomerId",
                table: "BookedBoards");

            migrationBuilder.DropForeignKey(
                name: "FK_BookedBoards_Longboards_LongboardId",
                table: "BookedBoards");

            migrationBuilder.AlterColumn<int>(
                name: "LongboardId",
                table: "BookedBoards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "BookedBoards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookedBoards_Customers_CustomerId",
                table: "BookedBoards",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookedBoards_Longboards_LongboardId",
                table: "BookedBoards",
                column: "LongboardId",
                principalTable: "Longboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookedBoards_Customers_CustomerId",
                table: "BookedBoards");

            migrationBuilder.DropForeignKey(
                name: "FK_BookedBoards_Longboards_LongboardId",
                table: "BookedBoards");

            migrationBuilder.AlterColumn<int>(
                name: "LongboardId",
                table: "BookedBoards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "BookedBoards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BookedBoards_Customers_CustomerId",
                table: "BookedBoards",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookedBoards_Longboards_LongboardId",
                table: "BookedBoards",
                column: "LongboardId",
                principalTable: "Longboards",
                principalColumn: "Id");
        }
    }
}
