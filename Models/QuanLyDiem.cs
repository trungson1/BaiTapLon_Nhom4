using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QL_SinhVien.Models
{
    [Table("QuanLyDiem")]
    public class QuanLyDiem
    {
        [Key]
        
        public string TenMon { get; set; }
        [ForeignKey("TenMon")]
        [Display(Name = "Mã Môn Học")]
        public QuanLyMonHoc? QuanLyMonHoc { get; set; }
        
        public string TenSV { get; set; } 
        [ForeignKey("TenSV")]
        [Display(Name = "Mã Sinh Viên")]
        public QuanLySV? QuanLySV { get; set; }
        [Display(Name = "Điểm")]
        public string Diem {get; set;}
    }
}