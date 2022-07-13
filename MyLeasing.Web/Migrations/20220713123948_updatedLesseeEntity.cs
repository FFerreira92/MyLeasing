using Microsoft.EntityFrameworkCore.Migrations;

namespace MyLeasing.Web.Migrations
{
    public partial class updatedLesseeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Lessee",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessee_UserId",
                table: "Lessee",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessee_AspNetUsers_UserId",
                table: "Lessee",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessee_AspNetUsers_UserId",
                table: "Lessee");

            migrationBuilder.DropIndex(
                name: "IX_Lessee_UserId",
                table: "Lessee");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Lessee");
        }
    }
}
