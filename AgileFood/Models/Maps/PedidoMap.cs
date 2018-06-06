using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AgileFood.Models.Maps
{
    public sealed class PedidoMap : EntityTypeConfiguration<Pedido>
    {
        public PedidoMap()
        {
            ToTable("Pedidos");

            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(x => x.DataDeRegistro)
                .IsRequired();

            Property(x => x.Observacoes)
                .HasMaxLength(300);

            Property(x => x.FuncionarioId)
                .IsRequired();

            Property(x => x.ValorTotal)
                .IsRequired();

            HasRequired(x => x.Funcionario)
                .WithMany(x => x.Pedidos)
                .HasForeignKey(c => c.FuncionarioId);

        }
    }
}