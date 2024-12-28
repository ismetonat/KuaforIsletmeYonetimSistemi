using Microsoft.AspNetCore.Mvc;

public class UserStatusViewComponent : ViewComponent
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserStatusViewComponent(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IViewComponentResult Invoke()
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var userEmail = session.GetString("UserEmail");
        var userRole = session.GetString("UserRole");

        // Kullanıcı giriş yapmamışsa Guest görünümünü döndür
        if (string.IsNullOrEmpty(userRole))
        {
            return View("Guest");
        }

        // Kullanıcı giriş yapmışsa LoggedIn görünümünü döndür
        return View("LoggedIn", userEmail);
    }
}
