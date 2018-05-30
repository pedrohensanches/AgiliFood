using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AgileFood.Models.Maps
{
    public sealed class ProdutoMap : EntityTypeConfiguration<Produto>
    {
        public ProdutoMap()
        {
            ToTable("Produtos");

            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(80);

            Property(x => x.Descricao)
                .HasMaxLength(300);

            Property(x => x.Valor)
                .IsRequired();

            Property(x => x.Disponivel)
                .IsRequired();

            Property(x => x.FornecedorId)
                .IsRequired();

            HasRequired(x => x.Fornecedor)
                .WithMany(x => x.Produtos)
                .HasForeignKey(c => c.FornecedorId);

            Property(x => x.CategoriaId)
                .IsRequired();

            HasRequired(x => x.Categoria)
                .WithMany(x => x.Produtos)
                .HasForeignKey(c => c.CategoriaId);

        }
    }
}