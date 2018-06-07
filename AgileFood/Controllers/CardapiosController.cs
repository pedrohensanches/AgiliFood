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
    public class CardapiosController : BaseController
    {
        private AgiliFoodContext db = new AgiliFoodContext();

        // GET: Cardapios
        public ActionResult Index()
        {
            var cardapios = db.Cardapios.Include(c => c.Fornecedor);
            return View(cardapios.ToList());
        }

        // GET: Cardapios/Adicionar
        public ActionResult Adicionar()
        {
            return View();
        }

        // POST: Cardapios/Adicionar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar([Bind(Include = "Id,Titulo,Ativo,SegundaFeira,TercaFeira,QuartaFeira,QuintaFeira,SextaFeira,Sabado,Domingo")] Cardapio cardapio)
        {
            if (ModelState.IsValid)
            {
                cardapio.DataDeRegistro = DateTime.Now;
                cardapio.Fornecedor = RepositorioFornecedores.RecuperaFornecedorLogado(db.Usuarios.Include(p => p.Fornecedor));
                db.Cardapios.Add(cardapio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cardapio);
        }

        // GET: Cardapios/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cardapio cardapio = db.Cardapios.Find(id);
            if (cardapio == null)
            {
                return HttpNotFound();
            }
            return View(cardapio);
        }

        // POST: Cardapios/Editar/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Titulo,Ativo,SegundaFeira,TercaFeira,QuartaFeira,QuintaFeira,SextaFeira,Sabado,Domingo")] Cardapio cardapio)
        {
            if (ModelState.IsValid)
            {
                cardapio.DataDeRegistro = DateTime.Now;
                db.Entry(cardapio).State = EntityState.Modified;
                //db.Entry(cardapio).Property(p => p.DataDeRegistro).IsModified = false;
                db.Entry(cardapio).Property(p => p.FornecedorId).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cardapio);
        }

        // GET: Cardapios/Deletar/5
        public ActionResult Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cardapio cardapio = db.Cardapios.Find(id);
            if (cardapio == null)
            {
                return HttpNotFound();
            }
            return View(cardapio);
        }

        // POST: Cardapios/Deletar/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult Confirmar(int id)
        {
            Cardapio cardapio = db.Cardapios.Find(id);
            db.Cardapios.Remove(cardapio);
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
