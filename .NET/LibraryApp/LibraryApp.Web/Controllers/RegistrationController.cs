using LibraryApp.Domain.Entities;
using LibraryApp.Infrastructure.Repositories;
using LibraryApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Controllers;

/// <summary>
///     Контроллер для обработки регистрации пользователей.
/// </summary>
/// <param name="userRepository">Репозиторий для работы с пользователями.</param>
[Route("/register")]
public class RegistrationController(IUserRepository userRepository, IUserRoleRepository userRoleRepository) : Controller
{
    /// <summary>
    ///     Отображение страницы регистрации.
    /// </summary>
    /// <returns>Представление с формой регистрации.</returns>
    [HttpGet]
    public IActionResult Index()
    {
        return View(new RegistrationViewModel());
    }

    /// <summary>
    ///     Обработка данных из формы регистрации и создание нового пользователя.
    /// </summary>
    /// <param name="model">Модель данных, введенных пользователем для регистрации.</param>
    /// <returns>Переадресация на страницу входа или возвращение на страницу регистрации с ошибками.</returns>
    [HttpPost]
    public async Task<IActionResult> Index(RegistrationViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var existingUser = await userRepository.GetByEmail(model.Email!);

        if (existingUser is not null)
        {
            ModelState.AddModelError("Email", "Пользователь с такой почтой уже существует.");

            return View(model);
        }

        var defaultRole = await userRoleRepository.GetById(1);

        var newUser = new UserEntity
        {
            Name = model.Name!,
            Surname = model.Surname!,
            Email = model.Email!,
            Password = model.Password!,
            Role = defaultRole!
        };

        var saveUser = await userRepository.Save(newUser);

        if (saveUser is null) return RedirectToAction("Index");

        HttpContext.Session.SetInt32("UserId", saveUser.Id);
        HttpContext.Session.SetString("UserEmail", newUser.Email);
        HttpContext.Session.SetString("UserName", newUser.Name);
        HttpContext.Session.SetString("UserSurname", newUser.Surname);
        HttpContext.Session.SetInt32("UserRole", newUser.Role.Id);

        return RedirectToAction("Index", "Profile");
    }
}