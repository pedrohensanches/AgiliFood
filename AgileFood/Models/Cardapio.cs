using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgileFood.Models
{

    [Table("Cardapios")]
    public class Cardapio
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "É obrigatório informar o Título")]
        [StringLength(80, ErrorMessage = "O Título deve conter no máximo 80 caracteres")]
        public string Titulo { get; set; }

        [Required]
        [Display(Name = "Data de Registro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataDeRegistro { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [ForeignKey("Id")]
        [Column("IdFornecedor")]
        [Required(ErrorMessage = "É obrigatório informar o Fornecedor")]
        public Fornecedor Fornecedor { get; set; }

        private const string msgErroDescricao = "O campo de descrição deve conter no máximo 300 caracteres";

        [Display(Name = "Segunda-Feira")]
        [StringLength(300, ErrorMessage = msgErroDescricao)]
        public string SegundaFeira { get; set; }

        [Display(Name = "Terça-Feira")]
        [StringLength(300, ErrorMessage = msgErroDescricao)]
        public string TercaFeira { get; set; }

        [Display(Name = "Quarta-Feira")]
        [StringLength(300, ErrorMessage = msgErroDescricao)]
        public string QuartaFeira { get; set; }

        [Display(Name = "Quinta-Feira")]
        [StringLength(300, ErrorMessage = msgErroDescricao)]
        public string QuintaFeira { get; set; }

        [Display(Name = "Sexta-Feira")]
        [StringLength(300, ErrorMessage = msgErroDescricao)]
        public string SextaFeira { get; set; }

        [Display(Name = "Sábado")]
        [StringLength(300, ErrorMessage = msgErroDescricao)]
        public string Sabado { get; set; }

        [Display(Name = "Domingo")]
        [StringLength(300, ErrorMessage = msgErroDescricao)]
        public string Domingo { get; set; }

    }
}