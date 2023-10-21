using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QL_SinhVien.Models
{
    [Table("QuanLyDiem")]
    public class QuanLyDiem
    {
        [Key]
        [Display(Name = "Tên Môn Học")]
        public string TenMon { get; set; }
        [ForeignKey("TenMon")]
        public QuanLyMonHoc? QuanLyMonHoc { get; set; }
        
        [Display(Name = "Tên Sinh Viên")]

        public string TenSV { get; set; } 
        [ForeignKey("TenSV")]
        public QuanLySV? QuanLySV { get; set; }
        [Display(Name = "Điểm")]
        public string Diem {get; set;}
    }
}