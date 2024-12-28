using Microsoft.AspNetCore.Mvc;

namespace KuaforIsletmeYonetimSistemi.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
