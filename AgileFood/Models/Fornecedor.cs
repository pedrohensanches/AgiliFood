using System.Collections.Generic;

namespace AgileFood.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public bool Ativo { get; set; }
        public virtual Usuario Responsavel { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
        public virtual ICollection<Cardapio> Cardapios { get; set; }
    }
}