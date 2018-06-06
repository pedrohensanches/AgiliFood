using AgileFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileFood.Repositorios
{
    public class RepositorioCardapios
    {
        public static List<Tuple<string, string>> GetCardapioDaSemana(int id)
        {
            try
            {
                using (AgiliFoodContext db = new AgiliFoodContext())
                {
                    Cardapio cardapio = (Cardapio)db.Cardapios.Where(g => (g.Fornecedor.Id == id) && (g.Ativo)).First();
                    List<Tuple<string, string>> lista = new List<Tuple<string, string>>();
                    if (cardapio.SegundaFeira != null) lista.Add(new Tuple<string, string>("Segunda-Feira", cardapio.SegundaFeira));
                    if (cardapio.TercaFeira != null) lista.Add(new Tuple<string, string>("Terça-Feira", cardapio.TercaFeira));
                    if (cardapio.QuartaFeira != null) lista.Add(new Tuple<string, string>("Quarta-Feira", cardapio.QuartaFeira));
                    if (cardapio.QuintaFeira != null) lista.Add(new Tuple<string, string>("Quinta-Feira", cardapio.QuintaFeira));
                    if (cardapio.SextaFeira != null) lista.Add(new Tuple<string, string>("Sexta-Feira", cardapio.SextaFeira));
                    if (cardapio.Sabado != null) lista.Add(new Tuple<string, string>("Sábado", cardapio.Sabado));
                    if (cardapio.Domingo != null) lista.Add(new Tuple<string, string>("Domingo", cardapio.Domingo));
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}