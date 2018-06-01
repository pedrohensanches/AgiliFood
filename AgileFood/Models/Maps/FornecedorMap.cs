using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AgileFood.Models.Maps
{
    public sealed class FornecedorMap : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorMap()
        {
            ToTable("Fornecedores");

            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(80);

            Property(x => x.CNPJ)
                .IsRequired()
                .HasMaxLength(14);

            Property(x => x.Ativo)
                .IsRequired();

            HasRequired(x => x.Responsavel)
                .WithMany()
                .HasForeignKey(c => c.ResponsavelId);

            //HasRequired(x => x.Responsavel)
            //    .WithMany()
            //    .HasForeignKey(c => c.ResponsavelId);

            //HasOptional(x => x.Responsavel)
            //.WithOptionalDependent(c => c.Fornecedor)
            //.WillCascadeOnDelete(false);

            //Property(x => x.ResponsavelId)
            //    .IsRequired();

            //HasRequired(x => x.Responsavel)
            //    .WithOptional(x => x.Fornecedor);

        }
    }
}