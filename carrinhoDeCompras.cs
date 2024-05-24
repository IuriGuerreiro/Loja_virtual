using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cliente;
using produto;
using carrinhoDeCompras;
using System.ComponentModel.Design;


namespace carrinhoDeCompras
{
    class carrinho_de_compras
    { // class carrinho de compras
        List<Produto> produtos = new List<Produto>();
        List<bool> comprado = new List<bool>();

        public void adicionar(Produto produto)
        {
            produtos.Add(produto);
            comprado.Add(false);
        }

        public void comprar()
        {
            foreach (var prudoto in comprado)
            {
                if (prudoto == false)
                {
                    produtos[comprado.IndexOf(prudoto)].AtualizarDisponiveis(-1);
                    comprado[comprado.IndexOf(prudoto)] = true;
                }
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
