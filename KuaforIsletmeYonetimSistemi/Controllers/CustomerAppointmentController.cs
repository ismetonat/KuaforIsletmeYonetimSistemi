using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CustomerAppointmentController : Controller
{
    private readonly KuaforYonetimDbContext _context;

    public CustomerAppointmentController(KuaforYonetimDbContext context)
    {
        _context = context;
    }

    // Müşteri Randevuları - Sadece giriş yapan kullanıcının randevularını listele
    public IActionResult Index()
    {
        var userEmail = HttpContext.Session.GetString("UserEmail"); // Oturumdan kullanıcının e-posta adresi alınır
        var customerAppointments = _context.Randevular
            .Include(r => r.Calisan)
            .Include(r => r.Islem)
            .Where(r => r.Kullanici.Email == userEmail)
            .ToList();

        return View(customerAppointments);
    }

    // Yeni randevu oluşturma
    public IActionResult Create()
    {
        ViewBag.Employees = _context.Calisanlar.ToList();
        ViewBag.Services = _context.Hizmetler.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Randevular appointment)
    {
        if (ModelState.IsValid)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var user = _context.Kullanicilar.FirstOrDefault(u => u.Email == userEmail);

            if (user == null)
            {
                return Unauthorized();
            }

            appointment.KullaniciId = user.Id;
            _context.Randevular.Add(appointment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.Employees = _context.Calisanlar.ToList();
        ViewBag.Services = _context.Hizmetler.ToList();
        return View(appointment);
    }
}
