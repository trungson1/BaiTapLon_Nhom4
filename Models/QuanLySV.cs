using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QL_SinhVien.Models
{
    [Table("QuanLySV")]
    public class QuanLySV
    {
        [Key]
        //Key cua class Student
        [Display(Name = "Mã sinh viên")]
        public string MaSV { get; set;}
        
        [Display(Name = "Họ và tên")]
        public string TenSV { get; set; }
        [ForeignKey("MaSV")]
        [Display(Name = "Ngày Sinh")]
        [DataType(DataType.Date)]
        public string NgaySinh {get; set; } 
        [Display(Name = "SDT")]
        public string SDT {get; set; }
        public string TenLop { get; set; }
        [ForeignKey("TenLop")]
        [Display(Name = "Lớp")] 
        public Lop? Lop { get; set; }
        public string TenKhoa { get; set; }
        [ForeignKey("TenKhoa")]
        public Khoa? Khoa { get; set; }
        
    }
}