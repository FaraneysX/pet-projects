using System.Windows;
using System.Windows.Controls;

namespace AnimalClinic.Pages.Panel.Views;

public partial class AddingDataButtons : Page
{
    public AddingDataButtons()
    {
        InitializeComponent();
    }

    private void AddingClientDataButton_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new AddingClientDataButtons());
    }

    private void AddingAnimalDataButton_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new AddingAnimalDataButtons());
    }

    private void AddingVisitDataButton_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new AddingVisitDataButtons());
    }
}
