using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

using AnimalClinic.DataAccess;

namespace AnimalClinic.Components;

public partial class BackButton : UserControl
{
    public BackButton()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.GetNavigationService(this).GoBack();

        DatabaseView.Database.Visibility = Visibility.Collapsed;
    }
}
