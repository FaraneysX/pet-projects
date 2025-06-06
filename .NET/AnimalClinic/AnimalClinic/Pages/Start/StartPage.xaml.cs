using System.Windows;
using System.Windows.Controls;

using AnimalClinic.DataAccess;
using AnimalClinic.Pages.Panel;
using AnimalClinic.Service;

namespace AnimalClinic.Pages.Start;

public partial class StartPage : Page
{
    public StartPage()
    {
        InitializeComponent();

        // Инициализация очереди сообщений об ошибке
        ErrorNotification.MessageQueue = new();
    }

    // Обработчик события нажатия кнопки подключения
    private void ConnectButton_Click(object sender, RoutedEventArgs e)
    {
        // Отключаем кнопку подключения и показываем индикатор загрузки
        ConnectButton.IsEnabled = false;
        Loading.Visibility = Visibility.Visible;

        // Выполняем асинхронное подключение
        ConnectionAsync();
    }

    // Асинхронный метод для подключения к системе
    private async void ConnectionAsync()
    {
        try
        {
            // Чтение настроек из файла конфигурации и аутентификация пользователя
            Settings.Read(Settings.LoadConfiguration());
            Settings.Authenticate(LoginTextBox.Text, PasswordTextBox.Password);

            // Очистка полей ввода логина и пароля
            LoginTextBox.Text = string.Empty;
            PasswordTextBox.Password = string.Empty;

            // Если пользователь успешно аутентифицирован
            if (Settings.Account != Account.None)
            {
                // Асинхронная синхронизация с базой данных
                await Task.Run(async () =>
                {
                    await using var dbContext = new AnimalClinicDbContext();

                    await dbContext.SynchronizationAsync();
                });

                // Переход на страницу панели управления
                NavigateToPanelPage();
            }
            else
            {
                // Вывод сообщения об ошибке при неверном имени пользователя или пароле
                ShowErrorMessage("Неверное имя пользователя или пароль.");
            }
        }
        catch (Exception ex)
        {
            // Вывод сообщения об ошибке при возникновении исключения
            ShowErrorMessage(ex.Message);
        }
    }

    // Метод для перехода на страницу панели управления
    private void NavigateToPanelPage()
    {
        NavigationService.Navigate(new PanelPage());
    }

    // Метод для отображения сообщения об ошибке
    private void ShowErrorMessage(string message)
    {
        ErrorNotification?.MessageQueue?.Enqueue(message);
        Loading.Visibility = Visibility.Collapsed;

        // Включаем кнопку подключения
        ConnectButton.IsEnabled = true;
    }

    // Обработчик события нажатия кнопки закрытия приложения
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        // Открытие окна подтверждения выхода из приложения
        var exitWindow = new ExitWindow();

        exitWindow.ShowDialog();
    }
}
