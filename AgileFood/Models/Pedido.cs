using System;
using System.Collections.Generic;

namespace AgileFood.Models
{
    public class Pedido
    {
        public Pedido()
        {
            Itens = new HashSet<ItemPedido>();
        }

        public int Id { get; set; }
        public DateTime DataDeRegistro { get; set; }
        public string Observacoes { get; set; }
        public double ValorTotal { get; set; }
        public int FuncionarioId { get; set; }

        public virtual Usuario Funcionario { get; set; }
        public virtual ICollection<ItemPedido> Itens { get; set; }
    }
}