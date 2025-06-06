import pandas as pd
from sklearn.ensemble import RandomForestRegressor
from sklearn.impute import SimpleImputer
from sklearn.metrics import mean_absolute_error
from sklearn.model_selection import train_test_split

# Загружаем данные
data = pd.read_csv('ds.csv')
data = data.drop(['Unnamed: 0'], axis=1)

# Выбираем целевой столбец
y = data.Movie_Rating

# Удаляем столбец Movie_Rating.
melb_predictors = data.drop(['Movie_Rating'], axis=1)

# Оставляем только числовые признаки.
X = melb_predictors.select_dtypes(exclude=['object'])

# Разделяем данные на обучающий и тестовый набор
X_train, X_valid, y_train, y_valid = train_test_split(X, y, train_size=0.8, test_size=0.2, random_state=0, shuffle=True)


# Функция для сравнения эффективности разных подходов
def score_dataset(X_train, X_valid, y_train, y_valid):
    model = RandomForestRegressor(n_estimators=10, random_state=0)
    model.fit(X_train, y_train)
    preds = model.predict(X_valid)

    return mean_absolute_error(y_valid, preds)


# Получаем столбцы, в которых есть недостающие значения.
cols_with_missing = [col for col in X_train.columns
                     if X_train[col].isnull().any()]

# Удаляем столбцы и в обучающем, и в тестовом наборах.
reduced_X_train = X_train.drop(cols_with_missing, axis=1)
reduced_X_valid = X_valid.drop(cols_with_missing, axis=1)

print("MAE при первом подходе (Удаление столбцов):")
print(score_dataset(reduced_X_train, reduced_X_valid, y_train, y_valid))

# Пропущенные значения в обучающем и валидационном наборах данных
# заменяем самыми часто встречающимися значениями.
my_imputer = SimpleImputer(strategy="most_frequent")
my_imputer.fit(X)
imputed_X_train = pd.DataFrame(my_imputer.transform(X_train))
imputed_X_valid = pd.DataFrame(my_imputer.transform(X_valid))

# Восстанавливаем имена столбцов.
imputed_X_train.columns = X_train.columns
imputed_X_valid.columns = X_valid.columns

print("MAE при втором подходе (Вставка):")
print(score_dataset(imputed_X_train, imputed_X_valid, y_train, y_valid))

# Создаем копию данных
X_train_plus = X_train.copy()
X_valid_plus = X_valid.copy()

# Создаем столбцы, в которых будет отмечаться, что вставляли
for col in cols_with_missing:
    X_train_plus[col + '_was_missing'] = X_train_plus[col].isnull()
    X_valid_plus[col + '_was_missing'] = X_valid_plus[col].isnull()

# Вставка.
# Для чисел - среднее.
# Для категориальных - самое часто встречающееся.
my_imputer = SimpleImputer()

# Обучаем и трансформируем импутер,
# заменяя пропущенные значения в X_train_plus на соответствующие импутированные значения.
imputed_X_train_plus = pd.DataFrame(my_imputer.fit_transform(X_train_plus))

# Используем обученный импутер для замены
# пропущенных значений в X_valid_plus.
imputed_X_valid_plus = pd.DataFrame(my_imputer.transform(X_valid_plus))

# Восстанавливаем имена столбцов
imputed_X_train_plus.columns = X_train_plus.columns
imputed_X_valid_plus.columns = X_valid_plus.columns

print("MAE при третьем подходе (Расширенная вставка):")
print(score_dataset(imputed_X_train_plus, imputed_X_valid_plus, y_train, y_valid))

# Размер исходных данных
print(X_train.shape)

# Количество недостающих значений в каждом столбце обучающего набора
missing_val_count_by_column = (X_train.isnull().sum())
print(missing_val_count_by_column[missing_val_count_by_column > 0])
