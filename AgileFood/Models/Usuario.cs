using System.Collections.Generic;

namespace AgileFood.Models
{
    public enum TipoDeUsuario : int
    {
        Administrador = 0,
        Financeiro = 1,
        Fornecedor = 2,
        Funcionario = 3
    }

    public class Usuario
    {
        public Usuario()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public TipoDeUsuario Tipo { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}