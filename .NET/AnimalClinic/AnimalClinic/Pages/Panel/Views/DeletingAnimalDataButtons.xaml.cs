using System.Windows;
using System.Windows.Controls;

using AnimalClinic.DataAccess;

namespace AnimalClinic.Pages.Panel.Views;

public partial class DeletingAnimalDataButtons : Page
{
    private string _id = string.Empty;
    private string _clientId = string.Empty;
    private string _nickname = string.Empty;
    private string _kind = string.Empty;
    private string _breed = string.Empty;

    public DeletingAnimalDataButtons()
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

        if (selectedItem is not null)
        {
            IdTextBox.IsEnabled = false;
            ClientIdTextBox.IsEnabled = false;
            NicknameTextBox.IsEnabled = false;
            KindTextBox.IsEnabled = false;
            BreedTextBox.IsEnabled = false;

            var item = selectedItem;

            IdTextBox.Text = item?.GetType().GetProperty("AnimalId")?.GetValue(item, null)?.ToString();
            ClientIdTextBox.Text = item?.GetType().GetProperty("ClientId")?.GetValue(item, null)?.ToString();
            NicknameTextBox.Text = item?.GetType().GetProperty("AnimalNickname")?.GetValue(item, null) as string;
            KindTextBox.Text = item?.GetType().GetProperty("AnimalKind")?.GetValue(item, null) as string;
            BreedTextBox.Text = item?.GetType().GetProperty("AnimalBreed")?.GetValue(item, null) as string;

            Unsubscribe();
        }
    }

    private async void IdTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        _id = IdTextBox.Text;

        await Task.Run(ShowData);
    }

    private async void ClientIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        _clientId = ClientIdTextBox.Text;

        await Task.Run(ShowData);
    }

    private async void NicknameTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        _nickname = NicknameTextBox.Text;

        await Task.Run(ShowData);
    }

    private async void KindTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        _kind = KindTextBox.Text;

        await Task.Run(ShowData);
    }

    private async void BreedTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        _breed = BreedTextBox.Text;

        await Task.Run(ShowData);
    }

    private void DeleteDataButton_Click(object sender, RoutedEventArgs e)
    {
        if (!IsValidated())
        {
            return;
        }

        DeleteAnimal();
    }

    private async void DeleteAnimal()
    {
        InitializeFields();

        await using var context = new AnimalClinicDbContext();

        var animal = context.Animals?.FirstOrDefault(animal => animal.Id == int.Parse(_id) && animal.Client.Id == int.Parse(_clientId));

        if (animal is not null)
        {
            context.Animals?.Remove(animal);

            try
            {
                await Task.Run(async () =>
                {
                    await context.SaveChangesAsync();
                });

                ClearInputFields();

                Notification?.MessageQueue?.Enqueue("Данные успешно удалены.");

                await ShowData();
            }
            catch (Exception ex)
            {
                Notification?.MessageQueue?.Enqueue($"Ошибка сохранения данных: {ex.Message}");
            }
        }
        else
        {
            Notification?.MessageQueue?.Enqueue("Животное с таким ID не найден.");

            return;
        }
    }

    private async void ResetButton_Click(object sender, RoutedEventArgs e)
    {
        Subscribe();
        ClearInputFields();

        _id = string.Empty;
        _clientId = string.Empty;
        _nickname = string.Empty;
        _kind = string.Empty;
        _breed = string.Empty;

        await Task.Run(ShowData);
    }

    private static void InitializeDataGrid()
    {
        DatabaseView.Reset();

        DatabaseView.AddColumn("ID животного", "AnimalId");
        DatabaseView.AddColumn("ID клиента", "ClientId");
        DatabaseView.AddColumn("Кличка", "AnimalNickname");
        DatabaseView.AddColumn("Вид", "AnimalKind");
        DatabaseView.AddColumn("Порода", "AnimalBreed");
    }

    private async Task ShowData()
    {
        Dispatcher.Invoke(InitializeDataGrid);

        await using var context = new AnimalClinicDbContext();

        if (context.Clients is not null)
        {
            var query = from animal in context.Animals
                        join client in context.Clients on animal.Client.Id equals client.Id into clientAnimals
                        from client in clientAnimals.DefaultIfEmpty()
                        select new
                        {
                            AnimalId = animal.Id,
                            ClientId = client.Id,
                            AnimalNickname = animal.Nickname,
                            AnimalKind = animal.Kind,
                            AnimalBreed = animal.Breed,
                        };

            if (int.TryParse(_id, out int animalId))
            {
                query = query.Where(item => item.AnimalId == animalId);
            }

            if (int.TryParse(_clientId, out int clientId))
            {
                query = query.Where(item => item.ClientId == clientId);
            }

            if (!string.IsNullOrWhiteSpace(_nickname))
            {
                query = query.Where(item => item.AnimalNickname.StartsWith(_nickname));
            }

            if (!string.IsNullOrWhiteSpace(_kind))
            {
                query = query.Where(item => item.AnimalKind.StartsWith(_kind));
            }

            if (!string.IsNullOrWhiteSpace(_breed))
            {
                query = query.Where(item => item.AnimalBreed.StartsWith(_breed));
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
        if (!AreRequiredFieldsFilled())
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
        if (string.IsNullOrEmpty(_id) ||
            string.IsNullOrWhiteSpace(_nickname) ||
            string.IsNullOrWhiteSpace(_kind) ||
            string.IsNullOrWhiteSpace(_breed))
        {
            Notification?.MessageQueue?.Enqueue("Обязательные поля должны быть заполнены.");

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

    private void Subscribe()
    {
        IdTextBox.TextChanged += IdTextBox_TextChanged;
        ClientIdTextBox.TextChanged += ClientIdTextBox_TextChanged;
        NicknameTextBox.TextChanged += NicknameTextBox_TextChanged;
        KindTextBox.TextChanged += KindTextBox_TextChanged;
        BreedTextBox.TextChanged += BreedTextBox_TextChanged;
    }

    private void Unsubscribe()
    {
        IdTextBox.TextChanged -= IdTextBox_TextChanged;
        ClientIdTextBox.TextChanged -= ClientIdTextBox_TextChanged;
        NicknameTextBox.TextChanged -= NicknameTextBox_TextChanged;
        KindTextBox.TextChanged -= KindTextBox_TextChanged;
        BreedTextBox.TextChanged -= BreedTextBox_TextChanged;
    }

    private void InitializeFields()
    {
        _id = IdTextBox.Text;
        _clientId = ClientIdTextBox.Text;
        _nickname = NicknameTextBox.Text;
        _kind = KindTextBox.Text;
        _breed = BreedTextBox.Text;
    }

    private void ClearInputFields()
    {
        IdTextBox.IsEnabled = true;
        ClientIdTextBox.IsEnabled = true;
        NicknameTextBox.IsEnabled = true;
        KindTextBox.IsEnabled = true;
        BreedTextBox.IsEnabled = true;

        IdTextBox.Text = string.Empty;
        ClientIdTextBox.Text = string.Empty;
        NicknameTextBox.Text = string.Empty;
        KindTextBox.Text = string.Empty;
        BreedTextBox.Text = string.Empty;
    }
}
