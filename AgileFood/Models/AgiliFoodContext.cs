using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AgileFood.Models
{
    public class AgiliFoodContext : DbContext
    {

        public AgiliFoodContext() : base("name=AgiliFoodContext") { }

        public DbSet<Cardapio> Cardapio { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<ItemPedido> ItemPedido { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

    }
}