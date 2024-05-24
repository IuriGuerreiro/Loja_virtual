--
-- File generated with SQLiteStudio v3.4.4 on ter mai 21 20:28:51 2024
--
-- Text encoding used: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table: produtos
CREATE TABLE IF NOT EXISTS produtos (id INTEGER UNIQUE PRIMARY KEY, nome TEXT, preco REAL, stock INTEGER, categoria TEXT);
INSERT INTO produtos (id, nome, preco, stock, categoria) VALUES (1, 'ram 8gb', 14.99, 32, 'Memoria Ram');
INSERT INTO produtos (id, nome, preco, stock, categoria) VALUES (2, 'ram 4gb', 6.99, 20, 'Memorial Ram');
INSERT INTO produtos (id, nome, preco, stock, categoria) VALUES (3, 'Monitor', 149.99, 12, 'Periféricos');
INSERT INTO produtos (id, nome, preco, stock, categoria) VALUES (4, 'disco rigido', 29.99, 53, 'Memoria');

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
