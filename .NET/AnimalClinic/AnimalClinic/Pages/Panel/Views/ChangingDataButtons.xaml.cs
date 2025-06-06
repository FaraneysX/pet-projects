using System.Windows;
using System.Windows.Controls;

namespace AnimalClinic.Pages.Panel.Views;

public partial class ChangingDataButtons : Page
{
    public ChangingDataButtons()
    {
        InitializeComponent();
    }

    private void ChangingClientDataButton_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new ChangingClientDataButtons());
    }

    private void ChangingAnimalDataButton_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new ChangingAnimalDataButtons());
    }

    private void ChangingVisitDataButton_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new ChangingVisitDataButtons());
    }
}
