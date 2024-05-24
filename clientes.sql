--
-- File generated with SQLiteStudio v3.4.4 on ter mai 21 20:28:28 2024
--
-- Text encoding used: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table: clientes
CREATE TABLE IF NOT EXISTS clientes (nome TEXT, numero_tele NUMERIC UNIQUE, Email TEXT PRIMARY KEY UNIQUE, morada TEXT, metodoPagamento TEXT, creditCardNumber INTEGER UNIQUE);
INSERT INTO clientes (nome, numero_tele, Email, morada, metodoPagamento, creditCardNumber) VALUES ('mariana', 932183131, 'mariana@gmail.com', 'rua das velas de lisboa ja ns o que estou a escrever', NULL, NULL);
INSERT INTO clientes (nome, numero_tele, Email, morada, metodoPagamento, creditCardNumber) VALUES ('macaco', 913112323, 'macaco@gmail.com', 'floresta', NULL, NULL);

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
