using System.Windows;
using System.Windows.Controls;

using AnimalClinic.DataAccess;

namespace AnimalClinic.Pages.Panel.Views;

public partial class DataSearchButtons : Page
{
    public DataSearchButtons()
    {
        InitializeComponent();

        Notification.MessageQueue = new();
    }

    private void ClientsWithVisitsOnWeekendsButton_Click(object sender, RoutedEventArgs e)
    {
        DatabaseView.Reset();

        DatabaseView.AddColumn("ID клиента", "ClientId");
        DatabaseView.AddColumn("Имя клиента", "ClientName");
        DatabaseView.AddColumn("Фамилия клиента", "ClientSurname");
        DatabaseView.AddColumn("Отчество клиента", "ClientPatronymic");
        DatabaseView.AddColumn("ID посещения", "VisitId");
        DatabaseView.AddColumn("Офис", "VisitOffice");
        DatabaseView.AddColumn("Дата", "VisitDate");
        DatabaseView.AddColumn("Время", "VisitTime");

        using var context = new AnimalClinicDbContext();

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
                            VisitId = visit.Id,
                            VisitOffice = visit.Office,
                            VisitDate = visit.Date,
                            VisitTime = visit.Time,
                        };

            var result = query.ToList().Where(item => IsWeekend(item.VisitDate));

            DatabaseView.Database.ItemsSource = result;
        }
    }

    // Метод проверки, что дата является выходным.
    private static bool IsWeekend(DateOnly? date)
    {
        if (date is null)
        {
            return false;
        }

        var dateTime = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day);

        return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
    }

    private void CatsOnlyButton_Click(object sender, RoutedEventArgs e)
    {
        DatabaseView.Reset();

        DatabaseView.AddColumn("ID животного", "AnimalId");
        DatabaseView.AddColumn("Кличка", "AnimalNickname");
        DatabaseView.AddColumn("Вид", "AnimalKind");
        DatabaseView.AddColumn("Порода", "AnimalBreed");

        using var context = new AnimalClinicDbContext();

        var query = context.Animals?.Select(animal => new
        {
            AnimalId = animal.Id,
            AnimalNickname = animal.Nickname,
            AnimalKind = animal.Kind,
            AnimalBreed = animal.Breed,
        }).ToList();

        var cats = query?
            .Where(item => item.AnimalKind != null &&
            (item.AnimalKind.Equals("кот", StringComparison.CurrentCultureIgnoreCase) ||
            item.AnimalKind.Equals("кошка", StringComparison.CurrentCultureIgnoreCase)))
            .ToList();

        DatabaseView.Database.ItemsSource = cats;
    }

    private void ResetButton_Click(object sender, RoutedEventArgs e)
    {
        DatabaseView.Reset();

        DatabaseView.Database.Visibility = Visibility.Collapsed;
    }
}
