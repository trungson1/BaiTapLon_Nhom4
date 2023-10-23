using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QL_SinhVien.Models
{
    public class Khoa
    {
        [Key]
        public string MaKhoa { get; set; }
        public string TenKhoa { get; set; } 

    }
}