import warnings

import matplotlib.pyplot as plt
import tensorflow as tf


def show_tensor(tensor, title):
    # Создание окна отображения размером х на х дюймов.
    plt.figure(figsize=(10, 8))

    # Отображение изображения из тензора.
    # Убираем все размерности длины 1, оставляя только высоту и ширину.
    # Необходимо для корректного отображения.
    plt.imshow(tf.squeeze(tensor))

    # Убираем оси координат.
    plt.axis('off')

    plt.title(title)
    plt.show()


# Свертка изображения.
# 1. Берем матрицу значений (ядро свертки).
# 2. Ядро накладывается на входное изображение и скользит по нему,
# вычисляя скалярное произведение между пикселями изображения в текущей
# позиции и значениями ядра.
# 3. В результате для каждого положения ядра получаем одно число, которое записывается
# в output-изображение в соответствующих координатах.
#
# Цель: находить на изображении важные признаки объектов
# и формировать их инвариантное представление.
def convolution(tensor, kernel):
    return tf.nn.conv2d(
        input=tensor,  # Входной тензор изображения, с которым будет выполнена свертка.
        filters=kernel,  # Ядро свертки (фильтр).
        strides=1,  # Шаг свертки.
        padding='SAME'  # Изображение дополняется по краям нулевыми пикселями, чтобы можно было наложить ядро
        # свертки на крайние области.
    )


# Пулинг.
# 1. Вырезаем окно фиксированного размера.
# 2. В пределах этого окна вычисляется статистика (здесь - максимальное значение).
# 3. Окно сдвигается на заданный шаг и процесс повторяется.
#
# Цель: кодировка изображения в более компактный набор значимых признаков,
# обладающих некоторой инвариантностью к сдвигам и искажениям.
def pooling(tensor):
    return tf.nn.pool(
        input=tensor,  # Входной тензор изображения, с которым будет выполнен пулинг.
        window_shape=(2, 2),  # Размер окна, в пределах которого вычисляется статистика для пулинга.
        pooling_type='MAX',  # Используется max-пулинг - в окне выбирается максимальное значение.
        strides=(2, 2),  # Шаги перемещения окна - 2 по вертикали и горизонтали.
        padding='SAME'  # Изображение дополняется по краям нулевыми пикселями, чтобы можно было наложить ядро
        # свертки на крайние области.
    )


def kernel_test(tensor, kernel, title):
    tensor_convolution = convolution(tensor, kernel)
    show_tensor(tensor_convolution, title + ' (свертка)')

    # Заменяем отрицательные значения фильтра на 0.
    # Замена отрицательных значений может помочь модели сфокусироваться
    # на важных признаках в данных, игнорируя менее важные детали.
    tensor_filter = tf.nn.relu(tensor_convolution)

    show_tensor(tensor_filter, title + ' (фильтрация)')

    tensor_pooling = pooling(tensor_filter)
    show_tensor(tensor_pooling, title + ' (пулинг)')


def kernel_horizontal_lines_test(tensor):
    # Ядро свертки 3х3, содержащее 32-битные числа с плавающей запятой.
    kernel_horizontal = tf.constant([
        [-1, -1, -1],
        [2, 2, 2],
        [-1, -1, -1]
    ], dtype=tf.float32)

    # Изменяем форму ядра свертки, добавляя в конец два дополнительных измерения,
    # соответствующих каналам входного и выходного изображений.
    # Необходимо для корректной свертки ядра с изображением.
    kernel_horizontal = tf.reshape(kernel_horizontal, [*kernel_horizontal.shape, 1, 1])

    kernel_test(tensor, kernel_horizontal, "Горизонтальные линии")


def kernel_contours_test(tensor):
    # Ядро свертки 3х3, содержащее 32-битные числа с плавающей запятой.
    kernel = tf.constant([
        [-1, -1, -1],
        [-1, 8, -1],
        [-1, -1, -1]
    ], dtype=tf.float32)

    # Изменяем форму ядра свертки, добавляя в конец два дополнительных измерения,
    # соответствующих каналам входного и выходного изображений.
    # Необходимо для корректной свертки ядра с изображением.
    kernel = tf.reshape(kernel, [*kernel.shape, 1, 1])

    kernel_test(tensor, kernel, "Контуры")


def kernel_vertical_lines(tensor):
    # Ядро свертки 3х3, содержащее 32-битные числа с плавающей запятой.
    kernel = tf.constant([
        [-1, 2, -1],
        [-1, 2, -1],
        [-1, 2, -1]
    ], dtype=tf.float32)

    # Изменяем форму ядра свертки, добавляя в конец два дополнительных измерения,
    # соответствующих каналам входного и выходного изображений.
    # Необходимо для корректной свертки ядра с изображением.
    kernel = tf.reshape(kernel, [*kernel.shape, 1, 1])

    kernel_test(tensor, kernel, "Вертикальные линии")


def test(tensor):
    kernel_vertical_lines(tensor)
    kernel_contours_test(tensor)
    kernel_horizontal_lines_test(tensor)


def preprocess_image(image_path):
    # Считывание содержимого файла.
    tensor_image = tf.io.read_file(image_path)

    # Декодирование файла в тензор.
    # channels=1 - декодирование должно иметь только один цветовой канал (серый).
    tensor_image = tf.io.decode_jpeg(tensor_image, channels=1)

    # Конвертация типа тензора в 32-битный формат с плавающей точкой.
    tensor_image = tf.image.convert_image_dtype(tensor_image, dtype=tf.float32)

    # Добавление дополнительной размерности в тензор.
    # Необходимо для нейросети.
    tensor_image = tf.expand_dims(tensor_image, axis=0)

    return tensor_image


def configure_plotting():
    # Настройка параметров фигуры.
    # Автоматическое размещение фигуры (здесь - по всему окну).
    plt.rc('figure', autolayout=True)

    # Цветовая палитра по-умолчанию для изображения.
    plt.rc('image', cmap='gray')

    warnings.filterwarnings('ignore')


configure_plotting()
image = preprocess_image('image.jpg')
test(image)
