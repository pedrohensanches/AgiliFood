using System.Collections.Generic;

namespace AgileFood.Models
{

    public enum Categoria : int
    {
        Marmitex = 0,
        Bebidas = 1,
        Sobremesas = 2,
        Outros = 3
    }

    public class Produto
    {
        public Produto()
        {
            Itens = new HashSet<ItemPedido>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public bool Disponivel { get; set; }
        public int FornecedorId { get; set; }
        public Categoria Categoria { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }
        public virtual ICollection<ItemPedido> Itens { get; set; }
    }
}