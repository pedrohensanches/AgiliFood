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
        [Display(Name = "Data de Registro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataDeRegistro { get; set; }

        [Required]
        [ForeignKey("Id")]
        [Column("IdUsuario")]
        [Display(Name = "Funcionário")]
        public Usuario Funcionario { get; set; }

        [Display(Name = "Observações")]
        [StringLength(300, ErrorMessage = "A observação tem o limite de 300 caracteres")]
        public string Observacoes { get; set; }

        public virtual ICollection<ItemPedido> Itens { get; set; }

    }
}