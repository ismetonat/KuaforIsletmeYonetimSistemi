using System.ComponentModel.DataAnnotations;

public class Kullanicilar
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string AdSoyad { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Sifre { get; set; }

}
