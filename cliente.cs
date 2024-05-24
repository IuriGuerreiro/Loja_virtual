using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cliente;
using produto;
using carrinhoDeCompras;
using System.Reflection.Metadata;

namespace cliente
{
    public class Cliente
    {
        private string nome;
        private string email;
        private int numero_telefone;
        private string morada;
        private string metodoPagamento;
        private long numeroCartao;
        private carrinho_de_compras carrinho;
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public int Numero_telefone
        {
            get { return numero_telefone; }
            set { numero_telefone = value; }
        }
        public string metodo_pagamento
        {
            get { return metodoPagamento; }
            set { metodoPagamento = value; }
        }
        public long NumeroCartao
        {
            get { return numeroCartao; }
            set { numeroCartao = value; }
        }
        public Cliente(string nome, string email, int numero_telefone, string morada)
        {
            this.nome = nome;
            this.email = email;
            this.numero_telefone = numero_telefone;
            this.morada = morada;
            carrinho = new carrinho_de_compras();
        }
        public Cliente(string nome, string email, int numero_telefone, string morada, string metodoPagamento)
        {
            this.nome = nome;
            this.email = email;
            this.numero_telefone = numero_telefone;
            this.morada = morada;
            this.metodoPagamento = metodoPagamento;
            carrinho = new carrinho_de_compras();
        }
        public Cliente(string nome, string email, int numero_telefone, string morada, string metodoPagamento, long numeroCartao)
        {
            this.nome = nome;
            this.email = email;
            this.numero_telefone = numero_telefone;
            this.morada = morada;
            this.metodoPagamento = metodoPagamento;
            this.numeroCartao = numeroCartao;
            carrinho = new carrinho_de_compras();
        }
        public void verCarrinho()//mostra o carrinho de compras
        {
            carrinho.verProdutos();
        }
        public String info()
        {
            return "Nome: " + nome + "\nEmail: " + email + "\nNumero telefone: " + numero_telefone + "\nMorada: " + morada;
        }

        public void comprar()
        {
            carrinho.comprar();
        }
        public void historicoDeCompras()
        {
            carrinho.historicoDeCompras();
        }
        public void acabarCompra()
        {
            carrinho.comprar();
        }
        public double precoCarrinho()
        {
            return carrinho.precoCarrinho();
            
        }
        public void verProdutos()
        {
            carrinho.verProdutos();
        }

        public void adicionarProduto(Produto produto)
        {
            carrinho.adicionar(produto);
        }

        public void adicionarProdutoComprado(Produto produto)
        {
            carrinho.adicionarComprado(produto);
        }
    }
}
