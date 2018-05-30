using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AgileFood.Models.Maps
{
    public sealed class CardapioMap : EntityTypeConfiguration<Cardapio>
    {
        public CardapioMap()
        {
            ToTable("Cardapios");

            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(x => x.Titulo)
                .IsRequired()
                .HasMaxLength(80);

            Property(x => x.DataDeRegistro)
                .IsRequired();

            Property(x => x.Ativo)
                .IsRequired();

            Property(x => x.SegundaFeira)
                .HasMaxLength(300);

            Property(x => x.TercaFeira)
                .HasMaxLength(300);

            Property(x => x.QuartaFeira)
                .HasMaxLength(300);

            Property(x => x.QuintaFeira)
                .HasMaxLength(300);

            Property(x => x.SextaFeira)
                .HasMaxLength(300);

            Property(x => x.Sabado)
                .HasMaxLength(300);

            Property(x => x.Domingo)
                .HasMaxLength(300);

            Property(x => x.FornecedorId)
                .IsRequired();
            
            HasRequired(x => x.Fornecedor)
                .WithMany(x => x.Cardapios)
                .HasForeignKey(c => c.FornecedorId);

        }
    }
}