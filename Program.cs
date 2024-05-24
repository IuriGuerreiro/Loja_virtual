// -----------------------------------------------------------------------
// <file>Program.cs</file>
// <author>edutechy</author>
// <date>2024-05-15</date>
// <description>
// Este ficheiro contém o programa principal para a aplicação da loja virtual.
// O programa permite o registo de clientes, adicionar produtos, a gestão do 
// carrinho de compras e processar transações de compra. 
//
// Funcionalidades principais:
// - Registo novos clientes.
// - Adicionar novos produtos ao catálogo da loja.
// - Listar catálogo de produtos disponíveis.
// - Adicionar produtos ao carrinho de compras do cliente.
// - Processar o pagamento dos itens no carrinho de compras.
// - Atualizar o catálogo de produtos após a compra.
//
// O programa utiliza uma base de dados SQLite para armazenar as informações
// de clientes, produtos e transações. Inclui funcionalidades para
// ler e escrever dados na base de dados, bem como para a gestão das
// operações básicas de uma loja virtual.
//
// Este código demonstra conceitos fundamentais da programação orientada a
// objetos, como classes, objetos, métodos e interação com base de dados.
// </description>
// -----------------------------------------------------------------------



using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using cliente;
using produto;
using carrinhoDeCompras;
using lojaVirtualComDB;


namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            loja loja = new loja();
            int opcao = -27362367;
            try
            {
                loja.CriarDB();
                loja.carregarProdutos();
                loja.carregarClientes();
                loja.carregarCarrinho();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            do
            {
                try
                {
                    Console.WriteLine("1-Adicionar produto");
                    Console.WriteLine("2-Adicionar cliente");
                    Console.WriteLine("3-Listar produtos");
                    Console.WriteLine("4-Listar clientes");
                    Console.WriteLine("5-Adicionar produto ao carrinho");
                    Console.WriteLine("6-Mostrar carrinho");
                    Console.WriteLine("7-Finalizar compra");
                    Console.WriteLine("0-sair");
                    Console.WriteLine("Opção- ");
                    opcao = int.Parse(Console.ReadLine());
                    switch (opcao)
                    {
                        case 1:
                            loja.adicionar_produto();
                            break;
                        case 2:
                            loja.adicionar_cliente();
                            break;
                        case 3:
                            loja.listar_produtos();
                            break;
                        case 4:
                            loja.listar_clientes();
                            break;
                        case 5:
                            loja.adicionar_produto_ao_carrinho();
                            break;
                        case 6:
                            loja.mostarCarrinho();
                            break;
                        case 7:
                            loja.finalizarCompra();
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (opcao != 0);
        }
    }
}