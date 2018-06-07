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
            try
            {
                using (AgiliFoodContext db = new AgiliFoodContext())
                {
                    if (pesquisaMes != null)
                    {
                        pedidos = pedidos.Where(c => c.DataDeRegistro.Month == pesquisaMes);
                    }

                    if (pesquisaAno != null)
                    {
                        pedidos = pedidos.Where(c => c.DataDeRegistro.Year == pesquisaAno);
                    }
                    return pedidos.OrderBy(p => p.DataDeRegistro).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}