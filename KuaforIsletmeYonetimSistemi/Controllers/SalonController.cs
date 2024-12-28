using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class SalonController : Controller
{
    private readonly KuaforYonetimDbContext _context;

    public SalonController(KuaforYonetimDbContext context)
    {
        _context = context;
    }

    // Salonları Listeleme
    public IActionResult Index()
    {
        var salons = _context.Salons.ToList();
        return View(salons);
    }

    // Yeni Salon Ekleme Sayfası
    public IActionResult Create()
    {
        // Boş bir model döndürerek View'da null referans hatasını önler
        return View(new Salon());
    }

    [HttpPost]
    public IActionResult Create(Salon salon)
    {
        if (!ModelState.IsValid)
        {
            // Model doğrulama hataları varsa formu yeniden göster
            return View(salon);
        }

        _context.Salons.Add(salon);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    // Salon Düzenleme Sayfası
    public IActionResult Edit(int id)
    {
        var salon = _context.Salons.Find(id);
        if (salon == null)
        {
            // Eğer ilgili salon bulunamazsa 404 Not Found döndür
            return NotFound();
        }
        return View(salon);
    }

    [HttpPost]
    public IActionResult Edit(Salon salon)
    {
        if (!ModelState.IsValid)
        {
            // Model doğrulama hataları varsa formu yeniden göster
            return View(salon);
        }

        _context.Salons.Update(salon);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    // Salon Silme
    public IActionResult Delete(int id)
    {
        var salon = _context.Salons.Find(id);
        if (salon == null)
        {
            // Eğer ilgili salon bulunamazsa 404 Not Found döndür
            return NotFound();
        }

        _context.Salons.Remove(salon);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
