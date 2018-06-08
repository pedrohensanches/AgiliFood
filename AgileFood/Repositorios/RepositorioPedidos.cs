using AgileFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileFood.Repositorios
{
    public class RepositorioPedidos
    {
        public static List<Pedido> RetornaPedidos(IQueryable<Pedido> pedidos, int? pesquisaMes, int? pesquisaAno)
        {
            if (pesquisaMes != null) pedidos = pedidos.Where(c => c.DataDeRegistro.Month == pesquisaMes);
            if (pesquisaAno != null) pedidos = pedidos.Where(c => c.DataDeRegistro.Year == pesquisaAno);
            int idUsuarioLogado = RepositorioUsuarios.UsuarioLogado().Id;
            pedidos = pedidos.Where(c => c.FuncionarioId == idUsuarioLogado);
            return pedidos.OrderBy(p => p.DataDeRegistro).ToList();
        }
    }
}