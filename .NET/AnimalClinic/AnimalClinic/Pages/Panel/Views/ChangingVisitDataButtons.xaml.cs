using System.Windows;
using System.Windows.Controls;

using AnimalClinic.DataAccess;

namespace AnimalClinic.Pages.Panel.Views;

public partial class ChangingVisitDataButtons : Page
{
    private string _id = string.Empty;
    private string _clientId = string.Empty;
    private string _office = string.Empty;
    private DateOnly? _date = null;
    private TimeOnly? _time = null;

    public ChangingVisitDataButtons()
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
            Unsubscribe();

            IdTextBox.IsEnabled = false;
            ClientIdTextBox.IsEnabled = false;

            var item = selectedItem;

            IdTextBox.Text = item?.GetType().GetProperty("VisitId")?.GetValue(item, null)?.ToString();
            ClientIdTextBox.Text = item?.GetType().GetProperty("ClientId")?.GetValue(item, null)?.ToString();
            OfficeTextBox.Text = item?.GetType().GetProperty("VisitOffice")?.GetValue(item, null)?.ToString();

            DateOnly? dateOnly = item?.GetType().GetProperty("VisitDate")?.GetValue(item, null) as DateOnly?;
            TimeOnly? timeOnly = item?.GetType().GetProperty("VisitTime")?.GetValue(item, null) as TimeOnly?;

            if (timeOnly is not null && dateOnly is not null)
            {
                VisitDatePicker.SelectedDate = dateOnly.Value.ToDateTime(timeOnly.Value);
                VisitTimePicker.SelectedTime = dateOnly.Value.ToDateTime(timeOnly.Value);
            }
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

    private async void OfficeTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        _office = OfficeTextBox.Text;

        await Task.Run(ShowData);
    }

    private async void VisitDatePicker_SelectedDateChanged(object? sender, SelectionChangedEventArgs e)
    {
        _date = DateOnly.FromDateTime(VisitDatePicker.SelectedDate.GetValueOrDefault());

        await Task.Run(ShowData);
    }

    private async void VisitTimePicker_SelectedTimeChanged(object? sender, RoutedPropertyChangedEventArgs<DateTime?> e)
    {
        _time = TimeOnly.FromDateTime(VisitTimePicker.SelectedTime.GetValueOrDefault());

        await Task.Run(ShowData);
    }

    private void ChangeDataButton_Click(object sender, RoutedEventArgs e)
    {
        InitializeFields();

        if (!IsValidated())
        {
            return;
        }

        SaveVisit();
    }

    private async void ResetButton_Click(object sender, RoutedEventArgs e)
    {
        Subscribe();
        ClearInputFields();

        _id = string.Empty;
        _clientId = string.Empty;
        _office = string.Empty;
        _date = null;
        _time = null;

        await Task.Run(ShowData);
    }

    private static void InitializeDataGrid()
    {
        DatabaseView.Reset();

        DatabaseView.AddColumn("ID", "VisitId");
        DatabaseView.AddColumn("ID клиента", "ClientId");
        DatabaseView.AddColumn("Офис", "VisitOffice");
        DatabaseView.AddColumn("Дата", "VisitDate");
        DatabaseView.AddColumn("Время", "VisitTime");
    }

    private async void SaveVisit()
    {
        InitializeFields();

        await using var context = new AnimalClinicDbContext();

        var visit = context.Visits?.FirstOrDefault(visit => visit.Id == int.Parse(_id) && visit.Client.Id == int.Parse(_clientId));

        if (visit is not null)
        {
            visit.Office = _office;
            visit.Date = _date;
            visit.Time = _time;

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
            Notification?.MessageQueue?.Enqueue("Посещение с таким ID не найден.");

            return;
        }
    }

    private async Task ShowData()
    {
        Dispatcher.Invoke(InitializeDataGrid);

        await using var context = new AnimalClinicDbContext();

        if (context.Clients is not null && context.Animals is not null)
        {
            var query = from visit in context.Visits
                        join client in context.Clients on visit.Client.Id equals client.Id
                        select new
                        {
                            VisitId = visit.Id,
                            ClientId = client.Id,
                            VisitOffice = visit.Office,
                            VisitDate = visit.Date,
                            VisitTime = visit.Time,
                        };

            if (int.TryParse(_id, out int visitId))
            {
                query = query.Where(item => item.VisitId == visitId);
            }

            if (int.TryParse(_clientId, out int clientId))
            {
                query = query.Where(item => item.ClientId == clientId);
            }

            if (!string.IsNullOrWhiteSpace(_office))
            {
                query = query.Where(item => item.VisitOffice.StartsWith(_office));
            }

            if (_date.HasValue)
            {
                query = query.Where(item => item.VisitDate == _date.Value);
            }

            if (_time.HasValue)
            {
                query = query.Where(item => item.VisitTime == _time.Value);
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
        if (string.IsNullOrWhiteSpace(_id) ||
            string.IsNullOrWhiteSpace(_office) ||
            _date is null ||
            _time is null)
        {
            Notification?.MessageQueue?.Enqueue("Обязательные поля должны быть заполнены.");

            return false;
        }

        if (_date < DateOnly.FromDateTime(DateTime.Today) && _time < TimeOnly.FromDateTime(DateTime.Now))
        {
            Notification?.MessageQueue?.Enqueue("Дата и время записи не должны быть раньше текущего момента.");

            return false;
        }

        return true;
    }

    private void Subscribe()
    {
        IdTextBox.TextChanged += IdTextBox_TextChanged;
        ClientIdTextBox.TextChanged += ClientIdTextBox_TextChanged;
        OfficeTextBox.TextChanged += OfficeTextBox_TextChanged;
        VisitDatePicker.SelectedDateChanged += VisitDatePicker_SelectedDateChanged;
        VisitTimePicker.SelectedTimeChanged += VisitTimePicker_SelectedTimeChanged;
    }

    private void Unsubscribe()
    {
        IdTextBox.TextChanged -= IdTextBox_TextChanged;
        ClientIdTextBox.TextChanged -= ClientIdTextBox_TextChanged;
        OfficeTextBox.TextChanged -= OfficeTextBox_TextChanged;
        VisitDatePicker.SelectedDateChanged -= VisitDatePicker_SelectedDateChanged;
        VisitTimePicker.SelectedTimeChanged -= VisitTimePicker_SelectedTimeChanged;
    }

    private void InitializeFields()
    {
        _id = IdTextBox.Text;
        _clientId = ClientIdTextBox.Text;
        _office = OfficeTextBox.Text;
        _date = DateOnly.FromDateTime(VisitDatePicker.SelectedDate.GetValueOrDefault());
        _time = TimeOnly.FromDateTime(VisitTimePicker.SelectedTime.GetValueOrDefault());
    }

    private void ClearInputFields()
    {
        IdTextBox.IsEnabled = true;
        ClientIdTextBox.IsEnabled = true;

        IdTextBox.Text = string.Empty;
        ClientIdTextBox.Text = string.Empty;
        OfficeTextBox.Text = string.Empty;
        VisitDatePicker.SelectedDate = null;
        VisitTimePicker.SelectedTime = null;
    }
}
