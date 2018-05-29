using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgileFood.Models
{

    [Table("ItensDePedidos")]
    public class ItemPedido
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Id")]
        [Column("IdPedido", Order = 1)]
        [Display(Name = "Pedido")]
        public Pedido Pedido { get; set; }

        [Required]
        [ForeignKey("Id")]
        [Column("IdProduto", Order = 2)]
        [Display(Name = "Produto")]
        public Produto Produto { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a Quantidade")]
        public int Quantidade { get; set; }

    }
}