using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QL_SinhVien.Models
{
    [Table("QuanLyMonHoc")]
    public class QuanLyMonHoc
    {
        [Key]

        [Display(Name = "Mã môn học")]

        public string MaMon { get; set; }

        [Display(Name = "Tên môn học")]

        public string TenMon { get; set; } 

    }
}