using Microsoft.AspNetCore.Mvc;
using KuaforIsletmeYonetimSistemi.ViewModels;
using System.Linq;

namespace KuaforIsletmeYonetimSistemi.Controllers
{
    public class AdminController : Controller
    {
        private readonly KuaforYonetimDbContext _context;

        public AdminController(KuaforYonetimDbContext context)
        {
            _context = context;
        }

        public IActionResult AdminDashboard()
        {
            try
            {
                // Genel Bilgiler
                var toplamKullanici = _context.Users.Count();
                var toplamCalisan = _context.Calisanlar.Count();
                var toplamRandevu = _context.Randevular.Count();
                var toplamHizmet = _context.Hizmetler.Count();

                // Haftalık ve Aylık Gelir Hesaplama
                var bugundenBirHaftaOncesi = DateTime.Now.AddDays(-7);
                var haftalikGelir = _context.Randevular
                    .Where(r => r.TarihSaat >= bugundenBirHaftaOncesi && r.Islem != null)
                    .Sum(r => (decimal?)r.Islem.Ucret) ?? 0; // Null kontrolleri eklendi

                var bugundenBirAyOncesi = DateTime.Now.AddMonths(-1);
                var aylikGelir = _context.Randevular
                    .Where(r => r.TarihSaat >= bugundenBirAyOncesi && r.Islem != null)
                    .Sum(r => (decimal?)r.Islem.Ucret) ?? 0; // Null kontrolleri eklendi

                // ViewModel Verileri
                var viewModel = new AdminDashboardViewModel
                {
                    ToplamKullanici = toplamKullanici,
                    ToplamCalisan = toplamCalisan,
                    ToplamRandevu = toplamRandevu,
                    ToplamHizmet = toplamHizmet,
                    HaftalikGelir = haftalikGelir,
                    AylikGelir = aylikGelir
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Hata durumunda mesaj ve boş bir ViewModel döndür
                ViewBag.ErrorMessage = $"Bir hata oluştu: {ex.Message}";
                return View(new AdminDashboardViewModel());
            }
        }
    }
}
