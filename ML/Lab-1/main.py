import pandas as pd

separator = '\n' + '=' * 80 + '\n\n'

# Установка параметров отображения.
pd.set_option('display.max_rows', 100)
pd.set_option('display.max_columns', 100)

# Загрузка данных.
data = pd.read_csv('database.csv')

# Вывод размера данных (количество строк и столбцов).
print('РАЗМЕР\n', data.shape, end=separator)

print('ОБЩАЯ ИНФОРМАЦИЯ О ДАННЫХ')
print(data.info(), end=separator)

print('ОПИСАТЕЛЬНАЯ СТАТИСТИКА ДАННЫХ', data.describe(), end=separator)

print('КРОСС-ТАБЛИЦА МЕЖДУ \'release_date\' И \'genre\'\n',
      pd.crosstab(data['release_date'], data['genre']), end=separator)

# Выбор данных для конкретных 'release_date' и 'genre'.
# Создание кросс-таблицы.
release_dates = data[(data['release_date'] == 1970) |
                     (data['release_date'] == 2019)]['release_date']
genres = data[(data['genre'] == 'pop') |
              (data['genre'] == 'rock')]['genre']

print(pd.crosstab(release_dates, genres), end=separator)

print('КРОСС-ТАБЛИЦА МЕЖДУ \'genre\' И \'topic\'\n',
      pd.crosstab(data['genre'], data['topic']), end=separator)

print('ГРУППИРОВКА ДАННЫХ ПО \'genre\' И ПОДСЧЕТ КОЛИЧЕСТВА \'topic\' В КАЖДОЙ ГРУППЕ\n',
      data.groupby('genre')['topic'].count(), end=separator)
