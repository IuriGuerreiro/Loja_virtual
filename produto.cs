using System;
using System.Collections.Generic;
using cliente;
using produto;
using carrinhoDeCompras;



namespace produto
{
    public class Produto
    {
        private string nome;
        private int id;
        private int stock;
        private double preco;
        private string categoria;

        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }
        public double Preco
        {
            get { return preco; }
            set { preco = value; }
        }
        public Produto(string nome, int id, int stock, double preco, string categoria)
        {
            this.nome = nome;
            this.id =id;
            this.stock = stock;
            this.preco = preco;
            this.categoria = categoria;
        }
        public void AtualizarDisponiveis(int quantidade)
        {
            stock += quantidade;
        }
        public String info()
        {
            return "Nome: " + nome + "\nStock: " + stock + "\nPreco: " + preco + "\nCategoria: " + categoria;
        }
    }

}
