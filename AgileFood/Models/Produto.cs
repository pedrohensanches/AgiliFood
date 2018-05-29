using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgileFood.Models
{

    [Table("Produtos")]
    public class Produto
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o Nome do Produto")]
        [StringLength(80, ErrorMessage = "O Nome do Produto deve conter no máximo 80 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(300, ErrorMessage = "A Descrição deve conter no máximo 300 caracteres")]
        public string Descricao { get; set; }

        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Preço Inválido (Informe apenas dois digitos decimais após o ponto)")]
        [Required(ErrorMessage = "É obrigatório informar o Valor do Produto")]
        public double Valor { get; set; }

        [Required]
        [Display(Name = "Disponível")]
        public bool Disponivel { get; set; }

        [ForeignKey("Id")]
        [Column("IdFornecedor")]
        [Required(ErrorMessage = "É obrigatório informar o Fornecedor do Produto")]
        public Fornecedor Fornecedor { get; set; }

        [ForeignKey("Id")]
        [Column("IdCategoria")]
        [Required(ErrorMessage = "É obrigatório informar a Categoria do Produto")]
        public Categoria Categoria { get; set; }

        public virtual ICollection<ItemPedido> Itens { get; set; }

    }
}