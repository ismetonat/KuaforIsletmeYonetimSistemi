using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Randevular
{
    [Key]
    public int Id { get; set; }

    // Foreign Key: Kullanıcı
    [ForeignKey("Kullanici")]
    public int KullaniciId { get; set; }
    public Kullanicilar Kullanici { get; set; }

    // Foreign Key: Çalışan
    [ForeignKey("Calisan")]
    public int CalisanId { get; set; }
    public Calisanlar Calisan { get; set; }

    // Foreign Key: İşlem
    [ForeignKey("Islem")]
    public int IslemId { get; set; }
    public Islemler Islem { get; set; }

    [Required]
    public DateTime TarihSaat { get; set; }
}
