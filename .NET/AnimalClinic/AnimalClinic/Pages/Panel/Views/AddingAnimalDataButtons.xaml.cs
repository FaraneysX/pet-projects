using System.Windows.Controls;

using AnimalClinic.DataAccess;
using AnimalClinic.DataAccess.Entities;

namespace AnimalClinic.Pages.Panel.Views;

public partial class AddingAnimalDataButtons : Page
{
    private string _clientId = string.Empty;
    private string _nickname = string.Empty;
    private string _kind = string.Empty;
    private string _breed = string.Empty;

    public AddingAnimalDataButtons()
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
        DatabaseView.Reset();

        DatabaseView.AddColumn("ID клиента", "ClientId");
        DatabaseView.AddColumn("Имя", "ClientName");
        DatabaseView.AddColumn("Фамилия", "ClientSurname");
        DatabaseView.AddColumn("Отчество", "ClientPatronymic");
        DatabaseView.AddColumn("Номер телефона", "ClientPhoneNumber");
        DatabaseView.AddColumn("ID питомца", "AnimalId");
        DatabaseView.AddColumn("Кличка питомца", "AnimalNickname");
        DatabaseView.AddColumn("Вид питомца", "AnimalKind");
        DatabaseView.AddColumn("Порода питомца", "AnimalBreed");
    }

    private void AddDataButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        InitializeFields();

        if (IsValidated())
        {
            AddAnimal();
        }
    }

    private async void AddAnimal()
    {
        await using var context = new AnimalClinicDbContext();

        var client = context.Clients?.FirstOrDefault(client => client.Id == int.Parse(_clientId));

        if (client is not null)
        {
            AnimalEntity animal = new()
            {
                Client = client,
                Nickname = _nickname,
                Kind = _kind,
                Breed = _breed,
            };

            context.Animals?.Add(animal);

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
    }

    private async Task ShowData()
    {
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
                            ClientPhoneNumber = client.PhoneNumber,
                            AnimalId = animal.Id,
                            AnimalNickname = animal.Nickname,
                            AnimalKind = animal.Kind,
                            AnimalBreed = animal.Breed,
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
        if (!AreRequiredFieldsFilled())
        {
            return false;
        }

        if (!IsClientIdValid())
        {
            return false;
        }

        if (!AreFieldsContainingOnlyLetters())
        {
            return false;
        }

        return true;
    }

    private bool AreRequiredFieldsFilled()
    {
        if (string.IsNullOrWhiteSpace(_clientId) ||
            string.IsNullOrWhiteSpace(_nickname) ||
            string.IsNullOrWhiteSpace(_kind) ||
            string.IsNullOrWhiteSpace(_breed))
        {
            Notification?.MessageQueue?.Enqueue("Обязательные поля должны быть заполнены.");

            return false;
        }

        return true;
    }

    private bool IsClientIdValid()
    {
        if (!_clientId.All(char.IsDigit))
        {
            Notification?.MessageQueue?.Enqueue("ID клиента должен содержать только цифры.");

            return false;
        }

        return true;
    }

    private bool AreFieldsContainingOnlyLetters()
    {
        if (!string.IsNullOrWhiteSpace(_nickname) && !_nickname.All(c => char.IsLetter(c) || c == ' ') ||
            !string.IsNullOrWhiteSpace(_breed) && !_breed.All(c => char.IsLetter(c) || c == ' ') ||
            !string.IsNullOrWhiteSpace(_kind) && !_kind.All(c => char.IsLetter(c) || c == ' '))
        {
            Notification?.MessageQueue?.Enqueue("Кличка, вид и порода должны содержать только буквы.");

            return false;
        }

        return true;
    }

    private void InitializeFields()
    {
        _clientId = ClientIdTextBox.Text;
        _nickname = NicknameTextBox.Text;
        _kind = KindTextBox.Text;
        _breed = BreedTextBox.Text;
    }

    private void ClearInputFields()
    {
        ClientIdTextBox.Text = string.Empty;
        NicknameTextBox.Text = string.Empty;
        KindTextBox.Text = null;
        BreedTextBox.Text = string.Empty;
    }
}
