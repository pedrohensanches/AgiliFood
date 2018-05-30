using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AgileFood.Models.Maps
{
    public sealed class UsuarioMaps : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMaps()
        {
            ToTable("Usuarios");

            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(databaseGeneratedOption: DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(80);

            Property(x => x.CPF)
                .IsRequired()
                .HasMaxLength(11);

            Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(50);

            Property(x => x.Senha)
                .IsRequired()
                .HasMaxLength(15);

            Property(x => x.Telefone)
                .HasMaxLength(15);

            Property(x => x.Tipo)
                .IsRequired();
            
            HasOptional(x => x.Fornecedor)
                .WithOptionalPrincipal(l => l.Responsavel);

        }
    }
}