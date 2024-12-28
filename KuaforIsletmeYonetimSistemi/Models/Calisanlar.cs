using System.ComponentModel.DataAnnotations;

public class Calisanlar
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Ad alanı zorunludur.")]
    [StringLength(50, ErrorMessage = "Ad 50 karakterden uzun olamaz.")]
    public string Ad { get; set; }

    [Required(ErrorMessage = "Soyad alanı zorunludur.")]
    [StringLength(50, ErrorMessage = "Soyad 50 karakterden uzun olamaz.")]
    public string Soyad { get; set; }

    [Required(ErrorMessage = "Telefon numarası zorunludur.")]
    [RegularExpression(@"^05\d{9}$|^05\d{2} \d{3} \d{2} \d{2}$", ErrorMessage = "Telefon numarası 05XXXXXXXXX veya 05XX XXX XX XX formatında olmalıdır.")]
    public string Telefon { get; set; }

    [StringLength(100, ErrorMessage = "Unvan 100 karakterden uzun olamaz.")]
    public string Unvan { get; set; }
}
