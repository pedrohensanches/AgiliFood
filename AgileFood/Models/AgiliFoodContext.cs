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

        public DbSet<Cardapio> Cardapios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<ItemPedido> ItensDePedidos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}