namespace KuaforIsletmeYonetimSistemi.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int ToplamKullanici { get; set; }
        public int ToplamCalisan { get; set; }
        public int ToplamRandevu { get; set; }
        public int ToplamHizmet { get; set; }
        public decimal HaftalikGelir { get; set; }
        public decimal AylikGelir { get; set; }
        public List<string> SonRandevular { get; set; }
    }
}
