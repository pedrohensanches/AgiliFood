using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AgileFood.Models.Maps
{
    public sealed class ItemPedidoMap : EntityTypeConfiguration<ItemPedido>
    {
        public ItemPedidoMap()
        {
            ToTable("ItensDePedidos");

            Property(x => x.Quantidade)
                .IsRequired();

            Property(x => x.PedidoId)
                .IsRequired();

            Property(x => x.ProdutoId)
                .IsRequired();

        }
    }
}