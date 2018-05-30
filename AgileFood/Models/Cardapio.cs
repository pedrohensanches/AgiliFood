using System;

namespace AgileFood.Models
{
    public class Cardapio
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime DataDeRegistro { get; set; }
        public bool Ativo { get; set; }
        public int FornecedorId { get; set; }
        public string SegundaFeira { get; set; }
        public string TercaFeira { get; set; }
        public string QuartaFeira { get; set; }
        public string QuintaFeira { get; set; }
        public string SextaFeira { get; set; }
        public string Sabado { get; set; }
        public string Domingo { get; set; }
        
        public virtual Fornecedor Fornecedor { get; set; }
    }
}