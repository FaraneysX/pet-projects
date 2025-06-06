using System.Windows;

namespace AnimalClinic;

public partial class ExitWindow : Window
{
    public ExitWindow()
    {
        InitializeComponent();
    }

    // Обработчик события нажатия кнопки "Нет"
    private void NoExitButton_Click(object sender, RoutedEventArgs e)
    {
        // Закрыть текущее окно выхода
        GetWindow(this).Close();
    }

    // Обработчик события нажатия кнопки "Да"
    private void YesExitButton_Click(object sender, RoutedEventArgs e)
    {
        // Завершить работу приложения
        Application.Current.Shutdown();
    }
}
