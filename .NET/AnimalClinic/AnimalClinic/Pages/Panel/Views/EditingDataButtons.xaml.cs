using System.Windows;
using System.Windows.Controls;

using AnimalClinic.Service;

namespace AnimalClinic.Pages.Panel.Views;

public partial class EditingDataButtons : Page
{
    public EditingDataButtons()
    {
        InitializeComponent();

        // Проверка уровня доступа пользователя
        if (Settings.Account != Account.Admin)
        {
            // Скрыть кнопку удаления данных для пользователей, не являющихся администраторами
            DeletingDataButton.Visibility = Visibility.Collapsed;
        }
    }

    private void AddingDataButton_Click(object sender, RoutedEventArgs e)
    {
        // Переход на страницу добавления данных
        NavigationService.Navigate(new AddingDataButtons());
    }

    private void ChangingDataButton_Click(object sender, RoutedEventArgs e)
    {
        // Переход на страницу изменения данных
        NavigationService.Navigate(new ChangingDataButtons());
    }

    private void DeletingDataButton_Click(object sender, RoutedEventArgs e)
    {
        // Переход на страницу удаления данных
        NavigationService.Navigate(new DeletingDataButtons());
    }
}
