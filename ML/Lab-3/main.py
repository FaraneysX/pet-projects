import matplotlib.pyplot as plt
import pandas as pd
from keras import layers
from tensorflow import keras


# Минимальная потеря валидации.
def minimum_validation_loss(model, X_train, y_train, X_valid, y_valid, title):
    # Компиляция модели перед началом процесса обучения.
    model.compile(
        # Алгоритм оптимизации для использования в процессе обучения нейронной сети.
        # Сочетает в себе идеи из оптимизаторов - метод адаптивного шага и метод моментов.
        optimizer='adam',

        # Функция потерь, которая будет минимизироваться во время обучения.
        # Соответствует средней абсолютной ошибке.
        loss='mae'
    )

    # Обучение нейронной сети.
    training = model.fit(
        # X_train - признаки.
        # y_train - целевые значения, которые модель должна предсказать.
        X_train, y_train,

        # Данные для валидации модели.
        # Используются для оценки производительности модели
        # в конце каждой эпохи обучения.
        validation_data=(X_valid, y_valid),

        # Количество примеров данных, используемых
        # для обновления весов модели на каждом шаге обучения.
        batch_size=256,

        # Количество эпох обучения (модель будет обучаться на данных 40 раз).
        epochs=40,

        # Уровень логирования в процессе обучения.
        # 0 - не будет выводиться информация о каждой эпохе.
        verbose=0
    )

    # Создание объекта DataFrame из словаря training.history.
    # training.history - словарь, где ключи - названия метрик,
    # значения - списки значений метрик в каждой эпохе.
    training_df = pd.DataFrame(training.history)

    # Выбираются столбцы:
    # 'loss' (функция потерь на тренировочных данных)
    # и 'val_loss' (функция потерь на валидационных данных).
    training_df.loc[:, ['loss', 'val_loss']].plot()

    # Отображаем график.
    plt.title(title)
    plt.show()

    # Возвращаем минимальное значение функции потерь
    # на валидационных данных в течение всех эпох обучения.
    return training_df['val_loss'].min()


# Диабет.
# Беременность, уровень глюкозы, кровяное давление,
# толщина кожи, инсулин, индекс массы тела,
# родословная функция диабета, возраст, результат.

data_name = 'diabetes.csv'
to_drop = 'BloodPressure'

# Чтение данных.
df = pd.read_csv(data_name)

# Случайная выборка доли данных из набора данных.
# Берем 70% случайных данных.
# Устанавливаем начальное значение для генератора
# случайных чисел.
# Используется для обучения модели.
df_train = df.sample(frac=0.7, random_state=0)

# Создание нового набора данных путем исключения
# строк, которые используются в df_train.
# Используется для валидации модели.
# Валидация помогает оценить производительность модели
# на новых данных, которые она не видела в процессе обучения.
df_valid = df.drop(df_train.index)

# Нормализация данных, приводя их к диапазону от 0 до 1.
min_ = df_train.min(axis=0)
max_ = df_train.max(axis=0)
df_train = (df_train - min_) / (max_ - min_)
df_valid = (df_valid - min_) / (max_ - min_)

# Удаляем столбец с именем to_drop из тренировочного набора данных.
X_train = df_train.drop(to_drop, axis=1)

# Выбираем столбец с именем to_drop.
y_train = df_train[to_drop]

# Удаляем столбец с именем to_drop из тестового набора данных.
X_valid = df_valid.drop(to_drop, axis=1)

# Выбираем столбец с именем to_drop.
y_valid = df_valid[to_drop]

# Определение размерности входного слоя модели.
# Количество признаков (входов), которые будут
# использоваться в модели машинного обучения.
# len(...) - количество столбцов в DataFrame.
# -1 - исключение из общего числа столбцов тот столбец,
# который используется как целевая переменная.
number_input_shape = len(df.axes[1]) - 1

# Создание модели нейронной сети.
# Первый слой - 512 нейронов, указываем размерность входного слоя.
# Второй слой - 512 нейронов (размерность передается автоматически).
# Третий слой - 512 нейронов.
# Четвертый слой - выходной слой с одним нейроном (ожидаем получить числовое значение).
# relu - 0, если отрицательные, иначе данные без изменений.
base_model = keras.Sequential([
    layers.Dense(512, activation='relu', input_shape=[number_input_shape]),
    layers.Dense(512, activation='relu'),
    layers.Dense(512, activation='relu'),
    layers.Dense(1)
])

# Улучшенная модель с 1024 нейронами.
wider_model = keras.Sequential([
    layers.Dense(1024, activation='relu', input_shape=[number_input_shape]),
    layers.Dense(1024, activation='relu'),
    layers.Dense(1024, activation='relu'),
    layers.Dense(1)
])

# Улучшенная модель с большим количеством слоев.
deeper_model = keras.Sequential([
    layers.Dense(512, activation='relu', input_shape=[number_input_shape]),
    layers.Dense(512, activation='relu'),
    layers.Dense(512, activation='relu'),
    layers.Dense(512, activation='relu'),
    layers.Dense(512, activation='relu'),
    layers.Dense(512, activation='relu'),
    layers.Dense(1),
])

min_validation_loss_for_base_model = minimum_validation_loss(base_model, X_train, y_train, X_valid, y_valid,
                                                             'Базовая модель')
min_validation_loss_for_wider_model = minimum_validation_loss(wider_model, X_train, y_train, X_valid, y_valid,
                                                              'Улучшенная модель с 1024 нейронами')
min_validation_loss_for_deeper_model = minimum_validation_loss(deeper_model, X_train, y_train, X_valid, y_valid,
                                                               'Улучшенная модель с 6 слоями.')

the_lowest_validation_loss = min(min_validation_loss_for_base_model,
                                 min_validation_loss_for_wider_model,
                                 min_validation_loss_for_deeper_model)

model_losses = {
    'base': min_validation_loss_for_base_model,
    'wider': min_validation_loss_for_wider_model,
    'deeper': min_validation_loss_for_deeper_model
}

best_model = min(model_losses, key=model_losses.get)

print(f"Minimum validation loss for base model: {min_validation_loss_for_base_model}")
print(f"Minimum validation loss for wider model: {min_validation_loss_for_wider_model}")
print(f"Minimum validation loss for deeper model model: {min_validation_loss_for_deeper_model}")

print(
    f'Наименьшие потери при валидации наблюдаются при использовании {best_model} модели ({the_lowest_validation_loss})')
