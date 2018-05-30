using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

            HasKey(x => new { x.PedidoId, x.ProdutoId });

            //HasKey(x => x.Id)
            //    .Property(x => x.Id)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
            //    .IsRequired();

            Property(x => x.Quantidade)
                .IsRequired();

            Property(x => x.PedidoId)
                .IsRequired();

            Property(x => x.ProdutoId)
                .IsRequired();

            HasRequired(c => c.Pedido)
                .WithMany(c => c.Itens)
                .HasForeignKey(c => c.PedidoId)
                .WillCascadeOnDelete(false);

            HasRequired(c => c.Produto)
                .WithMany(c => c.Itens)
                .HasForeignKey(c => c.ProdutoId)
                .WillCascadeOnDelete(false);

        }
    }
}