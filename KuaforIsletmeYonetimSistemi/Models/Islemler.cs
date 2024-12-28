using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Islemler
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Ad { get; set; }

    [Required]
    public int Sure { get; set; } // Dakika cinsinden süre

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Ucret { get; set; } // Ücret bilgisi

    // Foreign Key: Salonlar tablosuna bağlı
    [ForeignKey("Salon")]
    public int SalonId { get; set; }
    public Salonlar Salon { get; set; }
}
