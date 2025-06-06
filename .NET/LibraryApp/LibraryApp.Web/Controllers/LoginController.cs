using LibraryApp.Infrastructure.Repositories;
using LibraryApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Controllers;

/// <summary>
///     Контроллер для обработки запросов на страницу авторизации пользователя.
/// </summary>
/// <param name="userRepository">Репозиторий для работы с данными пользователей.</param>
[Route("/")]
[Route("/login")]
public class LoginController(IUserRepository userRepository) : Controller
{
    /// <summary>
    ///     Отображать страницу логина.
    /// </summary>
    /// <returns>
    ///     Возвращает представление для страницы логина. Если пользователь уже авторизован, происходит перенаправление на
    ///     страницу профиля.
    /// </returns>
    [HttpGet]
    public IActionResult Index()
    {
        var userEmail = HttpContext.Session.GetString("UserEmail");

        if (userEmail != null) return RedirectToAction("Index", "Profile");

        return View(new LoginViewModel());
    }

    /// <summary>
    ///     Обрабатывать запросы на авторизацию пользователя.
    /// </summary>
    /// <param name="model">Модель с данными для входа.</param>
    /// <returns>Если вход успешен, перенаправляет на главную страницу, иначе возвращает представление с ошибкой.</returns>
    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = await userRepository.GetByEmail(model.Email!);

        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Неверная почта или пароль.");

            return View(model);
        }

        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("UserEmail", user.Email);
        HttpContext.Session.SetString("UserName", user.Name);
        HttpContext.Session.SetString("UserSurname", user.Surname);
        HttpContext.Session.SetInt32("UserRole", user.Role.Id);

        if (userRepository.VerifyByPassword(user, model.Password!))
            return RedirectToAction("Index", "Profile");

        ModelState.AddModelError(string.Empty, "Неверная почта или пароль.");

        return View(model);
    }
}