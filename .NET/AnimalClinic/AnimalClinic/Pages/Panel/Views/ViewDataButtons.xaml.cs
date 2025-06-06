using System.Windows;
using System.Windows.Controls;

using AnimalClinic.DataAccess;

namespace AnimalClinic.Pages.Panel.Views;

public partial class ViewDataButtons : Page
{
    public ViewDataButtons()
    {
        InitializeComponent();
    }

    // Обработчик нажатия кнопки "Выбрать все данные клиента"
    private void SelectAllClientDataButton_Click(object sender, RoutedEventArgs e)
    {
        Select(ClientDataCheckBoxes);
    }

    // Обработчик нажатия кнопки "Удалить все данные клиента"
    private void RemoveAllClientDataButton_Click(object sender, RoutedEventArgs e)
    {
        Remove(ClientDataCheckBoxes);
    }

    // Обработчик нажатия кнопки "Выбрать все данные животного"
    private void SelectAllAnimalDataButton_Click(object sender, RoutedEventArgs e)
    {
        Select(AnimalDataCheckBoxes);
    }

    // Обработчик нажатия кнопки "Удалить все данные животного"
    private void RemoveAllAnimalDataButton_Click(object sender, RoutedEventArgs e)
    {
        Remove(AnimalDataCheckBoxes);
    }

    // Обработчик нажатия кнопки "Выбрать все данные посещения"
    private void SelectAllVisitDataButton_Click(object sender, RoutedEventArgs e)
    {
        Select(VisitDataCheckBoxes);
    }

    // Обработчик нажатия кнопки "Удалить все данные посещения"
    private void RemoveAllVisitDataButton_Click(object sender, RoutedEventArgs e)
    {
        Remove(VisitDataCheckBoxes);
    }

    // Обработчик нажатия кнопки "Выбрать все данные"
    private void SelectAllButton_Click(object sender, RoutedEventArgs e)
    {
        // Выбрать все данные клиента, животного и посещения
        Select(ClientDataCheckBoxes);
        Select(AnimalDataCheckBoxes);
        Select(VisitDataCheckBoxes);
    }

    // Обработчик нажатия кнопки "Удалить все данные"
    private void RemoveAllButton_Click(object sender, RoutedEventArgs e)
    {
        // Удалить все данные клиента, животного и посещения
        Remove(ClientDataCheckBoxes);
        Remove(AnimalDataCheckBoxes);
        Remove(VisitDataCheckBoxes);
    }

    // Обработчик нажатия кнопки "Поиск"
    private async void SearchButton_Click(object sender, RoutedEventArgs e)
    {
        // Получение выбранных полей клиента, животного и посещения
        var clientFields = GetCheckedFieldNames(ClientDataCheckBoxes);
        var animalFields = GetCheckedFieldNames(AnimalDataCheckBoxes);
        var visitFields = GetCheckedFieldNames(VisitDataCheckBoxes);

        // Настройка отображения таблицы данных
        DataGridViewSetup(clientFields, animalFields, visitFields);

        // Если не выбрано ни одного поля, очистить таблицу и завершить метод
        if (clientFields.Count == 0 &&
            animalFields.Count == 0 &&
            visitFields.Count == 0)
        {
            ClearButton_Click(sender, e);

            return;
        }

        if (clientFields.Count != 0 &&
            animalFields.Count == 0 &&
            visitFields.Count == 0)
        {
            await Task.Run(() => ClientDataView(clientFields));

            return;
        }

        // Логика отображения данных в зависимости от выбранных полей
        if (clientFields.Count == 0 &&
            animalFields.Count != 0 &&
            visitFields.Count == 0)
        {
            await Task.Run(() => AnimalDataView(animalFields));

            return;
        }

        if (clientFields.Count == 0 &&
            animalFields.Count == 0 &&
            visitFields.Count != 0)
        {
            await Task.Run(() => VisitDataView(visitFields));

            return;
        }

        if (clientFields.Count != 0 &&
            animalFields.Count != 0 &&
            visitFields.Count == 0)
        {
            await Task.Run(() => ClientAnimalDataView(clientFields, animalFields));

            return;
        }

        if (clientFields.Count != 0 &&
            animalFields.Count == 0 &&
            visitFields.Count != 0)
        {
            await Task.Run(() => ClientVisitDataView(clientFields, visitFields));

            return;
        }

        if (clientFields.Count == 0 &&
            animalFields.Count != 0 &&
            visitFields.Count != 0)
        {
            await Task.Run(() => AnimalVisitDataView(animalFields, visitFields));

            return;
        }

        // Метод отображения данных клиента в таблице
        await Task.Run(async () =>
        {
            // Получение контекста базы данных
            await using var context = new AnimalClinicDbContext();

            // Проверка наличия данных о животных и посещениях
            if (context.Animals is not null && context.Visits is not null)
            {
                // Формирование запроса на выборку данных из базы
                var query = from client in context.Clients
                            join animal in context.Animals on client.Id equals animal.Client.Id into clientAnimals
                            from animal in clientAnimals.DefaultIfEmpty()
                            join visit in context.Visits on client.Id equals visit.Client.Id into clientVisits
                            from visit in clientVisits.DefaultIfEmpty()
                            select new
                            {
                                ClientId = client.Id,
                                // Отображение выбранных полей клиента
                                ClientName = clientFields.Contains("ClientNameCheckBox") ? client.Name : null,
                                ClientSurname = clientFields.Contains("ClientSurnameCheckBox") ? client.Surname : null,
                                ClientPatronymic = clientFields.Contains("ClientPatronymicCheckBox") ? client.Patronymic : null,
                                ClientBirthDate = clientFields.Contains("ClientBirthDateCheckBox") ? client.BirthDate : null,
                                ClientEmail = clientFields.Contains("ClientEmailCheckBox") ? client.Email : null,
                                ClientPhoneNumber = clientFields.Contains("ClientPhoneNumberCheckBox") ? client.PhoneNumber : null,
                                AnimalId = animal.Id,
                                AnimalNickname = animalFields.Contains("AnimalNicknameCheckBox") ? animal.Nickname : null,
                                AnimalKind = animalFields.Contains("AnimalKindCheckBox") ? animal.Kind : null,
                                AnimalBreed = animalFields.Contains("AnimalBreedCheckBox") ? animal.Breed : null,
                                VisitId = visit.Id,
                                VisitOffice = visitFields.Contains("VisitOfficeCheckBox") ? visit.Office : null,
                                VisitDate = visitFields.Contains("VisitDateCheckBox") ? visit.Date : null,
                                VisitTime = visitFields.Contains("VisitTimeCheckBox") ? visit.Time : null
                            };

                // Если запрос не вернул результатов, завершить выполнение метода
                if (!query.Any())
                {
                    return;
                }

                // Отображение данных в таблице с использованием диспетчера для доступа к элементам интерфейса из другого потока
                Dispatcher.Invoke(() =>
                {
                    // Сортировка данных по выбранным полям
                    if (VisitDataSortAscendingRadioButton.IsChecked == true)
                    {
                        query = query.OrderBy(item => item.VisitOffice);
                    }
                    else
                    {
                        query = query.OrderByDescending(item => item.VisitOffice);
                    }

                    if (AnimalDataSortAscendingRadioButton.IsChecked == true)
                    {
                        query = query.OrderBy(item => item.AnimalNickname);
                    }
                    else
                    {
                        query = query.OrderByDescending(item => item.AnimalNickname);
                    }

                    if (ClientDataSortAscendingRadioButton.IsChecked == true)
                    {
                        query = query.OrderBy(item => item.ClientName);
                    }
                    else
                    {
                        query = query.OrderByDescending(item => item.ClientName);
                    }

                    // Отображение данных в таблице
                    DatabaseView.Database.ItemsSource = query.ToList();
                });
            }
        });
    }

    // Метод отображения данных животного в таблице (аналогичен ClientDataView)
    private async Task ClientDataView(List<string> clientFields)
    {
        await using var context = new AnimalClinicDbContext();

        if (context.Animals is not null && context.Visits is not null)
        {
            var query = from client in context.Clients
                        join animal in context.Animals on client.Id equals animal.Client.Id into clientAnimals
                        from animal in clientAnimals.DefaultIfEmpty()
                        join visit in context.Visits on client.Id equals visit.Client.Id into clientVisits
                        from visit in clientVisits.DefaultIfEmpty()
                        select new
                        {
                            ClientId = client.Id,
                            ClientName = clientFields.Contains("ClientNameCheckBox") ? client.Name : null,
                            ClientSurname = clientFields.Contains("ClientSurnameCheckBox") ? client.Surname : null,
                            ClientPatronymic = clientFields.Contains("ClientPatronymicCheckBox") ? client.Patronymic : null,
                            ClientBirthDate = clientFields.Contains("ClientBirthDateCheckBox") ? client.BirthDate : null,
                            ClientEmail = clientFields.Contains("ClientEmailCheckBox") ? client.Email : null,
                            ClientPhoneNumber = clientFields.Contains("ClientPhoneNumberCheckBox") ? client.PhoneNumber : null,
                            AnimalId = animal.Id,
                            VisitId = visit.Id,
                        };

            if (!query.Any())
            {
                return;
            }

            Dispatcher.Invoke(() =>
            {
                if (ClientDataSortAscendingRadioButton.IsChecked == true)
                {
                    query = query.OrderBy(item => item.ClientName);
                }
                else
                {
                    query = query.OrderByDescending(item => item.ClientName);
                }

                DatabaseView.Database.ItemsSource = query.ToList();
            });
        }
    }

    private async Task AnimalDataView(List<string> animalFields)
    {
        await using var context = new AnimalClinicDbContext();

        var query = context.Animals?.Select(animal => new
        {
            AnimalId = animal.Id,
            AnimalNickname = animalFields.Contains("AnimalNicknameCheckBox") ? animal.Nickname : null,
            AnimalKind = animalFields.Contains("AnimalKindCheckBox") ? animal.Kind : null,
            AnimalBreed = animalFields.Contains("AnimalBreedCheckBox") ? animal.Breed : null,
        }).ToList();

        if (query?.Count == 0)
        {
            return;
        }

        Dispatcher.Invoke(() =>
        {
            if (AnimalDataSortAscendingRadioButton.IsChecked == true)
            {
                query = query?.OrderBy(item => item.AnimalNickname).ToList();
            }
            else
            {
                query = query?.OrderByDescending(item => item.AnimalNickname).ToList();
            }

            DatabaseView.Database.ItemsSource = query;
        });
    }

    private async Task VisitDataView(List<string> visitFields)
    {
        await using var context = new AnimalClinicDbContext();

        var query = context.Visits?.Select(visit => new
        {
            VisitId = visit.Id,
            VisitOffice = visitFields.Contains("VisitOfficeCheckBox") ? visit.Office : null,
            VisitDate = visitFields.Contains("VisitDateCheckBox") ? visit.Date : null,
            VisitTime = visitFields.Contains("VisitTimeCheckBox") ? visit.Time : null,
        }).ToList();

        if (query?.Count == 0)
        {
            return;
        }

        Dispatcher.Invoke(() =>
        {
            if (VisitDataSortAscendingRadioButton.IsChecked == true)
            {
                query = query?.OrderBy(item => item.VisitOffice).ToList();
            }
            else
            {
                query = query?.OrderByDescending(item => item.VisitOffice).ToList();
            }

            DatabaseView.Database.ItemsSource = query;
        });
    }

    private async Task ClientAnimalDataView(List<string> clientFields, List<string> animalFields)
    {
        await using var context = new AnimalClinicDbContext();

        if (context.Animals is not null)
        {
            var query = from client in context.Clients
                        join animal in context.Animals on client.Id equals animal.Client.Id into clientAnimals
                        from animal in clientAnimals.DefaultIfEmpty()
                        select new
                        {
                            ClientId = client.Id,
                            ClientName = clientFields.Contains("ClientNameCheckBox") ? client.Name : null,
                            ClientSurname = clientFields.Contains("ClientSurnameCheckBox") ? client.Surname : null,
                            ClientPatronymic = clientFields.Contains("ClientPatronymicCheckBox") ? client.Patronymic : null,
                            ClientBirthDate = clientFields.Contains("ClientBirthDateCheckBox") ? client.BirthDate : null,
                            ClientEmail = clientFields.Contains("ClientEmailCheckBox") ? client.Email : null,
                            ClientPhoneNumber = clientFields.Contains("ClientPhoneNumberCheckBox") ? client.PhoneNumber : null,
                            AnimalId = animal.Id,
                            AnimalNickname = animalFields.Contains("AnimalNicknameCheckBox") ? animal.Nickname : null,
                            AnimalKind = animalFields.Contains("AnimalKindCheckBox") ? animal.Kind : null,
                            AnimalBreed = animalFields.Contains("AnimalBreedCheckBox") ? animal.Breed : null,
                        };

            if (!query.Any())
            {
                return;
            }

            Dispatcher.Invoke(() =>
            {

                if (AnimalDataSortAscendingRadioButton.IsChecked == true)
                {
                    query = query.OrderBy(item => item.AnimalNickname);
                }
                else
                {
                    query = query.OrderByDescending(item => item.AnimalNickname);
                }

                if (ClientDataSortAscendingRadioButton.IsChecked == true)
                {
                    query = query.OrderBy(item => item.ClientName);
                }
                else
                {
                    query = query.OrderByDescending(item => item.ClientName);
                }

                DatabaseView.Database.ItemsSource = query.ToList();
            });
        }
    }

    private async Task ClientVisitDataView(List<string> clientFields, List<string> visitFields)
    {
        await using var context = new AnimalClinicDbContext();

        if (context.Visits is not null)
        {
            var query = from client in context.Clients
                        join visit in context.Visits on client.Id equals visit.Client.Id into clientVisits
                        from visit in clientVisits.DefaultIfEmpty()
                        select new
                        {
                            ClientId = client.Id,
                            ClientName = clientFields.Contains("ClientNameCheckBox") ? client.Name : null,
                            ClientSurname = clientFields.Contains("ClientSurnameCheckBox") ? client.Surname : null,
                            ClientPatronymic = clientFields.Contains("ClientPatronymicCheckBox") ? client.Patronymic : null,
                            ClientBirthDate = clientFields.Contains("ClientBirthDateCheckBox") ? client.BirthDate : null,
                            ClientEmail = clientFields.Contains("ClientEmailCheckBox") ? client.Email : null,
                            ClientPhoneNumber = clientFields.Contains("ClientPhoneNumberCheckBox") ? client.PhoneNumber : null,
                            VisitId = visit.Id,
                            VisitOffice = visitFields.Contains("VisitOfficeCheckBox") ? visit.Office : null,
                            VisitDate = visitFields.Contains("VisitDateCheckBox") ? visit.Date : null,
                            VisitTime = visitFields.Contains("VisitTimeCheckBox") ? visit.Time : null,
                        };

            if (!query.Any())
            {
                return;
            }

            Dispatcher.Invoke(() =>
            {
                if (VisitDataSortAscendingRadioButton.IsChecked == true)
                {
                    query = query.OrderBy(item => item.VisitOffice);
                }
                else
                {
                    query = query.OrderByDescending(item => item.VisitOffice);
                }

                if (ClientDataSortAscendingRadioButton.IsChecked == true)
                {
                    query = query.OrderBy(item => item.ClientName);
                }
                else
                {
                    query = query.OrderByDescending(item => item.ClientName);
                }

                DatabaseView.Database.ItemsSource = query.ToList();
            });
        }
    }

    private async Task AnimalVisitDataView(List<string> animalFields, List<string> visitFields)
    {
        await using var context = new AnimalClinicDbContext();

        if (context.Visits is not null)
        {
            var query = from animal in context.Animals
                        join visit in context.Visits on animal.Client.Id equals visit.Client.Id into clientVisits
                        from visit in clientVisits.DefaultIfEmpty()
                        select new
                        {
                            AnimalId = animal.Id,
                            AnimalNickname = animalFields.Contains("AnimalNicknameCheckBox") ? animal.Nickname : null,
                            AnimalKind = animalFields.Contains("AnimalKindCheckBox") ? animal.Kind : null,
                            AnimalBreed = animalFields.Contains("AnimalBreedCheckBox") ? animal.Breed : null,
                            VisitId = visit.Id,
                            VisitOffice = visitFields.Contains("VisitOfficeCheckBox") ? visit.Office : null,
                            VisitDate = visitFields.Contains("VisitDateCheckBox") ? visit.Date : null,
                            VisitTime = visitFields.Contains("VisitTimeCheckBox") ? visit.Time : null,
                        };

            if (!query.Any())
            {
                return;
            }

            Dispatcher.Invoke(() =>
            {
                if (VisitDataSortAscendingRadioButton.IsChecked == true)
                {
                    query = query.OrderBy(item => item.VisitOffice);
                }
                else
                {
                    query = query.OrderByDescending(item => item.VisitOffice);
                }

                if (AnimalDataSortAscendingRadioButton.IsChecked == true)
                {
                    query = query.OrderBy(item => item.AnimalNickname);
                }
                else
                {
                    query = query.OrderByDescending(item => item.AnimalNickname);
                }

                DatabaseView.Database.ItemsSource = query.ToList();
            });
        }
    }

    // Обработчик события нажатия кнопки "Очистить"
    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        // Очистка колонок таблицы данных и скрытие таблицы
        DatabaseView.Database.Columns.Clear();
        DatabaseView.Database.ItemsSource = null;
        DatabaseView.Database.Visibility = Visibility.Collapsed;

        // Снятие выбора с чекбоксов
        Remove(ClientDataCheckBoxes);
        Remove(AnimalDataCheckBoxes);
        Remove(VisitDataCheckBoxes);
    }

    // Метод установки выбранного состояния для чекбоксов
    private static void Select(StackPanel stackPanel)
    {
        SetCheckBoxesCheckedStatus(stackPanel, true);
    }

    // Метод снятия выбранного состояния для чекбоксов
    private static void Remove(StackPanel stackPanel)
    {
        SetCheckBoxesCheckedStatus(stackPanel, false);
    }

    // Метод установки состояния выбранности для чекбоксов внутри панели
    private static void SetCheckBoxesCheckedStatus(StackPanel stackPanel, bool checkedStatus)
    {
        // Перебор всех элементов в панели
        foreach (var element in stackPanel.Children.OfType<CheckBox>())
        {
            // Установка состояния выбранности для чекбокса
            element.IsChecked = checkedStatus;
        }
    }

    // Метод получения имен выбранных полей из панели с чекбоксами
    private static List<string> GetCheckedFieldNames(StackPanel stackPanel)
    {
        // Выбор чекбоксов, которые выбраны
        return stackPanel.Children.OfType<CheckBox>()
            .Where(checkBox => checkBox.IsChecked == true)
            .Select(checkBox => checkBox.Name)
            .ToList();
    }

    // Метод настройки отображения таблицы данных
    private static void DataGridViewSetup(List<string> clientFields, List<string> animalFields, List<string> visitFields)
    {
        // Показать таблицу данных
        DatabaseView.Database.Visibility = Visibility.Visible;

        // Очистить колонки и содержимое таблицы
        DatabaseView.Database.Columns.Clear();
        DatabaseView.Database.ItemsSource = null;

        // Настройка колонок таблицы данных в зависимости от выбранных полей
        if (clientFields.Count != 0)
        {
            // Добавление колонок данных клиента
            DatabaseView.AddColumn("ID клиента", "ClientId");

            // Добавление колонок данных о клиенте, если выбраны
            if (clientFields.Contains("ClientNameCheckBox"))
            {
                DatabaseView.AddColumn("Имя", "ClientName");
            }

            if (clientFields.Contains("ClientSurnameCheckBox"))
            {
                DatabaseView.AddColumn("Фамилия", "ClientSurname");
            }

            if (clientFields.Contains("ClientPatronymicCheckBox"))
            {
                DatabaseView.AddColumn("Отчество", "ClientPatronymic");
            }

            if (clientFields.Contains("ClientBirthDateCheckBox"))
            {
                DatabaseView.AddColumn("Дата рождения", "ClientBirthDate");
            }

            if (clientFields.Contains("ClientEmailCheckBox"))
            {
                DatabaseView.AddColumn("Электронная почта", "ClientEmail");
            }

            if (clientFields.Contains("ClientPhoneNumberCheckBox"))
            {
                DatabaseView.AddColumn("Номер телефона", "ClientPhoneNumber");
            }

            if (animalFields.Count == 0)
            {
                DatabaseView.AddColumn("ID питомца", "AnimalId");
            }

            if (visitFields.Count == 0)
            {
                DatabaseView.AddColumn("ID записи", "VisitId");
            }
        }

        if (animalFields.Count != 0)
        {
            DatabaseView.AddColumn("ID питомца", "AnimalId");

            if (animalFields.Contains("AnimalNicknameCheckBox"))
            {
                DatabaseView.AddColumn("Кличка питомца", "AnimalNickname");
            }

            if (animalFields.Contains("AnimalKindCheckBox"))
            {
                DatabaseView.AddColumn("Вид питомца", "AnimalKind");
            }

            if (animalFields.Contains("AnimalBreedCheckBox"))
            {
                DatabaseView.AddColumn("Порода питомца", "AnimalBreed");
            }
        }

        if (visitFields.Count != 0)
        {
            DatabaseView.AddColumn("ID записи", "VisitId");

            if (visitFields.Contains("VisitOfficeCheckBox"))
            {
                DatabaseView.AddColumn("Офис", "VisitOffice");
            }

            if (visitFields.Contains("VisitDateCheckBox"))
            {
                DatabaseView.AddColumn("Дата посещения", "VisitDate");
            }

            if (visitFields.Contains("VisitTimeCheckBox"))
            {
                DatabaseView.AddColumn("Время посещения", "VisitTime");
            }
        }
    }
}
