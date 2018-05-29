using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgileFood.Models
{

    public enum TipoDeUsuario : int
    {
        Administrador = 0,
        Financeiro = 1,
        Fornecedor = 2,
        Funcionario = 3
    }

    [Table("Usuarios")]
    public class Usuario
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o Nome")]
        [StringLength(80, ErrorMessage = "O Nome deve conter no máximo 80 caracteres")]
        public string Nome { get; set; }

        [Index(IsUnique = true)]
        [Required(ErrorMessage = "É obrigatório informar o CPF")]
        [StringLength(11, ErrorMessage = "O CPF deve conter no máximo 11 caracteres numéricos")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o Tipo De Usuário")]
        public TipoDeUsuario Tipo { get; set; }

        [Index(IsUnique = true)]
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "É obrigatório informar o E-mail")]
        [StringLength(50, ErrorMessage = "O E-mail deve conter no máximo 50 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a Senha")]
        [StringLength(15, ErrorMessage = "A Senha deve conter no máximo 15 caracteres")]
        public string Senha { get; set; }

        [StringLength(15, ErrorMessage = "O Telefone deve conter no máximo 15 caracteres numéricos")]
        public string Telefone { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }

    }
}