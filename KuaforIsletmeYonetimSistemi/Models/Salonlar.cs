using System.ComponentModel.DataAnnotations;

public class Salonlar
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Ad { get; set; }

    public string Adres { get; set; }

    public string CalismaSaatleri { get; set; }
}
