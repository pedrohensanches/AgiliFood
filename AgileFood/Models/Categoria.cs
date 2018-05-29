using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgileFood.Models
{

    [Table("Categorias")]
    public class Categoria
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o Nome da Categoria")]
        [StringLength(50, ErrorMessage = "O Nome da Categoria deve conter no máximo 50 caracteres")]
        public string Nome { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }

    }
}