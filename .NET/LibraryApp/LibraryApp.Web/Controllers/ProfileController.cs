using LibraryApp.Domain.Entities;
using LibraryApp.Infrastructure.Repositories;
using LibraryApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Controllers;

/// <summary>
///     Контроллер для отображения профиля пользователя.
/// </summary>
/// <param name="bookRepository">Репозиторий для работы с книгами.</param>
[Route("/profile")]
public class ProfileController(IUserRepository userRepository, IBookRepository bookRepository) : Controller
{
    /// <summary>
    ///     Страница профиля пользователя.
    /// </summary>
    /// <returns>Возвращает представление с данными пользователя и его книгами.</returns>
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var userEmail = HttpContext.Session.GetString("UserEmail");

        if (userEmail is null) return RedirectToAction("Index", "Login");

        var userId = HttpContext.Session.GetInt32("UserId");
        var userName = HttpContext.Session.GetString("UserName");
        var userSurname = HttpContext.Session.GetString("UserSurname");
        var user = await userRepository.GetByEmail(userEmail);
        var isAdmin = user?.Role.Id == 2;

        var userBooks = await bookRepository.GetByUserId(userId!.Value);
        var allBooks = await bookRepository.GetAllFree();

        var availableBooks = allBooks.Where(book => userBooks.All(userBook => userBook.Id != book.Id))
            .Select(book => new BookViewModel
            {
                Title = book.Title,
                Author = book.Author
            })
            .ToList();

        var model = new ProfileViewModel
        {
            Name = userName,
            Surname = userSurname,
            Email = userEmail,
            UserBooks = isAdmin
                ? []
                : userBooks.Select(book => new BookViewModel
                {
                    Title = book.Title,
                    Author = book.Author
                }).ToList(),
            AvailableBooks = availableBooks,
            IsAdmin = isAdmin
        };

        return View(model);
    }

    [HttpPost("/add-book")]
    public async Task<IActionResult> AddBook(string bookTitle, string bookAuthor)
    {
        var userEmail = HttpContext.Session.GetString("UserEmail");

        if (userEmail is null) return RedirectToAction("Index", "Login");

        var book = await bookRepository.GetByTitleAndAuthor(bookTitle, bookAuthor);

        if (book is null) return RedirectToAction("Index");

        await userRepository.AddBookToUser(HttpContext.Session.GetInt32("UserId")!.Value, book);

        return RedirectToAction("Index");
    }

    [HttpPost("/add-new-book")]
    public async Task<IActionResult> AddNewBook(string bookTitle, string bookAuthor)
    {
        var userEmail = HttpContext.Session.GetString("UserEmail");

        if (userEmail is null || HttpContext.Session.GetInt32("UserRole") != 2)
            return RedirectToAction("Index", "Login");

        var book = new BookEntity
        {
            Title = bookTitle,
            Author = bookAuthor
        };

        await bookRepository.Save(book);

        return RedirectToAction("Index");
    }

    [HttpPost("/remove-book")]
    public async Task<IActionResult> RemoveBook(string bookTitle, string bookAuthor)
    {
        var userEmail = HttpContext.Session.GetString("UserEmail");

        if (userEmail is null) return RedirectToAction("Index", "Login");

        var book = await bookRepository.GetByTitleAndAuthor(bookTitle, bookAuthor);

        if (book is null) return RedirectToAction("Index");

        await userRepository.RemoveBookFromUser(HttpContext.Session.GetInt32("UserId")!.Value, book);

        return RedirectToAction("Index");
    }

    [HttpPost("/remove-book-from-list")]
    public async Task<IActionResult> RemoveBookFromList(string bookTitle, string bookAuthor)
    {
        var userEmail = HttpContext.Session.GetString("UserEmail");

        if (userEmail is null || HttpContext.Session.GetInt32("UserRole") != 2)
            return RedirectToAction("Index", "Login");

        var book = await bookRepository.GetByTitleAndAuthor(bookTitle, bookAuthor);

        if (book is null) return RedirectToAction("Index");

        await bookRepository.Delete(book);

        return RedirectToAction("Index");
    }

    [HttpGet("/get-book-details")]
    public async Task<IActionResult> GetBookDetails(string bookTitle, string bookAuthor)
    {
        var userEmail = HttpContext.Session.GetString("UserEmail");

        if (userEmail is null || HttpContext.Session.GetInt32("UserRole") != 2)
            return RedirectToAction("Index", "Login");

        var book = await bookRepository.GetByTitleAndAuthor(bookTitle, bookAuthor);

        if (book is null) return NotFound();

        var bookViewModel = new BookViewModel
        {
            Title = book.Title,
            Author = book.Author
        };

        return Json(bookViewModel);
    }

    [HttpPost("/save-book-changes")]
    public async Task<IActionResult> SaveBookChanges(string originalTitle, string originalAuthor, string newTitle,
        string newAuthor)
    {
        var userEmail = HttpContext.Session.GetString("UserEmail");

        if (userEmail is null || HttpContext.Session.GetInt32("UserRole") != 2)
            return RedirectToAction("Index", "Login");

        var book = await bookRepository.GetByTitleAndAuthor(originalTitle, originalAuthor);

        if (book is null) return RedirectToAction("Index");

        book.Title = newTitle;
        book.Author = newAuthor;

        await bookRepository.Save(book);

        return RedirectToAction("Index");
    }

    [HttpPost("/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Login");
    }
}