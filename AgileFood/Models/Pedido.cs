using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgileFood.Models
{

    [Table("Pedidos")]
    public class Pedido
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        [ForeignKey("Id")]
        [Display(Name = "Funcionário")]
        public Usuario Funcionario { get; set; }

        [Display(Name = "Observações")]
        [StringLength(300, ErrorMessage = "A observação tem o limite de 300 caracteres")]
        public string Observacoes { get; set; }

        public virtual ICollection<ItemPedido> Itens { get; set; }

    }
}