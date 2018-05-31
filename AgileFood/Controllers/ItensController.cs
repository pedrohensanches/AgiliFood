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
    public class ItensController : Controller
    {
        private AgiliFoodContext db = new AgiliFoodContext();

        // GET: Itens
        public ActionResult Index()
        {
            var itensDePedidos = db.ItensDePedidos.Include(i => i.Pedido).Include(i => i.Produto);
            return View(itensDePedidos.ToList());
        }

        // GET: Itens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemPedido itemPedido = db.ItensDePedidos.Find(id);
            if (itemPedido == null)
            {
                return HttpNotFound();
            }
            return View(itemPedido);
        }

        // GET: Itens/Create
        public ActionResult Create()
        {
            ViewBag.PedidoId = new SelectList(db.Pedidos, "Id", "Observacoes");
            ViewBag.ProdutoId = new SelectList(db.Produtos, "Id", "Nome");
            return View();
        }

        // POST: Itens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PedidoId,ProdutoId,Quantidade")] ItemPedido itemPedido)
        {
            if (ModelState.IsValid)
            {
                db.ItensDePedidos.Add(itemPedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PedidoId = new SelectList(db.Pedidos, "Id", "Observacoes", itemPedido.PedidoId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "Id", "Nome", itemPedido.ProdutoId);
            return View(itemPedido);
        }

        // GET: Itens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemPedido itemPedido = db.ItensDePedidos.Find(id);
            if (itemPedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.PedidoId = new SelectList(db.Pedidos, "Id", "Observacoes", itemPedido.PedidoId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "Id", "Nome", itemPedido.ProdutoId);
            return View(itemPedido);
        }

        // POST: Itens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PedidoId,ProdutoId,Quantidade")] ItemPedido itemPedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemPedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PedidoId = new SelectList(db.Pedidos, "Id", "Observacoes", itemPedido.PedidoId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "Id", "Nome", itemPedido.ProdutoId);
            return View(itemPedido);
        }

        // GET: Itens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemPedido itemPedido = db.ItensDePedidos.Find(id);
            if (itemPedido == null)
            {
                return HttpNotFound();
            }
            return View(itemPedido);
        }

        // POST: Itens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemPedido itemPedido = db.ItensDePedidos.Find(id);
            db.ItensDePedidos.Remove(itemPedido);
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
