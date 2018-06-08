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

        public static int TotalPedidosNoMes()
        {
            try
            {
                using (AgiliFoodContext db = new AgiliFoodContext())
                {
                    int mesAtual = DateTime.Now.Month;
                    return db.Pedidos.Where(c => c.DataDeRegistro.Month == mesAtual).Count();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static double ValorTotalPedidosNoMes()
        {
            try
            {
                using (AgiliFoodContext db = new AgiliFoodContext())
                {
                    int mesAtual = DateTime.Now.Month;
                    List<Pedido> pedidos = db.Pedidos.Where(c => c.DataDeRegistro.Month == mesAtual).ToList();
                    double valor = 0;
                    foreach (Pedido pedido in pedidos)
                    {
                        valor += pedido.ValorTotal;
                    }
                    return valor;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static double TotalPedidosHoje()
        {
            try
            {
                using (AgiliFoodContext db = new AgiliFoodContext())
                {
                    int dia = DateTime.Now.Day;
                    int mes = DateTime.Now.Month;
                    int ano = DateTime.Now.Year;
                    return db.Pedidos.Where(c => (c.DataDeRegistro.Day == dia) && (c.DataDeRegistro.Month == mes) && (c.DataDeRegistro.Year == ano)).Count();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static double ValorTotalPedidosHoje()
        {
            try
            {
                using (AgiliFoodContext db = new AgiliFoodContext())
                {
                    int dia = DateTime.Now.Day;
                    int mes = DateTime.Now.Month;
                    int ano = DateTime.Now.Year;
                    List<Pedido> pedidos = db.Pedidos.Where(c => (c.DataDeRegistro.Day == dia) && (c.DataDeRegistro.Month == mes) && (c.DataDeRegistro.Year == ano)).ToList();
                    double valor = 0;
                    foreach (Pedido pedido in pedidos)
                    {
                        valor += pedido.ValorTotal;
                    }
                    return valor;
                }
            }
            catch (Exception)
            {
                return 0;
            }
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