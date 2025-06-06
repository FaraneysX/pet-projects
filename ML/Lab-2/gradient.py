import pandas as pd
from sklearn.compose import ColumnTransformer
from sklearn.impute import SimpleImputer
from sklearn.metrics import mean_absolute_error
from sklearn.model_selection import train_test_split
from sklearn.pipeline import Pipeline
from sklearn.preprocessing import OneHotEncoder
from xgboost import XGBRegressor

# Читаем датасет
data = pd.read_csv('ds.csv')
data = data.drop(['Unnamed: 0'], axis=1)

y = data.Movie_Rating
X = data.drop(['Movie_Rating'], axis=1)

print(X.info())

# Выбираем обучающий и тестовый набор
X_train_full, X_valid_full, y_train, y_valid = train_test_split(X, y, train_size=0.8, test_size=0.2, random_state=0)

# Ищем категориальные столбцы
categorical_cols = [cname for cname in X_train_full.columns
                    if X_train_full[cname].nunique() < 10 and
                    X_train_full[cname].dtype == "object"]

# Выбираем столбцы с числовыми значениями
numerical_cols = [cname for cname in X_train_full.columns
                  if X_train_full[cname].dtype in ['int64', 'float64']]

# Оставляем только выбранные столбцы
my_cols = categorical_cols + numerical_cols
X_train = X_train_full[my_cols].copy()
X_valid = X_valid_full[my_cols].copy()

# Предобработка для числовых данных
numerical_transformer = SimpleImputer(strategy='constant')

# Предобработка для категориальных данных
categorical_transformer = Pipeline(steps=[
    ('imputer', SimpleImputer(strategy='most_frequent')),
    ('onehot', OneHotEncoder(handle_unknown='ignore'))
])

preprocessor = ColumnTransformer(
    transformers=[
        ('num', numerical_transformer, numerical_cols),
        ('cat', categorical_transformer, categorical_cols)
    ])

# XGBRegressor - градиентный бустинг.
# XGBoost строит композицию деревьев, добавляя их последовательно.
# Каждое новое дерево направлено на исправление ошибок предыдущих.
my_pipeline = Pipeline(steps=[('preprocessor', preprocessor),
                              ('model', XGBRegressor(n_estimators=50))
                              ])

# Обучение модели.
my_pipeline.fit(X_train, y_train)

# Предобработка тестовых данных и прогнозирование
preds = my_pipeline.predict(X_valid)

# Оценка модели
score = mean_absolute_error(y_valid, preds)
print('MAE:', score)
