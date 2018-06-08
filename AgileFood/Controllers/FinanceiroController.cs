using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AgileFood.Models;
using AgileFood.Repositorios;

namespace AgileFood.Controllers
{
    public class FinanceiroController : BaseController
    {
        private AgiliFoodContext db = new AgiliFoodContext();

        // GET: Financeiro
        public ActionResult Index(string pesquisaNome, int? pesquisaMes, int? pesquisaAno)
        {
            List<Pedido> pedidos = RepositorioPedidos.RetornaTodosPedidos(db.Pedidos.Include(p => p.Funcionario).Include(p => p.Itens), pesquisaNome, pesquisaMes, pesquisaAno);
            if (Request.IsAjaxRequest()) return PartialView("_Pedidos", pedidos);
            Session["Pedido"] = null;
            return View(pedidos);
        }

        // GET: Financeiro/Details/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}