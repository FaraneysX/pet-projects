using System.Windows;
using System.Windows.Controls;

namespace AnimalClinic.Pages.Panel.Views;

public partial class DeletingDataButtons : Page
{
    public DeletingDataButtons()
    {
        InitializeComponent();
    }

    private void DeletingClientDataButton_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new DeletingClientDataButtons());
    }

    private void DeletingAnimalDataButton_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new DeletingAnimalDataButtons());
    }

    private void DeletingVisitDataButton_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new DeletingVisitDataButtons());
    }
}
