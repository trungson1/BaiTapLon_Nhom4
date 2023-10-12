using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QL_SinhVien.Models
{
    [Table("Lop")]
    public class Lop
    {
        [Key]

        [Display(Name = "Mã lớp")]

        public string MaLop { get; set; }

        [Display(Name = "Tên lớp")]

        public string TenLop { get; set; } 

    }
}