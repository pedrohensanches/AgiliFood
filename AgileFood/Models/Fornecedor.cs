using System.Collections.Generic;

namespace AgileFood.Models
{
    public class Fornecedor
    {
        public Fornecedor()
        {
            Produtos = new HashSet<Produto>();
            Cardapios = new HashSet<Cardapio>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public bool Ativo { get; set; }
        public int ResponsavelId { get; set; }

        public virtual Usuario Responsavel { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
        public virtual ICollection<Cardapio> Cardapios { get; set; }
    }
}