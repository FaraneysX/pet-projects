import pandas as pd
from sklearn.compose import ColumnTransformer
from sklearn.ensemble import RandomForestRegressor
from sklearn.impute import SimpleImputer
from sklearn.metrics import roc_auc_score
from sklearn.model_selection import cross_val_score
from sklearn.model_selection import train_test_split
from sklearn.pipeline import Pipeline
from sklearn.preprocessing import OneHotEncoder

# Загружаем датасет.
data = pd.read_csv('ds.csv')
data = data.drop(['Unnamed: 0'], axis=1)

# Отделяем целевые данные.
y = data.Movie_Rating
X = data.drop(['Movie_Rating'], axis=1)

# Выбираем обучающий и тестовый набор.
X_train_full, X_valid_full, y_train, y_valid = train_test_split(X, y, train_size=0.8, test_size=0.2, random_state=0)

# Ищем категориальные столбцы.
categorical_cols = [cname for cname in X_train_full.columns
                    if X_train_full[cname].nunique() < 10 and
                    X_train_full[cname].dtype == "object"]

# Выбираем столбцы с числовыми значениями.
numerical_cols = [cname for cname in X_train_full.columns
                  if X_train_full[cname].dtype in ['int64', 'float64']]

# Оставляем только выбранные столбцы.
my_cols = categorical_cols + numerical_cols
X_train = X_train_full[my_cols].copy()
X_valid = X_valid_full[my_cols].copy()

# Предобработка для числовых данных.
# Заполнение отсутствующих значения в числовые данные.
# Отсутствующие значения будут заполнены константными значениями (0).
numerical_transformer = SimpleImputer(strategy='constant')

# Предобработка для категориальных данных.
# Заполнение отсутствующих значений наиболее
# часто встречающимся значением в столбце.
# Прямое кодирование категориальных данных
# после их заполнения.
# Кодировщик игнорирует неизвестные категории при преобразовании.
categorical_transformer = Pipeline(steps=[
    ('imputer', SimpleImputer(strategy='most_frequent')),
    ('onehot', OneHotEncoder(handle_unknown='ignore'))
])

# Объединение преобразований для числовых и категориальных данных.
preprocessor = ColumnTransformer(
    transformers=[
        ('num', numerical_transformer, numerical_cols),
        ('cat', categorical_transformer, categorical_cols)
    ])

# Конвейер, который объединяем процесс предварительной обработки данных
# и модель машинного обучения.
my_pipeline = Pipeline(steps=[('preprocessor', preprocessor),
                              ('model', RandomForestRegressor(n_estimators=100,
                                                              random_state=0))
                              ])

# Перекрестная проверка с использованием конвейера для моделирования
# и предварительной обработки данных.
# X - матрица признаков (набор данных).
# y - вектор целевых значений.
# cv - количество разбиений для перекрестной проверки.
# scoring - метрика для оценки производительности.
# Умножаем на -1, так как cross_val_score возвращает отрицательное MAE
scores = -1 * cross_val_score(my_pipeline, X, y,
                              cv=5,
                              scoring='neg_mean_absolute_error')

print("MAE:\n", scores)

print("Среднее по всем экспериментам:")
print(scores.mean())
print(roc_auc_score(X, y))
