using cliente;
using produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using produto;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using System.Data;
using System.ComponentModel;
using System.Security.Cryptography;

namespace lojaVirtualComDB
{
    internal class loja
    {
        List<Produto> Produtos = new List<Produto>();
        List<Cliente> Clientes = new List<Cliente>();
        string connectionDB = "Data Source=loja_virtual.db";

        public void CriarDB()
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionDB))
                {
                    connection.Open();

                    string criarProdutos = @"CREATE TABLE IF NOT EXISTS produtos (
                                    Id INTEGER PRIMARY KEY UNIQUE,
                                    Nome TEXT UNIQUE,
                                    Preco REAL,
                                    Stock INTEGER,
                                    Categoria TEXT)";
                    using (var cmd = new SQLiteCommand(criarProdutos, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    string criarClientes = @"CREATE TABLE IF NOT EXISTS clientes (
                                    nome TEXT,
                                    numero_tele TEXT UNIQUE,   
                                    Email TEXT PRIMARY KEY UNIQUE,
                                    morada TEXT,
                                    metodoPagamento TEXT,
                                    creditCardNumber TEXT UNIQUE)";
                    using (var cmd = new SQLiteCommand(criarClientes, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    string criarCarrinho = @"CREATE TABLE IF NOT EXISTS carrinho (
                                    Email TEXT,
                                    Id INTEGER,
                                    comprado INTEGER,
                                    FOREIGN KEY(Email) REFERENCES clientes(Email),
                                    FOREIGN KEY(Id) REFERENCES produtos(Id))";
                    using (var cmd = new SQLiteCommand(criarCarrinho, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar a base de dados: {ex.Message}");
            }
        }
        public void carregarProdutos()
        {
            string nome;
            int id;
            int stock;
            double preco;
            string categoria;
            using (var con = new SqliteConnection(connectionDB))
            {
                con.Open();

                // Preparação de uma consulta (Query) SQL à base de dados
                var cmd = new SqliteCommand("SELECT * FROM produtos", con);

                try
                {
                    // Execução da consulta
                    using var reader = cmd.ExecuteReader();

                    // Leitura e tratamento dos campos (Atributos) lidos da base de dados.

                    // Com estes podem re-criar as listas de objetos e outros objetos
                    // necessários ao vosso programa tal como se fossem lidos de um ficheiro texto.

                    // TODO: Ver resolução do teste para exemplo prático de como criar
                    // os objetos a partir dos dados armazenados em ficheiro.

                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                        stock = reader.GetInt32(2);
                        preco = reader.GetDouble(3);
                        categoria = reader.GetString(4);
                        preco = Math.Round(preco, 2);
                        nome = reader.GetString(1);
                        Produto produto = new Produto(nome, id, stock, preco, categoria);
                        Produtos.Add(produto);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    // Fecha a ligação à base de dados
                    con.Close();
                }
            }
        }

        public void carregarClientes()
        {
            string nome;
            int id;
            int numeroTelefone;
            string email;
            string morada;
            using (var con = new SqliteConnection(connectionDB))
            {
                con.Open();

                // Preparação de uma consulta (Query) SQL à base de dados
                var sqlite = new SqliteCommand("SELECT * FROM clientes", con);

                try
                {
                    // Execução da consulta
                    using var reader = sqlite.ExecuteReader();

                    while (reader.Read())//percorre a tabela clientes
                    {
                        nome = reader.GetString(0).ToString();//nome do cliente
                        numeroTelefone = reader.GetInt32(1);// numero de telefone
                        email = reader.GetString(2).ToString();//email
                        morada = reader.GetString(3).ToString();//morada
                        Cliente cliente = new Cliente(nome, email, numeroTelefone, morada);//cria um novo cliente
                        Clientes.Add(cliente);//adiciona o cliente à lista de clientes
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    // Fecha a ligação à base de dados
                    con.Close();
                }
            }
        }
        public void carregarCarrinho()
        {
            if (Clientes.Count > 0 && Produtos.Count > 0)
            {
                using (var con = new SqliteConnection(connectionDB))
                {
                    con.Open();

                    // Preparação de uma consulta (Query) SQL à base de dados
                    var sqlite = new SqliteCommand("SELECT * FROM carrinho", con);

                    try
                    {
                        // Execução da consulta
                        using var reader = sqlite.ExecuteReader();

                        while (reader.Read())//percorre a tabela carrinho
                        {
                            int comprado = reader.GetInt32(2);//verifica se o produto foi comprado

                            if (comprado == 0)//se o produto não foi comprado
                            {
                                foreach (var cliente in Clientes)//percorre a lista de clientes
                                {
                                    if (cliente.Email == reader.GetString(0))//verifica se o email do cliente é igual ao email do carrinho
                                    {
                                        int idProduto = reader.GetInt32(1);//id do produto
                                        foreach (Produto produto in Produtos)//percorre a lista de produtos
                                        {
                                            if (produto.Id == idProduto)//verifica se o id do produto é igual ao id do produto no carrinho
                                            {
                                                if (produto.Stock > 0)//verifica se o produto está disponivel
                                                {
                                                    cliente.adicionarProduto(produto);//adiciona o produto ao carrinho
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach(var cliente in Clientes)//percorre a lista de clientes
                                {
                                    if(cliente.Email == reader.GetString(0))//verifica se o email do cliente é igual ao email do carrinho
                                    {
                                        int idProduto = reader.GetInt32(1);//id do produto
                                        foreach (Produto produto in Produtos)//percorre a lista de produtos
                                        {
                                            if (produto.Id == idProduto)//verifica se o id do produto é igual ao id do produto no carrinho
                                            {
                                                cliente.adicionarProdutoComprado(produto);//adiciona o produto ao carrinho
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        // Fecha a ligação à base de dados
                        con.Close();
                    }
                }
            }
        }

        public void adicionar_produto()
        {
            Console.WriteLine("Nome do produto:");
            string nome = Console.ReadLine();
            Console.WriteLine("Preço do produto:");
            double preco1 = LerDouble("preco");
            Console.WriteLine("Stock do produto:");
            int stock1 = verificar_num();
            Console.WriteLine("Categoria do produto:");
            string categoria = Console.ReadLine();
            Produto produto = new Produto(nome, Produtos.Count + 1, stock1, preco1, categoria);
            Produtos.Add(produto);
            try
            {
                using (var con = new SqliteConnection(connectionDB))
                {
                    con.Open();
                    var cmd = new SqliteCommand("INSERT INTO produtos (Id, Nome, Preco, Stock, categoria) VALUES (@Id, @Nome, @Preco, @Stock, @Categoria)", con);
                    cmd.Parameters.AddWithValue("@Id", Produtos.Count);
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Preco", preco1);
                    cmd.Parameters.AddWithValue("@Stock", stock1);
                    cmd.Parameters.AddWithValue("@Categoria", categoria);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }catch (Exception e)
            {
                Console.WriteLine("Erro: " + e.GetType());
            }
        }

        public void adicionar_cliente()
        {
            Console.WriteLine("Nome do cliente:");
            string nome = Console.ReadLine();
            Console.WriteLine("Email do cliente:");
            string email = emailReader();
            Console.WriteLine("Numero de telefone do cliente:");
            int numero1 = verificar_num();
            Console.WriteLine("Morada do cliente:");
            string morada = Console.ReadLine();
            Cliente cliente = new Cliente(nome, email, numero1, morada);
            Clientes.Add(cliente);
            try
            {
                using (var con = new SqliteConnection(connectionDB))
                {
                    con.Open();
                    var cmd = new SqliteCommand("INSERT INTO clientes (nome, numero_tele, Email, morada) VALUES (@Nome, @Numero, @Email, @Morada)", con);
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Numero", numero1);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Morada", morada);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: " + e.GetType());
            }
        }

        public void listar_produtos()
        {
            Console.WriteLine("Produtos:");
            int i = 1;
            foreach (Produto produto in Produtos)
            {
                Console.WriteLine($"\nProduto Nº{i}");
                Console.WriteLine(produto.info());
                i++;
            }
        }

        public void listar_clientes()
        {
            int i = 1;
            foreach (Cliente cliente in Clientes)
            {
                Console.WriteLine($"\nCliente Nº{i}");
                Console.WriteLine(cliente.info());
                i++;
            }
        }

        public void adicionar_produto_ao_carrinho()
        {
            bool emailExiste = false;
            bool produtoExiste = false;
            Console.WriteLine("Email do cliente:");
            string email = emailReader();
            Console.WriteLine("Id do produto:");
            int ID = 345678765;
            do{
                ID = verificar_num();
            } while ( ID > Produtos.Count);
            foreach (Cliente cliente in Clientes)
            {
                if (cliente.Email == email)
                {
                    emailExiste = true;
                    foreach (Produto produto in Produtos)
                    {
                        if (produto.Id == ID)
                        {
                            produtoExiste = true;
                            cliente.adicionarProduto(produto);
                            try
                            {
                                using (var con = new SqliteConnection(connectionDB))
                                {
                                    con.Open();
                                    var cmd = new SqliteCommand("INSERT INTO carrinho (Email, Id, comprado) VALUES (@Email, @Id, @Comprado)", con);
                                    cmd.Parameters.AddWithValue("@Email", email);
                                    cmd.Parameters.AddWithValue("@Id", ID);
                                    cmd.Parameters.AddWithValue("@Comprado", 0);
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    Console.WriteLine("Produto adicionado ao carrinho");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Erro: " + e.GetType());
                            }
                        }
                    }
                }
            }
            if ( emailExiste == false)
            {
                Console.WriteLine("esse email não existe");
            }
            else
            {
                if (produtoExiste == false)
                {
                    Console.WriteLine("\nesse produto não existe\n\n");
                }
            }

        }
        public void mostarCarrinho()
        {
            Console.WriteLine("\nEmail do cliente:");
            string email = emailReader();
            foreach (Cliente cliente in Clientes)
            {
                if (cliente.Email == email)
                {
                    cliente.verCarrinho();
                }
            }
        }

        public void finalizarCompra()
        {
            bool emailExiste = false;
            Console.WriteLine("Email do cliente:");
            string email = emailReader();
            foreach (Cliente cliente in Clientes)
            {
                if(cliente.Email == email)
                {
                    emailExiste = true;
                    if (cliente.metodo_pagamento != null && cliente.metodo_pagamento != "")
                    {
                        acabarCompra(cliente);
                    }
                    else
                    {
                        Console.WriteLine("Tem que adicionar um metodo de pagamento");
                        do
                        {
                            Console.WriteLine("Introduza o metodo de pagamento(MBway, cartao de debito/credito, paypal, IBAN, Skrill):");
                            cliente.metodo_pagamento = Console.ReadLine();
                            cliente.metodo_pagamento = cliente.metodo_pagamento.ToLower();
                        } while (cliente.metodo_pagamento != "mbway" && cliente.metodo_pagamento != "cartao de debito" && cliente.metodo_pagamento != "cartao de credito" && cliente.metodo_pagamento != "paypal" && cliente.metodo_pagamento != "iban" && cliente.metodo_pagamento != "skrill");
                        if (cliente.metodo_pagamento == "cartao de debito" || cliente.metodo_pagamento == "cartao de credito")
                        {
                            Console.WriteLine("Introduza o numero do cartao de credito:");
                            cliente.NumeroCartao = verificar_num();
                        }


                        using (var con = new SqliteConnection(connectionDB))
                        {
                            con.Open();
                            var cmd = new SqliteCommand("UPDATE clientes SET metodoPagamento = @metodoPagamento Where Email= @Email", con);
                            cmd.Parameters.AddWithValue("@metodoPagamento", cliente.metodo_pagamento);
                            cmd.Parameters.AddWithValue("@Email", cliente.Email);
                            cmd.ExecuteNonQuery();
                            if (cliente.metodo_pagamento == "cartao de debito" || cliente.metodo_pagamento == "cartao de credito")
                            {
                                var cmd2 = new SqliteCommand("UPDATE clientes SET creditCardNumber = @creditCardNumber Where Email= @Email", con);
                                cmd2.Parameters.AddWithValue("@creditCardNumber", cliente.NumeroCartao);
                                cmd2.Parameters.AddWithValue("@Email", cliente.Email);
                                cmd2.ExecuteNonQuery();
                            }
                            con.Close();
                        }

                        acabarCompra(cliente);
                    }
                }
            }
            if(emailExiste == false)
            {
                Console.WriteLine("Esse email não existe");
            }
        }


        public void acabarCompra(Cliente cliente)
        {
            string reposta;
            double preco;
            preco = cliente.precoCarrinho();
            do
            {
                Console.WriteLine($"O preco será {preco} deseja continuar? (sim/nao)");
                reposta = Console.ReadLine();
                reposta = reposta.ToLower();
                if (reposta.Contains("sim"))
                {
                    try
                    {
                        using (var con = new SqliteConnection(connectionDB))
                        {
                            con.Open();
                            var cmd = new SqliteCommand("UPDATE carrinho SET comprado = 1 WHERE Email = @Email", con);
                            cmd.Parameters.AddWithValue("@Email", cliente.Email);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            cliente.acabarCompra();
                            Console.WriteLine($"Compra concluida!!!\nForam retirados {preco} do {cliente.metodo_pagamento}");
                            cliente.acabarCompra();
                        }
                    }catch(Exception e)
                    {
                        Console.WriteLine("erro" + e.Message);
                    }
                }
            } while (reposta != "sim" && reposta != "nao");
        }


        private int verificar_num()// verifica se o numero é inteiro
        {
            int resultado = 0;
            bool flag = true;
            while (flag)//loop para verificar se o numero é inteiro
            {
                try//tenta converter o numero para inteiro
                {
                    resultado = int.Parse(Console.ReadLine());
                    flag = false;
                }
                catch (Exception e)//se não conseguir converter o numero para inteiro
                {
                    Console.WriteLine("Erro: " + e.GetType() + resultado);
                    flag = true;
                }
                if (flag == false)//se conseguir converter o numero para inteiro retorna o numero
                    return resultado;

            }
            return -3333;
        }
        public static double LerDouble(string coisa)
        {
            while (true)
            {
                string numeroStr = Console.ReadLine();

                try
                {
                    double numero = double.Parse(numeroStr);
                    return numero;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"formato inválido introduza {coisa} novamente:");
                }
            }
        }
        public string emailReader()
        {
            do
            {
                string email;
                email = Console.ReadLine();
                if (email.Contains("@") && email.Contains("."))
                {
                    return email;
                }
                else
                {
                    Console.WriteLine("Email inválido, introduza novamente:");
                }
            }while(true);
        }
    }
}
