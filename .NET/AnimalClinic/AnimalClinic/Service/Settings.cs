using Microsoft.Extensions.Configuration;

namespace AnimalClinic.Service;

public enum Account
{
    None,
    User,
    Admin
}

// Статический класс для управления настройками приложения
public static class Settings
{
    // Строка подключения к базе данных
    public static string? ConnectionString { get; set; }

    // Словарь для хранения учетных данных (имя пользователя и пароль)
    public static Dictionary<string, string> Credentials { get; set; } = [];

    public static Account Account { get; set; } = Account.None;

    // Метод для чтения настроек из IConfiguration
    public static void Read(IConfiguration configuration)
    {
        // Получаем значение строки подключения из конфигурации
        ConnectionString = configuration.GetValue<string>("ConnectionString");

        Credentials.Clear();

        // Проверяем наличие строки подключения
        if (string.IsNullOrEmpty(ConnectionString))
        {
            throw new InvalidOperationException("Отсутствует строка подключения к серверу (ConnectionString).");
        }

        // Получаем раздел "Credentials" из конфигурации и заполняем словарь учетных данных
        var credentialsSection = configuration.GetSection("Credentials");

        foreach (var credential in credentialsSection.GetChildren())
        {
            var username = credential.Key;
            var password = credential.Value;

            // Проверяем наличие корректных учетных данных
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                Credentials.Add(username, password);
            }
            else
            {
                throw new InvalidOperationException("Некорректные учетные данные.");
            }
        }
    }

    // Метод для загрузки конфигурации из файла appsettings.json
    public static IConfiguration LoadConfiguration()
    {
        // Создаем новый объект ConfigurationBuilder и загружаем конфигурацию из файла appsettings.json
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        return configuration;
    }

    // Метод для проверки учетных данных и возвращения соответствующей роли
    public static void Authenticate(string username, string password)
    {
        // Проверяем, существует ли указанное имя пользователя в словаре учетных данных
        if (Credentials.TryGetValue(username, out var expectedPassword))
        {
            // Если пароль совпадает с ожидаемым паролем для данного пользователя
            if (password == expectedPassword)
            {
                // Проверяем, является ли пользователь администратором
                if (username == "admin")
                {
                    Account = Account.Admin;

                    return;
                }
                else
                {
                    Account = Account.User;

                    return;
                }
            }
        }

        // Если имя пользователя или пароль неверны, устанавливаем роль пользователя как None
        Account = Account.None;
    }
}
