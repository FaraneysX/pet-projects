using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AnimalClinic.DataAccess;

// Статический класс для работы с представлением базы данных в виде DataGrid
public static class DatabaseView
{
    // Свойство, представляющее DataGrid для отображения данных
    public static DataGrid Database { get; set; } = new();

    // Метод для добавления столбца в DataGrid
    public static void AddColumn(string header, string bindingColumn)
    {
        // Создание нового текстового столбца с указанным заголовком и привязкой к свойству объекта
        Database.Columns.Add(new DataGridTextColumn
        {
            Header = header,
            Binding = new Binding(bindingColumn),
            IsReadOnly = true,
        });
    }

    // Метод для сброса настроек DataGrid
    public static void Reset()
    {
        // Установка видимости DataGrid
        Database.Visibility = Visibility.Visible;

        // Очистка столбцов DataGrid
        Database.Columns.Clear();

        // Установка источника данных DataGrid в null
        Database.ItemsSource = null;
    }
}
