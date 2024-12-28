using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("Calisanlar")]
public class CalisanlarController : Controller
{
    private readonly KuaforYonetimDbContext _context;

    public CalisanlarController(KuaforYonetimDbContext context)
    {
        _context = context;
    }

    // GET: Calisanlar
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var calisanlar = await _context.Calisanlar.ToListAsync();
        return View(calisanlar);
    }

    // GET: Calisanlar/Create
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Calisanlar/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Ad,Soyad,Telefon,Unvan")] Calisanlar calisan)
    {
        // Daha önce aynı Ad ve Soyad ile kayıt olup olmadığını kontrol edin
        bool calisanExists = _context.Calisanlar.Any(c => c.Ad == calisan.Ad && c.Soyad == calisan.Soyad);

        if (calisanExists)
        {
            // Eğer aynı kayıt varsa, hata mesajını göster
            TempData["Error"] = "Bu kayıt daha önceden oluşturulmuştur. Lütfen farklı isim soyisim de bir çalışan ekleyiniz.";
            return View(calisan);
        }

        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Bu kayıt daha önceden oluşturulmuştur. Lütfen farklı isim soyisim de bir çalışan ekleyiniz..";
            return View(calisan);
        }

        _context.Add(calisan);
        await _context.SaveChangesAsync();
        TempData["Message"] = "Yeni çalışan başarıyla eklendi.";
        return RedirectToAction(nameof(Index));
    }

    // GET: Calisanlar/Edit/{id}
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var calisan = await _context.Calisanlar.FindAsync(id);
        if (calisan == null)
        {
            TempData["Error"] = "Düzenlemek istediğiniz çalışan bulunamadı.";
            return RedirectToAction(nameof(Index));
        }

        return View(calisan);
    }

    // POST: Calisanlar/Edit/{id}
    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Soyad,Telefon,Unvan")] Calisanlar calisan)
    {
        if (id != calisan.Id)
        {
            TempData["Error"] = "Çalışan bilgisi eşleşmiyor.";
            return RedirectToAction(nameof(Index));
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(calisan);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Çalışan başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Calisanlar.Any(e => e.Id == id))
                {
                    TempData["Error"] = "Güncellemek istediğiniz çalışan artık mevcut değil.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw;
                }
            }
        }

        return View(calisan);
    }

    // GET: Calisanlar/Delete/{id}
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var calisan = await _context.Calisanlar.FindAsync(id);
        if (calisan == null)
        {
            TempData["Error"] = "Silmek istediğiniz çalışan bulunamadı.";
            return RedirectToAction(nameof(Index));
        }

        return View(calisan);
    }

    // POST: Calisanlar/Delete/{id}
    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var calisan = await _context.Calisanlar.FindAsync(id);
        if (calisan == null)
        {
            TempData["Error"] = "Silmek istediğiniz çalışan bulunamadı.";
            return RedirectToAction(nameof(Index));
        }

        _context.Calisanlar.Remove(calisan);
        await _context.SaveChangesAsync();

        TempData["Message"] = "Çalışan başarıyla silindi.";
        return RedirectToAction(nameof(Index));
    }
}
