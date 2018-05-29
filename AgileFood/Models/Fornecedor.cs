using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgileFood.Models
{

    [Table("Fornecedores")]
    public class Fornecedor
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o Nome do Restaurante Fornecedor")]
        [StringLength(80, ErrorMessage = "O Nome deve conter no máximo 80 caracteres")]
        public string Nome { get; set; }

        [Index(IsUnique = true)]
        [Required(ErrorMessage = "É obrigatório informar o CNPJ")]
        [StringLength(14, ErrorMessage = "O CNPJ deve conter no máximo 14 caracteres numéricos")]
        public string CNPJ { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [ForeignKey("Id")]
        [Column("IdUsuario")]
        [Display(Name = "Responsável")]
        [Required(ErrorMessage = "É obrigatório informar o Usuário Responsável")]
        public Usuario Responsavel { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }

        public virtual ICollection<Cardapio> Cardapios { get; set; }

    }
}