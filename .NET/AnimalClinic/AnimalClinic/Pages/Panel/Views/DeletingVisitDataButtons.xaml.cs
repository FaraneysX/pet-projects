using System.Windows;
using System.Windows.Controls;

using AnimalClinic.DataAccess;

namespace AnimalClinic.Pages.Panel.Views;

public partial class DeletingVisitDataButtons : Page
{
    // Поля для хранения значений полей формы
    private string _id = string.Empty;
    private string _clientId = string.Empty;
    private string _office = string.Empty;
    private DateOnly? _date = null;
    private TimeOnly? _time = null;

    public DeletingVisitDataButtons()
    {
        InitializeComponent();

        // Инициализация очереди уведомлений
        Notification.MessageQueue = new();
    }

    // Метод, вызываемый при инициализации страницы
    protected override async void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);

        // Инициализация компонентов таблицы
        InitializeDataGrid();

        // Подписка на событие изменения выбора в таблице
        DatabaseView.Database.SelectionChanged += DatabaseView_SelectionChanged;

        // Отписка от события при выгрузке страницы
        Unloaded += (sender, e) =>
        {
            DatabaseView.Database.SelectionChanged -= DatabaseView_SelectionChanged;
        };

        // Отображение данных о посещениях
        await Task.Run(ShowData);
    }

    // Обработчик события изменения выбора в таблице данных
    private void DatabaseView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Получение выбранного элемента
        var selectedItem = DatabaseView.Database.SelectedItem;

        // Обновление значений полей формы на основе выбранного элемента
        if (selectedItem is not null)
        {
            // Блокировка полей формы
            Unsubscribe();

            IdTextBox.IsEnabled = false;
            ClientIdTextBox.IsEnabled = false;
            OfficeTextBox.IsEnabled = false;
            VisitDatePicker.IsEnabled = false;
            VisitTimePicker.IsEnabled = false;

            // Получение значений из выбранного элемента и установка их в поля формы
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

    // Обработчики событий изменения текста в текстовых полях и выбора даты и времени
    // Данные методы обновляют соответствующие значения полей и вызывают метод обновления данных

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

    private void DeleteDataButton_Click(object sender, RoutedEventArgs e)
    {
        // Инициализация полей для удаления данных
        InitializeFields();

        // Проверка введенных данных на корректность
        if (!IsValidated())
        {
            return;
        }

        // Удаление выбранного посещения
        DeleteVisit();
    }

    // Обработчик нажатия кнопки "Сбросить"
    private async void ResetButton_Click(object sender, RoutedEventArgs e)
    {
        // Подписка на события изменения полей формы
        Subscribe();

        // Очистка полей формы
        ClearInputFields();

        // Сброс значений полей фильтрации данных
        _id = string.Empty;
        _clientId = string.Empty;
        _office = string.Empty;
        _date = null;
        _time = null;

        // Обновление данных на странице
        await Task.Run(ShowData);
    }

    // Метод для инициализации компонентов таблицы данных о посещениях
    private static void InitializeDataGrid()
    {
        // Сброс предыдущих настроек таблицы
        DatabaseView.Reset();

        // Добавление столбцов в таблицу данных о посещениях
        DatabaseView.AddColumn("ID", "VisitId");
        DatabaseView.AddColumn("ID клиента", "ClientId");
        DatabaseView.AddColumn("Офис", "VisitOffice");
        DatabaseView.AddColumn("Дата", "VisitDate");
        DatabaseView.AddColumn("Время", "VisitTime");
    }

    // Метод для удаления выбранного посещения
    private async void DeleteVisit()
    {
        // Подключение к базе данных
        await using var context = new AnimalClinicDbContext();

        // Получение информации о выбранном посещении
        var visit = context.Visits?.FirstOrDefault(visit => visit.Id == int.Parse(_id) && visit.Client.Id == int.Parse(_clientId));

        // Если посещение найдено, происходит его удаление
        if (visit is not null)
        {
            context.Visits?.Remove(visit);

            try
            {
                // Сохранение изменений в базе данных
                await Task.Run(async () =>
                {
                    await context.SaveChangesAsync();
                });

                // Очистка полей формы
                ClearInputFields();

                // Вывод уведомления об успешном удалении данных
                Notification?.MessageQueue?.Enqueue("Данные успешно удалены.");

                // Обновление данных на странице
                await ShowData();
            }
            catch (Exception ex)
            {
                // Вывод уведомления об ошибке сохранения данных
                Notification?.MessageQueue?.Enqueue($"Ошибка сохранения данных: {ex.Message}");
            }
        }
        else
        {
            Notification?.MessageQueue?.Enqueue("Посещение с таким ID не найден.");
        }
    }

    // Метод для отображения данных о посещениях
    private async Task ShowData()
    {
        // Инициализация компонентов таблицы данных о посещениях
        Dispatcher.Invoke(InitializeDataGrid);

        // Подключение к базе данных
        await using var context = new AnimalClinicDbContext();

        // Проверка наличия данных о клиентах и животных в базе данных
        if (context.Clients is not null && context.Animals is not null)
        {
            // Формирование запроса для получения данных о посещениях
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

            // Применение фильтров к запросу, если они указаны
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

            // Если есть данные для отображения, добавляем их в таблицу данных
            if (query.Any())
            {
                Dispatcher.Invoke(() =>
                {
                    DatabaseView.Database.ItemsSource = query.ToList();
                });
            }
        }
    }

    // Метод для валидации введенных данных
    private bool IsValidated()
    {
        // Проверка наличия значений в обязательных полях и их корректности
        if (string.IsNullOrWhiteSpace(_id) ||
            string.IsNullOrWhiteSpace(_office) ||
            _date is null ||
            _time is null)
        {
            Notification?.MessageQueue?.Enqueue("Обязательные поля должны быть заполнены.");

            return false;
        }

        // Проверка даты и времени на корректность
        if (_date < DateOnly.FromDateTime(DateTime.Today) && _time < TimeOnly.FromDateTime(DateTime.Now))
        {
            Notification?.MessageQueue?.Enqueue("Дата и время записи не должны быть раньше текущего момента.");

            return false;
        }

        return true;
    }

    // Метод для подписки на события изменения полей формы
    private void Subscribe()
    {
        IdTextBox.TextChanged += IdTextBox_TextChanged;
        ClientIdTextBox.TextChanged += ClientIdTextBox_TextChanged;
        OfficeTextBox.TextChanged += OfficeTextBox_TextChanged;
        VisitDatePicker.SelectedDateChanged += VisitDatePicker_SelectedDateChanged;
        VisitTimePicker.SelectedTimeChanged += VisitTimePicker_SelectedTimeChanged;
    }

    // Метод для отписки от событий изменения полей формы
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
        OfficeTextBox.IsEnabled = true;
        VisitDatePicker.IsEnabled = true;
        VisitTimePicker.IsEnabled = true;

        IdTextBox.Text = string.Empty;
        ClientIdTextBox.Text = string.Empty;
        OfficeTextBox.Text = string.Empty;
        VisitDatePicker.SelectedDate = null;
        VisitTimePicker.SelectedTime = null;
    }
}
