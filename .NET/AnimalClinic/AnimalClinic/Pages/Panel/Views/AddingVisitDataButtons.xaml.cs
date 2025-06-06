using System.Windows;
using System.Windows.Controls;

using AnimalClinic.DataAccess;
using AnimalClinic.DataAccess.Entities;

namespace AnimalClinic.Pages.Panel.Views;

public partial class AddingVisitDataButtons : Page
{
    private string _clientId = string.Empty;
    private string _office = string.Empty;
    private DateOnly? _date = null;
    private TimeOnly? _time = null;

    public AddingVisitDataButtons()
    {
        InitializeComponent();

        Notification.MessageQueue = new();
    }

    protected override async void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);

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
        DatabaseView.AddColumn("Номер телефона", "ClientPhoneNumber");
        DatabaseView.AddColumn("ID посещения", "VisitId");
        DatabaseView.AddColumn("Офис", "VisitOffice");
        DatabaseView.AddColumn("Дата посещения", "VisitDate");
        DatabaseView.AddColumn("Время посещения", "VisitTime");
    }

    private void AddDataButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        InitializeFields();
        InitializeDataGrid();

        if (IsValidated())
        {
            AddVisit();
        }
    }

    private async void AddVisit()
    {
        await using var context = new AnimalClinicDbContext();

        var client = context.Clients?.FirstOrDefault(client => client.Id == int.Parse(_clientId));

        if (client is not null)
        {
            VisitEntity visit = new()
            {
                Client = client,
                Office = _office,
                Date = _date,
                Time = _time,
            };

            context.Visits?.Add(visit);

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
        Dispatcher.Invoke(() =>
        {
            InitializeDataGrid();
        });

        await using var context = new AnimalClinicDbContext();

        if (context.Visits is not null)
        {
            var query = from client in context.Clients
                        join visit in context.Visits on client.Id equals visit.Client.Id into clientVisits
                        from visit in clientVisits.DefaultIfEmpty()
                        select new
                        {
                            ClientId = client.Id,
                            ClientName = client.Name,
                            ClientSurname = client.Surname,
                            ClientPatronymic = client.Patronymic,
                            ClientPhoneNumber = client.PhoneNumber,
                            VisitId = visit.Id,
                            VisitOffice = visit.Office,
                            VisitDate = visit.Date,
                            VisitTime = visit.Time,
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
        if (string.IsNullOrWhiteSpace(_clientId) ||
            string.IsNullOrWhiteSpace(_office) ||
            _date is null ||
            _time is null)
        {
            Notification?.MessageQueue?.Enqueue("Обязательные поля должны быть заполнены.");

            return false;
        }

        if (!_clientId.All(char.IsDigit))
        {
            Notification?.MessageQueue?.Enqueue("ID клиента должен содержать только цифры.");

            return false;
        }

        if (_date < DateOnly.FromDateTime(DateTime.Today) && _time < TimeOnly.FromDateTime(DateTime.Now))
        {
            Notification?.MessageQueue?.Enqueue("Дата и время записи не должны быть раньше текущего момента.");

            return false;
        }

        return true;
    }

    private void InitializeFields()
    {
        _clientId = ClientIdTextBox.Text;
        _office = OfficeTextBox.Text;

        if (VisitDatePicker.SelectedDate is not null)
        {
            _date = DateOnly.FromDateTime(VisitDatePicker.SelectedDate.Value);
        }

        if (VisitTimePicker.SelectedTime is not null)
        {
            _time = TimeOnly.FromDateTime(VisitTimePicker.SelectedTime.Value);
        }
    }

    private void ClearInputFields()
    {
        ClientIdTextBox.Text = string.Empty;
        OfficeTextBox.Text = string.Empty;
        VisitDatePicker.SelectedDate = null;
        VisitTimePicker.SelectedTime = null;
    }
}
