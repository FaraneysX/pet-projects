using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

using AnimalClinic.DataAccess;
using AnimalClinic.DataAccess.Entities;

namespace AnimalClinic.Pages.Panel.Views;

public partial class AddingClientDataButtons : Page
{
    private string _name = string.Empty;
    private string _surname = string.Empty;
    private string _patronymic = string.Empty;
    private DateOnly? _birthDate = null;
    private string _email = string.Empty;
    private string _phoneNumber = string.Empty;

    public AddingClientDataButtons()
    {
        InitializeComponent();

        Notification.MessageQueue = new();
    }

    protected override async void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);

        InitializeDataGrid();

        await Task.Run(ShowData);
    }

    private static void InitializeDataGrid()
    {
        DatabaseView.Database.Visibility = Visibility.Visible;
        DatabaseView.Database.Columns.Clear();
        DatabaseView.Database.ItemsSource = null;

        DatabaseView.AddColumn("ID клиента", "ClientId");
        DatabaseView.AddColumn("Имя", "ClientName");
        DatabaseView.AddColumn("Фамилия", "ClientSurname");
        DatabaseView.AddColumn("Отчество", "ClientPatronymic");
        DatabaseView.AddColumn("Дата рождения", "ClientBirthDate");
        DatabaseView.AddColumn("Электронная почта", "ClientEmail");
        DatabaseView.AddColumn("Номер телефона", "ClientPhoneNumber");
        DatabaseView.AddColumn("ID питомца", "AnimalId");
    }

    private void AddDataButton_Click(object sender, RoutedEventArgs e)
    {
        InitializeFields();

        if (IsValidated())
        {
            AddClient();
        }
    }

    private async void AddClient()
    {
        await using var context = new AnimalClinicDbContext();

        ClientEntity client = new()
        {
            Name = _name,
            Surname = _surname,
            Patronymic = _patronymic,
            BirthDate = _birthDate,
            Email = _email,
            PhoneNumber = _phoneNumber,
        };

        context.Clients?.Add(client);

        try
        {
            await Task.Run(async () =>
            {
                await context.SaveChangesAsync();
            });

            ClearInputFields();

            Notification?.MessageQueue?.Enqueue("Данные успешно сохранены.");

            await ShowData();
        }
        catch (Exception ex)
        {
            Notification?.MessageQueue?.Enqueue($"Ошибка сохранения данных: {ex.Message}");
        }
    }

    private async Task ShowData()
    {
        await using var context = new AnimalClinicDbContext();

        if (context.Animals is not null && context.Visits is not null)
        {
            var query = from client in context.Clients
                        join animal in context.Animals on client.Id equals animal.Client.Id into clientAnimals
                        from animal in clientAnimals.DefaultIfEmpty()
                        select new
                        {
                            ClientId = client.Id,
                            ClientName = client.Name,
                            ClientSurname = client.Surname,
                            ClientPatronymic = client.Patronymic,
                            ClientBirthDate = client.BirthDate,
                            ClientEmail = client.Email,
                            ClientPhoneNumber = client.PhoneNumber,
                            AnimalId = animal.Id,
                        };

            if (!query.Any())
            {
                return;
            }

            Dispatcher.Invoke(() =>
            {
                DatabaseView.Database.ItemsSource = query.ToList();
            });
        }
    }

    private bool IsValidated()
    {
        if (string.IsNullOrWhiteSpace(_name) ||
            string.IsNullOrWhiteSpace(_surname) ||
            _birthDate is null ||
            string.IsNullOrWhiteSpace(_phoneNumber))
        {
            Notification?.MessageQueue?.Enqueue("Обязательные поля должны быть заполнены.");

            return false;
        }

        if (!_name.All(c => char.IsLetter(c) || c == ' ') ||
            !_surname.All(c => char.IsLetter(c) || c == ' '))
        {
            Notification?.MessageQueue?.Enqueue("Имя и фамилия должны содержать только буквы.");

            return false;
        }

        if (!string.IsNullOrWhiteSpace(_patronymic) && !_patronymic.All(c => char.IsLetter(c) || c == ' '))
        {
            Notification?.MessageQueue?.Enqueue("Отчество должно содержать только буквы.");

            return false;
        }

        if (_birthDate > DateOnly.FromDateTime(DateTime.Today))
        {
            Notification?.MessageQueue?.Enqueue("Дата рождения не должна быть позже текущей.");

            return false;
        }

        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        if (!string.IsNullOrWhiteSpace(_email) && !Regex.IsMatch(_email, emailPattern))
        {
            Notification?.MessageQueue?.Enqueue("Почта введена неверно.");

            return false;
        }

        string phoneNumberPattern = @"^\d{1}\d{3}\d{3}\d{2}\d{2}$";

        if (!Regex.IsMatch(_phoneNumber, phoneNumberPattern))
        {
            Notification?.MessageQueue?.Enqueue("Номер телефона введен неверно.");

            return false;
        }

        return true;
    }

    private void InitializeFields()
    {
        _name = NameTextBox.Text;
        _surname = SurnameTextBox.Text;
        _patronymic = PatronymicTextBox.Text;
        _birthDate = DateOnly.FromDateTime(BirthDatePicker.SelectedDate.GetValueOrDefault());
        _email = EmailTextBox.Text;
        _phoneNumber = PhoneNumberTextBox.Text;
    }

    private void ClearInputFields()
    {
        NameTextBox.Text = string.Empty;
        SurnameTextBox.Text = string.Empty;
        PatronymicTextBox.Text = string.Empty;
        BirthDatePicker.SelectedDate = null;
        EmailTextBox.Text = string.Empty;
        PhoneNumberTextBox.Text = string.Empty;
    }
}
