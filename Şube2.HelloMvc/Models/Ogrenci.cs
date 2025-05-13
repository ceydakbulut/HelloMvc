using System.ComponentModel.DataAnnotations.Schema;

namespace Şube2.HelloMvc.Models
{
    public class Ogrenci
    {
        

        public int OgrenciId { get; set; }
        public string? Ad{ get; set; }
        public string? Soyad { get; set; }
    }
}
