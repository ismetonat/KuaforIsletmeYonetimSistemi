using Microsoft.AspNetCore.Mvc;

public class AppointmentController : Controller
{
    private readonly KuaforYonetimDbContext _context;

    public AppointmentController(KuaforYonetimDbContext context)
    {
        _context = context;
    }

    // Randevuları Listeleme
    public IActionResult Index(string searchQuery, DateTime? dateFilter)
    {
        if (HttpContext.User.Identity.IsAuthenticated)
        {
            TempData["Email"] = HttpContext.User.Identity.Name;
        }

        var appointments = _context.Appointments.AsQueryable();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            appointments = appointments.Where(a =>
                a.CustomerName.Contains(searchQuery) ||
                a.EmployeeName.Contains(searchQuery) ||
                a.ServiceType.Contains(searchQuery));
        }

        if (dateFilter.HasValue)
        {
            appointments = appointments.Where(a => a.AppointmentDate == dateFilter.Value);
        }

        return View(appointments.ToList());
    }

    // Yeni Randevu Ekleme Sayfası
    public IActionResult Create()
    {
        TempData.Keep("Email");

        ViewBag.Hizmetler = _context.Hizmetler.ToList();
        ViewBag.Calisanlar = _context.Calisanlar.ToList();
        ViewBag.Salons = _context.Salons.ToList(); // Salonlar burada dolduruluyor
        return View();
    }

    [HttpPost]
    public IActionResult Create(Appointment appointment)
    {
        TempData.Keep("Email");

        if (appointment.AppointmentDate < DateTime.Now.Date)
        {
            ViewBag.Error = "Geçmiş tarihe yönelik randevu alınamaz.";
            ViewBag.Hizmetler = _context.Hizmetler.ToList();
            ViewBag.Calisanlar = _context.Calisanlar.ToList();
            ViewBag.Salons = _context.Salons.ToList();
            return View(appointment);
        }

        // Hizmet Süresi Kontrolü
        var hizmet = _context.Hizmetler.FirstOrDefault(h => h.Ad == appointment.ServiceType);
        if (hizmet == null)
        {
            ViewBag.Error = "Geçersiz hizmet türü seçildi.";
            ViewBag.Hizmetler = _context.Hizmetler.ToList();
            ViewBag.Calisanlar = _context.Calisanlar.ToList();
            ViewBag.Salons = _context.Salons.ToList();
            return View(appointment);
        }

        int hizmetSuresi = hizmet.SureDakika;
        DateTime yeniBaslangic = appointment.AppointmentDate.Add(appointment.AppointmentTime);
        DateTime yeniBitis = yeniBaslangic.AddMinutes(hizmetSuresi);

        // Çalışan Bazlı Çakışma Kontrolü
        var existingAppointments = _context.Appointments
            .Where(a => a.EmployeeName == appointment.EmployeeName)
            .ToList(); // Sorguyu bellekte çalıştırmak için ToList()

        bool conflictingAppointment = existingAppointments.Any(a =>
        {
            DateTime mevcutBaslangic = a.AppointmentDate.Add(a.AppointmentTime);
            DateTime mevcutBitis = mevcutBaslangic.AddMinutes(hizmetSuresi);
            return (yeniBaslangic <
            mevcutBitis) && (yeniBitis > mevcutBaslangic);
        });

        if (conflictingAppointment)
        {
            ViewBag.Error = "Bu saat aralığında farklı bir randevu bulunmaktadır.";
            ViewBag.Hizmetler = _context.Hizmetler.ToList();
            ViewBag.Calisanlar = _context.Calisanlar.ToList();
            ViewBag.Salons = _context.Salons.ToList(); // Salonlar burada tekrar dolduruluyor
            return View(appointment);
        }

        if (ModelState.IsValid)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.Hizmetler = _context.Hizmetler.ToList();
        ViewBag.Calisanlar = _context.Calisanlar.ToList();
        ViewBag.Salons = _context.Salons.ToList(); // Salonlar burada tekrar dolduruluyor
        return View(appointment);
    }

    // Randevu Silme
    public IActionResult Delete(int id)
    {
        TempData.Keep("Email");

        var appointment = _context.Appointments.Find(id);
        if (appointment != null)
        {
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
