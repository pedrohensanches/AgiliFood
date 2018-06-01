using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AgileFood.Models;

namespace AgileFood.Controllers
{
    public class PedidosController : Controller
    {
        private AgiliFoodContext db = new AgiliFoodContext();

        // GET: Pedidos
        public ActionResult Index()
        {
            var pedidos = db.Pedidos.Include(p => p.Funcionario);
            return View(pedidos.ToList());
        }

        // GET: Pedidos/Details/5
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

        // GET: Pedidos/Adicionar
        public ActionResult Adicionar()
        {
            Fornecedor fornecedor = TempData["Fornecedor"] as Fornecedor;
            ViewBag.Produtos = new SelectList(db.Produtos.
                Where(g => (g.Fornecedor.Id == fornecedor.Id) && (g.Disponivel)), "Id", "Nome");

            ViewBag.FuncionarioId = new SelectList(db.Usuarios, "Id", "Nome");
            return View();
        }

        // POST: Pedidos/Adicionar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar([Bind(Include = "Id,DataDeRegistro,Observacoes,FuncionarioId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Pedidos.Add(pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FuncionarioId = new SelectList(db.Usuarios, "Id", "Nome", pedido.FuncionarioId);
            return View(pedido);
        }

        // GET: Pedidos/Editar/5
        public ActionResult Editar(int? id)
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
            ViewBag.FuncionarioId = new SelectList(db.Usuarios, "Id", "Nome", pedido.FuncionarioId);
            return View(pedido);
        }

        // POST: Pedidos/Editar/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,DataDeRegistro,Observacoes,FuncionarioId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FuncionarioId = new SelectList(db.Usuarios, "Id", "Nome", pedido.FuncionarioId);
            return View(pedido);
        }

        // GET: Pedidos/Deletar/5
        public ActionResult Deletar(int? id)
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

        // POST: Pedidos/Deletar/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult Confirmar(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedido);
            db.SaveChanges();
            return RedirectToAction("Index");
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
