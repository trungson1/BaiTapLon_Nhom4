using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QL_SinhVien.Models
{
    [Table("Khoa")]
    public class Khoa
    {
        [Key]

        [Display(Name = "Mã khoa")]

        public string MaKhoa { get; set; }
        
        [Display(Name = "Tên khoa")]

        public string TenKhoa { get; set; } 

    }
}