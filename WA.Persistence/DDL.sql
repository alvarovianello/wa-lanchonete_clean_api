CREATE SCHEMA IF NOT EXISTS dbo;

CREATE TABLE dbo.Customer (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100),
    email VARCHAR(100) UNIQUE,
    cpf VARCHAR(11) UNIQUE
);


CREATE TABLE dbo.Category (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) UNIQUE,
    description TEXT
);

CREATE TABLE dbo.Product (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100),
    description TEXT,
    price NUMERIC(10, 2),
    category_id INT REFERENCES dbo.Category(id),
    image VARCHAR(400)
);

CREATE TABLE dbo.Orders (
    id SERIAL PRIMARY KEY,
    customer_id INT REFERENCES dbo.Customer(id),
    order_number VARCHAR(50),
    total_price NUMERIC(10, 2),
    status VARCHAR(50),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE dbo.OrderItem (
    id SERIAL PRIMARY KEY,
    order_id INT REFERENCES dbo.Orders(id),
    product_id INT REFERENCES dbo.Product(id),
    quantity INT,
    price NUMERIC(10, 2)
);

CREATE TABLE dbo.OrderStatus (
    id SERIAL PRIMARY KEY,
    order_id INT REFERENCES dbo.Orders(id),
    status VARCHAR(50),
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE dbo.Payment (
    id SERIAL PRIMARY KEY,
    order_id INT REFERENCES dbo.Orders(id),
    payment_method VARCHAR(50),
    payment_status VARCHAR(50),
    payment_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	payment_date_processed TIMESTAMP,
	in_store_order_id VARCHAR(500),
	qr_data VARCHAR(500)
);

INSERT INTO dbo.Category (name, description) VALUES
('Lanche', 'Sanduíches e hambúrgueres variados'),
('Acompanhamento', 'Batatas fritas, saladas e outros acompanhamentos'),
('Bebida', 'Refrigerantes, sucos e outras bebidas'),
('Sobremesa', 'Sobremesas diversas');

INSERT INTO dbo.Product (name, description, price, category_id) VALUES
('Cheeseburger', 'Hambúrguer com queijo', 10.00, 1),
('Batata Frita', 'Porção de batatas fritas', 5.00, 2),
('Refrigerante', 'Coca-Cola 350ml', 3.00, 3),
('Sorvete', 'Casquinha de sorvete', 4.00, 4);

INSERT INTO dbo.Customer (name, email, cpf) VALUES
('Cliente Anônimo', '', '');

INSERT INTO dbo.Orders (customer_id, order_number, total_price, status, created_at) VALUES
(1, '26368', 29, 'Recebido','2024-05-25 19:47:50.531376');

INSERT INTO dbo.OrderItem (order_id, product_id, quantity, price) VALUES
(1, 1, 2, 20),
(1, 2, 1, 5),
(1, 4, 1, 4);

INSERT INTO dbo.OrderStatus (order_id, status, updated_at) VALUES
(1, 'Recebido', '2024-05-25 19:47:50.569727');