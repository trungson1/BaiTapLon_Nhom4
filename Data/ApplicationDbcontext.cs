using QL_SinhVien.Models;
using Microsoft.EntityFrameworkCore;

namespace QL_SinhVien.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<QL_SinhVien.Models.QuanLySV> QuanLySV { get; set; } = default!;
        public DbSet<QL_SinhVien.Models.Lop> Lop { get; set; } = default!;
        public DbSet<QL_SinhVien.Models.Khoa> Khoa { get; set; } = default!;
        public DbSet<QL_SinhVien.Models.QuanLyMonHoc> QuanLyMonHoc { get; set; } = default!;
         public DbSet<QL_SinhVien.Models.QuanLyDiem> QuanLyDiem { get; set; } = default!;

       
    }
}