using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

using AnimalClinic.DataAccess;

namespace AnimalClinic.Pages.Panel.Views;

public partial class ChangingClientDataButtons : Page
{
    private string _id = string.Empty;
    private string _name = string.Empty;
    private string _surname = string.Empty;
    private string _patronymic = string.Empty;
    private DateOnly? _birthDate = null;
    private string _email = string.Empty;
    private string _phoneNumber = string.Empty;

    public ChangingClientDataButtons()
    {
        InitializeComponent();

        Notification.MessageQueue = new();
    }

    protected override async void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);

        InitializeDataGrid();

        DatabaseView.Database.SelectionChanged += DatabaseView_SelectionChanged;

        Unloaded += (sender, e) =>
        {
            DatabaseView.Database.SelectionChanged -= DatabaseView_SelectionChanged;
        };

        await Task.Run(ShowData);
    }

    private void DatabaseView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = DatabaseView.Database.SelectedItem;

        if (selectedItem != null)
        {
            IdTextBox.IsEnabled = false;

            var item = selectedItem;

            IdTextBox.Text = item?.GetType().GetProperty("ClientId")?.GetValue(item, null)?.ToString();
            NameTextBox.Text = item?.GetType().GetProperty("ClientName")?.GetValue(item, null) as string;
            SurnameTextBox.Text = item?.GetType().GetProperty("ClientSurname")?.GetValue(item, null) as string;
            PatronymicTextBox.Text = item?.GetType().GetProperty("ClientPatronymic")?.GetValue(item, null) as string;
            EmailTextBox.Text = item?.GetType().GetProperty("ClientEmail")?.GetValue(item, null) as string;
            PhoneNumberTextBox.Text = item?.GetType().GetProperty("ClientPhoneNumber")?.GetValue(item, null) as string;

            DateOnly? dateOnly = item?.GetType().GetProperty("ClientBirthDate")?.GetValue(item, null) as DateOnly?;

            if (dateOnly is not null)
            {
                BirthDatePicker.SelectedDate = dateOnly.Value.ToDateTime(TimeOnly.MinValue);
            }

            Unsubscribe();
        }
    }

    private async void IdTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        _id = IdTextBox.Text;

        await Task.Run(ShowData);
    }

    private async void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        _name = NameTextBox.Text;

        await Task.Run(ShowData);
    }

    private async void SurnameTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        _surname = SurnameTextBox.Text;

        await Task.Run(ShowData);
    }

    private async void PatronymicTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        _patronymic = PatronymicTextBox.Text;

        await Task.Run(ShowData);
    }

    private async void BirthDatePicker_SelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        _birthDate = DateOnly.FromDateTime(BirthDatePicker.SelectedDate.GetValueOrDefault());

        await Task.Run(ShowData);
    }

    private async void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        _email = EmailTextBox.Text;

        await Task.Run(ShowData);
    }

    private async void PhoneNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        _phoneNumber = PhoneNumberTextBox.Text;

        await Task.Run(ShowData);
    }

    private void ChangeDataButton_Click(object sender, RoutedEventArgs e)
    {
        InitializeFields();

        if (!IsValidated())
        {
            return;
        }

        SaveClient();
    }

    private async void ResetButton_Click(object sender, RoutedEventArgs e)
    {
        Subscribe();
        ClearInputFields();

        _id = string.Empty;
        _name = string.Empty;
        _surname = string.Empty;
        _patronymic = string.Empty;
        _birthDate = null;
        _email = string.Empty;
        _phoneNumber = string.Empty;

        await Task.Run(ShowData);
    }

    private static void InitializeDataGrid()
    {
        DatabaseView.Reset();

        DatabaseView.AddColumn("ID клиента", "ClientId");
        DatabaseView.AddColumn("Имя", "ClientName");
        DatabaseView.AddColumn("Фамилия", "ClientSurname");
        DatabaseView.AddColumn("Отчество", "ClientPatronymic");
        DatabaseView.AddColumn("Дата рождения", "ClientBirthDate");
        DatabaseView.AddColumn("Электронная почта", "ClientEmail");
        DatabaseView.AddColumn("Номер телефона", "ClientPhoneNumber");
        DatabaseView.AddColumn("ID питомца", "AnimalId");
    }

    private async void SaveClient()
    {
        InitializeFields();

        await using var context = new AnimalClinicDbContext();

        var client = context.Clients?.FirstOrDefault(client => client.Id == int.Parse(_id));

        if (client is not null)
        {
            client.Name = _name;
            client.Surname = _surname;
            client.Patronymic = _patronymic;
            client.BirthDate = _birthDate;
            client.Email = _email;
            client.PhoneNumber = _phoneNumber;

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
        else
        {
            Notification?.MessageQueue?.Enqueue("Клиент с таким ID не найден.");
        }
    }

    private async Task ShowData()
    {
        Dispatcher.Invoke(InitializeDataGrid);

        await using var context = new AnimalClinicDbContext();

        if (context.Animals is not null)
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

            if (int.TryParse(_id, out int id))
            {
                query = query.Where(item => item.ClientId == id);
            }

            if (!string.IsNullOrWhiteSpace(_name))
            {
                query = query.Where(item => item.ClientName.StartsWith(_name));
            }

            if (!string.IsNullOrWhiteSpace(_surname))
            {
                query = query.Where(item => item.ClientSurname.StartsWith(_surname));
            }

            if (!string.IsNullOrWhiteSpace(_patronymic))
            {
                query = query.Where(item => item.ClientPatronymic.StartsWith(_patronymic));
            }

            if (!string.IsNullOrWhiteSpace(_email))
            {
                query = query.Where(item => item.ClientEmail.StartsWith(_email));
            }

            if (_birthDate.HasValue)
            {
                query = query.Where(item => item.ClientBirthDate == _birthDate.Value);
            }

            if (!string.IsNullOrWhiteSpace(_phoneNumber))
            {
                query = query.Where(item => item.ClientPhoneNumber.StartsWith(_phoneNumber));
            }

            if (query.Any())
            {
                Dispatcher.Invoke(() =>
                {
                    DatabaseView.Database.ItemsSource = query.ToList();
                });
            }
        }
    }

    private bool IsValidated()
    {
        if (string.IsNullOrEmpty(_id) ||
            string.IsNullOrWhiteSpace(_name) ||
            string.IsNullOrWhiteSpace(_surname) ||
            _birthDate is null ||
            string.IsNullOrWhiteSpace(_phoneNumber) ||
            _id is null)
        {
            Notification?.MessageQueue?.Enqueue("Обязательные поля должны быть заполнены.");

            return false;
        }

        if (!int.TryParse(_id, out int id))
        {
            Notification?.MessageQueue?.Enqueue("ID должен содержать только цифры");

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

    private void Subscribe()
    {
        IdTextBox.TextChanged += IdTextBox_TextChanged;
        NameTextBox.TextChanged += NameTextBox_TextChanged;
        SurnameTextBox.TextChanged += SurnameTextBox_TextChanged;
        PatronymicTextBox.TextChanged += PatronymicTextBox_TextChanged;
        BirthDatePicker.SelectedDateChanged += BirthDatePicker_SelectedDateChanged;
        EmailTextBox.TextChanged += EmailTextBox_TextChanged;
        PhoneNumberTextBox.TextChanged += PhoneNumberTextBox_TextChanged;
    }

    private void Unsubscribe()
    {
        IdTextBox.TextChanged -= IdTextBox_TextChanged;
        NameTextBox.TextChanged -= NameTextBox_TextChanged;
        SurnameTextBox.TextChanged -= SurnameTextBox_TextChanged;
        PatronymicTextBox.TextChanged -= PatronymicTextBox_TextChanged;
        BirthDatePicker.SelectedDateChanged -= BirthDatePicker_SelectedDateChanged;
        EmailTextBox.TextChanged -= EmailTextBox_TextChanged;
        PhoneNumberTextBox.TextChanged -= PhoneNumberTextBox_TextChanged;
    }

    private void InitializeFields()
    {
        _id = IdTextBox.Text;
        _name = NameTextBox.Text;
        _surname = SurnameTextBox.Text;
        _patronymic = PatronymicTextBox.Text;
        _birthDate = DateOnly.FromDateTime(BirthDatePicker.SelectedDate.GetValueOrDefault());
        _email = EmailTextBox.Text;
        _phoneNumber = PhoneNumberTextBox.Text;
    }

    private void ClearInputFields()
    {
        IdTextBox.IsEnabled = true;

        IdTextBox.Text = string.Empty;
        NameTextBox.Text = string.Empty;
        SurnameTextBox.Text = string.Empty;
        PatronymicTextBox.Text = string.Empty;
        BirthDatePicker.SelectedDate = null;
        EmailTextBox.Text = string.Empty;
        PhoneNumberTextBox.Text = string.Empty;
    }
}
