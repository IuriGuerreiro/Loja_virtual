using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cliente;
using produto;
using carrinhoDeCompras;
using System.ComponentModel.Design;
using System.Data;
using Microsoft.Data.Sqlite;


namespace carrinhoDeCompras
{
    class carrinho_de_compras
    { // class carrinho de compras
        List<Produto> produtos = new List<Produto>();
        List<bool> comprado = new List<bool>();
        string connectionDB = "Data Source=loja_virtual.db";
        public void adicionar(Produto produto)
        {
            produtos.Add(produto);
            comprado.Add(false);
        }

        public void comprar()//compra os produtos 
        {
            int i = 0;
            foreach (var prudo in comprado)
            {
                if (prudo == false)
                {
                    comprado[i] = true;
                    produtos[i].Stock -= 1;
                    using (var con = new SqliteConnection("Data Source=loja_virtual.db"))
                    {
                        con.Open();
                        var cmd1 = new SqliteCommand("UPDATE produtos SET Stock = @Stock WHERE Id = @Id", con);
                        cmd1.Parameters.AddWithValue("@Stock", produtos[i].Stock);
                        cmd1.Parameters.AddWithValue("@Id", produtos[i].Id);
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                }
                i++;
            }
        }
        public double precoCarrinho()
        {
            double precototal = 0;
            int i= 0;
            foreach (var prudo in comprado)
            {
                if (prudo == false)
                {
                    precototal += produtos[i].Preco;
                    i++;
                }
            }
            return precototal;
        }
        public void verProdutos()
        {
            Console.WriteLine("\nProdutos no carrinho\n");
            for (int i = 0; i < produtos.Count; i++)
            {
                if (comprado[i] == false)
                {
                    Console.WriteLine($"Nome: {produtos[i].Nome}, preco {produtos[i].Preco}");
                }
            }
        }

        public void historicoDeCompras()
        {
            for (int i = 0; i < produtos.Count; i++)
            {
                if (comprado[i] == true)
                {
                    Console.WriteLine(produtos[i].Nome + " Foi comprado");
                }
            }
        }
        public void adicionarComprado(Produto produto)
        {
            produtos.Add(produto);
            comprado.Add(true);
        }
    }

}
