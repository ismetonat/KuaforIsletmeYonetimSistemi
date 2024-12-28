using KuaforIsletmeYonetimSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace KuaforIsletmeYonetimSistemi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KuaforYonetimDbContext _context; // Veritabanı bağlamı

        public HomeController(ILogger<HomeController> logger, KuaforYonetimDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Ana sayfa: Salonlar listesini getir ve View'a gönder
        public IActionResult Index()
        {
            try
            {
                var salonlar = _context.Salonlar.ToList(); // Veritabanından salonları çek
                return View(salonlar);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hata: {ex.Message}");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        // Gizlilik politikası sayfası
        public IActionResult Privacy()
        {
            ViewBag.Message = "Bu, gizlilik politikası sayfasıdır.";
            return View();
        }

        // Admin Dashboard: Admin yetkisi kontrol edilir
        public IActionResult AdminDashboard()
        {
            if (!IsAuthorized("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // Kullanıcı Dashboard: Kullanıcı yetkisi kontrol edilir
        public IActionResult UserDashboard()
        {
            if (!IsAuthorized("User"))
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // Hata sayfası
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Yetki kontrolü metodu
        private bool IsAuthorized(string role)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            return userRole != null && userRole == role;
        }
    }
}
