using System.Diagnostics;
using LibraryApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Controllers;

/// <summary>
///     Контроллер для обработки ошибок в приложении.
/// </summary>
[Route("/error")]
public class ErrorController : Controller
{
    /// <summary>
    ///     Обрабатывать запросы на страницу ошибки.
    /// </summary>
    /// <param name="statusCode">Код состояния HTTP, связанный с ошибкой.</param>
    /// <returns>Представление ошибки с информацией о запросе и коде состояния.</returns>
    [HttpGet]
    public IActionResult Error(int? statusCode)
    {
        var model = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            StatusCode = statusCode
        };

        return View(model);
    }
}