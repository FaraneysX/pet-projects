using System.Windows.Controls;

using AnimalClinic.DataAccess;
using AnimalClinic.Pages.Panel.Views;
using AnimalClinic.Service;

namespace AnimalClinic.Pages.Panel;

public partial class PanelPage : Page
{
    public PanelPage()
    {
        InitializeComponent();

        SuccessfulNotification.MessageQueue = new();

        // Отображение уведомления о успешном соединении с базой данных в зависимости от типа учетной записи
        if (Settings.Account == Account.Admin)
        {
            SuccessfulNotification.MessageQueue.Enqueue("Соединение с БД установлено (администратор).");
        }
        else
        {
            SuccessfulNotification.MessageQueue.Enqueue("Соединение с БД установлено (пользователь).");
        }

        // Привязка источника данных для отображения таблицы базы данных
        DatabaseView.Database = DatabaseDataGrid;

        // Отображение представления основных кнопок управления
        ButtonsView.Content = new MainButtons();
    }
}
