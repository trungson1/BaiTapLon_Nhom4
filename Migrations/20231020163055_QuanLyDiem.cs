using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QL_SinhVien.Migrations
{
    /// <inheritdoc />
    public partial class QuanLyDiem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuanLyDiem",
                columns: table => new
                {
                    TenMon = table.Column<string>(type: "TEXT", nullable: false),
                    TenSV = table.Column<string>(type: "TEXT", nullable: false),
                    Diem = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuanLyDiem", x => x.TenMon);
                    table.ForeignKey(
                        name: "FK_QuanLyDiem_QuanLyMonHoc_TenMon",
                        column: x => x.TenMon,
                        principalTable: "QuanLyMonHoc",
                        principalColumn: "MaMon",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuanLyDiem_QuanLySV_TenSV",
                        column: x => x.TenSV,
                        principalTable: "QuanLySV",
                        principalColumn: "MaSV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuanLyDiem_TenSV",
                table: "QuanLyDiem",
                column: "TenSV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuanLyDiem");
        }
    }
}
