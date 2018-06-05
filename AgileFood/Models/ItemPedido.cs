﻿namespace AgileFood.Models
{
    public class ItemPedido
    {
        public int PedidoId { get; set; }
        public virtual Pedido Pedido { get; set; }
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }
        public int Quantidade { get; set; }

    }
}