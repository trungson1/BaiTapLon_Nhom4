using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QL_SinhVien.Models
{
    public class Lop
    {
        [Key]
        public string MaLop { get; set; }
        public string TenLop { get; set; } 

    }
}