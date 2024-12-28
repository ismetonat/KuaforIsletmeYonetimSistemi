using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using System.Linq;

public class AccountController : Controller
{
    private readonly KuaforYonetimDbContext _context;

    public AccountController(KuaforYonetimDbContext context)
    {
        _context = context;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            ViewBag.Error = "Geçersiz e-posta veya şifre.";
            return View();
        }

        // Oturum bilgilerini kaydet
        HttpContext.Session.SetString("UserRole", user.Role);
        HttpContext.Session.SetString("UserName", user.FullName);
        HttpContext.Session.SetString("UserEmail", user.Email);

        // Yönlendirme
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult Logout()
    {
        // Oturum bilgilerini temizle
        HttpContext.Session.Clear();

        // Kullanıcı aynı sayfada kalsın
        string referrerUrl = Request.Headers["Referer"].ToString();
        return Redirect(referrerUrl);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string fullName, string email, string password, string confirmPassword)
    {
        if (password != confirmPassword)
        {
            ViewBag.Error = "Şifreler eşleşmiyor.";
            return View();
        }

        if (_context.Users.Any(u => u.Email == email))
        {
            ViewBag.Error = "Bu e-posta adresi zaten kullanılıyor.";
            return View();
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var newUser = new Users
        {
            FullName = fullName,
            Email = email,
            PasswordHash = hashedPassword,
            Role = "User" // Varsayılan olarak "User" rolü atanabilir
        };

        _context.Users.Add(newUser);
        _context.SaveChanges();

        // Başarıyla kayıt olundu
        ViewBag.Success = "Kayıt başarılı! Giriş yapabilirsiniz.";
        return RedirectToAction("Login");
    }
}
