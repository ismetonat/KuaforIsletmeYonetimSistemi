using System.ComponentModel.DataAnnotations;

public class Salon
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Salon adı boş bırakılamaz.")]
    [StringLength(100, ErrorMessage = "Salon adı 100 karakterden uzun olamaz.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Adres boş bırakılamaz.")]
    [StringLength(200, ErrorMessage = "Adres 200 karakterden uzun olamaz.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Telefon numarası boş bırakılamaz.")]
    [Phone(ErrorMessage = "Geçerli bir telefon numarası girin.")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Açıklama boş bırakılamaz.")]
    [StringLength(300, ErrorMessage = "Açıklama 300 karakterden uzun olamaz.")]
    public string Description { get; set; }
    
}
