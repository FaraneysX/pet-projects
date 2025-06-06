using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace AnimalClinic.Pages.Panel.Views;

public partial class MainButtons : Page
{
    public MainButtons()
    {
        InitializeComponent();
    }

    private void ViewDataButton_Click(object sender, RoutedEventArgs e)
    {
        // Переход на страницу просмотра данных
        NavigationService.Navigate(new ViewDataButtons());
    }

    private void EditingDataButton_Click(object sender, RoutedEventArgs e)
    {
        // Переход на страницу редактирования данных
        NavigationService.Navigate(new EditingDataButtons());
    }

    private void DataSearchButton_Click(object sender, RoutedEventArgs e)
    {
        // Переход на страницу поиска данных
        NavigationService.Navigate(new DataSearchButtons());
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        // Возвращение на предыдущую страницу
        NavigationService.GetNavigationService(this).GoBack();
    }
}
