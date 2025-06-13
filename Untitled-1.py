import numpy as np
X_train = np.array([[8, 9, 9],
                    [4, 4, 6],
                    [9, 8, 8],
                    [5, 6, 4]])
y_train = np.array([1, -1, 1, -1])
# Khởi tạo trọng số
w = np.zeros(3)
b = 0
# Hàm kích hoạt
def activate(x, w, b):
    return 1 if np.dot(x, w) + b >= 0 else -1
# Thuật toán Perceptron
def train_perceptron(X, y, w, b, epochs=10):
    for _ in range(epochs):
        for i in range(len(X)):
            x = X[i]
            target = y[i]
            output = activate(x, w, b)
            if output != target: 
                w += target * x
                b += target
    return w, b
# Train Perceptron
w, b = train_perceptron(X_train, y_train, w, b)
# Test
test_data = np.array([8, 7, 8])
prediction = activate(test_data, w, b)
print(f"Loại của bộ dữ liệu [8, 7, 8] là: {prediction}")