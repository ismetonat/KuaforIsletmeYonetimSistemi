using System.ComponentModel.DataAnnotations;

public class Hizmetler
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Hizmet adı zorunludur.")]
    public string Ad { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır.")]
    public decimal Fiyat { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Süre en az 1 dakika olmalıdır.")]
    public int SureDakika { get; set; }

    public string Aciklama { get; set; }

    // GorselYolu artık isteğe bağlı
    public string GorselYolu { get; set; }
}
