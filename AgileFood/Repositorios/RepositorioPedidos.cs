using AgileFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileFood.Repositorios
{
    public class RepositorioPedidos
    {
        public static List<Pedido> RetornaPedidosDoFuncionarioLogado(IQueryable<Pedido> pedidos, int? pesquisaMes, int? pesquisaAno)
        {
            if (pesquisaMes != null) pedidos = pedidos.Where(c => c.DataDeRegistro.Month == pesquisaMes);
            if (pesquisaAno != null) pedidos = pedidos.Where(c => c.DataDeRegistro.Year == pesquisaAno);
            int idUsuarioLogado = RepositorioUsuarios.UsuarioLogado().Id;
            pedidos = pedidos.Where(c => c.FuncionarioId == idUsuarioLogado);
            return pedidos.OrderBy(p => p.DataDeRegistro).ToList();
        }

        public static List<Pedido> RetornaTodosPedidos(IQueryable<Pedido> pedidos, string pesquisaNome, int? pesquisaMes, int? pesquisaAno)
        {
            if (!String.IsNullOrEmpty(pesquisaNome)) pedidos = pedidos.Where(u => u.Funcionario.Nome.Contains(pesquisaNome));
            if (pesquisaMes != null) pedidos = pedidos.Where(c => c.DataDeRegistro.Month == pesquisaMes);
            if (pesquisaAno != null) pedidos = pedidos.Where(c => c.DataDeRegistro.Year == pesquisaAno);
            return pedidos.OrderBy(p => p.DataDeRegistro).ToList();
        }
    }
}