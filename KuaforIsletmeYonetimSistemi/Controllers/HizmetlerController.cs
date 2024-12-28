using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

[Route("Hizmetler")]
public class HizmetlerController : Controller
{
    private readonly KuaforYonetimDbContext _context;

    public HizmetlerController(KuaforYonetimDbContext context)
    {
        _context = context;
    }

    // GET: Hizmetler/Index
    [HttpGet("")]
    public IActionResult Index()
    {
        var hizmetler = _context.Hizmetler.ToList();

        // Kullanıcı adını al ve ViewData'ya ekle
        var userName = User.Identity.Name;
        ViewData["UserName"] = userName;

        return View(hizmetler);
    }

    // GET: Hizmetler/Create
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Hizmetler/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Hizmetler hizmet, IFormFile Gorsel)
    {
        try
        {
            if (Gorsel != null && Gorsel.Length > 0)
            {
                string dosyaAdi = Path.GetFileName(Gorsel.FileName);
                string yol = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", dosyaAdi);

                using (var stream = new FileStream(yol, FileMode.Create))
                {
                    Gorsel.CopyTo(stream);
                }

                hizmet.GorselYolu = "/images/" + dosyaAdi;
                ModelState.Remove("GorselYolu");
            }
            else
            {
                TempData["Error"] = "Lütfen bir görsel seçin.";
                return View(hizmet);
            }

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Formdaki alanları doğru doldurduğunuzdan emin olun.";
                return View(hizmet);
            }

            var existingHizmet = _context.Hizmetler.FirstOrDefault(h => h.Ad == hizmet.Ad);
            if (existingHizmet != null)
            {
                TempData["Error"] = "Bu isimde bir hizmet zaten mevcut. Lütfen başka bir isim deneyin.";
                return View(hizmet);
            }

            _context.Hizmetler.Add(hizmet);
            _context.SaveChanges();

            TempData["Message"] = "Hizmet başarıyla eklendi.";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Hizmet eklenirken bir hata oluştu: {ex.Message}";
            return View(hizmet);
        }
    }

    // GET: Hizmetler/Edit/{id}
    [HttpGet("Edit/{id}")]
    public IActionResult Edit(int id)
    {
        var hizmet = _context.Hizmetler.Find(id);
        if (hizmet == null)
        {
            TempData["Error"] = "Düzenlemek istediğiniz hizmet bulunamadı.";
            return RedirectToAction("Index");
        }

        return View(hizmet);
    }

    // POST: Hizmetler/Edit/{id}
    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Hizmetler hizmet, IFormFile Gorsel)
    {
        try
        {
            if (id != hizmet.Id)
            {
                TempData["Error"] = "Hizmet bilgisi uyuşmuyor.";
                return RedirectToAction("Index");
            }

            if (Gorsel != null && Gorsel.Length > 0)
            {
                string dosyaAdi = Path.GetFileName(Gorsel.FileName);
                string yol = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", dosyaAdi);

                using (var stream = new FileStream(yol, FileMode.Create))
                {
                    Gorsel.CopyTo(stream);
                }

                hizmet.GorselYolu = "/images/" + dosyaAdi;
                ModelState.Remove("GorselYolu");
            }

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Formdaki alanları doğru doldurduğunuzdan emin olun.";
                return View(hizmet);
            }

            _context.Hizmetler.Update(hizmet);
            _context.SaveChanges();

            TempData["Message"] = "Hizmet başarıyla güncellendi.";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Hizmet güncellenirken bir hata oluştu: {ex.Message}";
            return View(hizmet);
        }
    }

    // GET: Hizmetler/Delete/{id}
    [HttpGet("Delete/{id}")]
    public IActionResult Delete(int id)
    {
        var hizmet = _context.Hizmetler.Find(id);
        if (hizmet == null)
        {
            TempData["Error"] = "Silmek istediğiniz hizmet bulunamadı.";
            return RedirectToAction("Index");
        }

        return View(hizmet);
    }

    // POST: Hizmetler/Delete/{id}
    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        try
        {
            var hizmet = _context.Hizmetler.Find(id);
            if (hizmet == null)
            {
                TempData["Error"] = "Silmek istediğiniz hizmet bulunamadı.";
                return RedirectToAction("Index");
            }

            _context.Hizmetler.Remove(hizmet);
            _context.SaveChanges();

            TempData["Message"] = "Hizmet başarıyla silindi.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Hizmet silinirken bir hata oluştu: {ex.Message}";
        }

        return RedirectToAction("Index");
    }
}
