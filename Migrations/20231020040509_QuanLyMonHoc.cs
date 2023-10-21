using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QL_SinhVien.Migrations
{
    /// <inheritdoc />
    public partial class QuanLyMonHoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuanLyMonHoc",
                columns: table => new
                {
                    MaMon = table.Column<string>(type: "TEXT", nullable: false),
                    TenMon = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuanLyMonHoc", x => x.MaMon);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuanLyMonHoc");
        }
    }
}
