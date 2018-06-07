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
    public class FornecedoresController : BaseController
    {
        private AgiliFoodContext db = new AgiliFoodContext();

        // GET: Fornecedores
        public ActionResult Index()
        {
            var fornecedores = db.Fornecedores.Include(f => f.Responsavel);
            return View(fornecedores.ToList());
        }

        // GET: Fornecedores/Adicionar
        public ActionResult Adicionar()
        {
            ViewBag.ResponsavelId = new SelectList(db.Usuarios.Where(x => x.Tipo == TipoDeUsuario.Fornecedor), "Id", "Nome");
            return View();
        }

        // POST: Fornecedores/Adicionar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar([Bind(Include = "Id,Nome,CNPJ,Ativo")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                Usuario usuarioFornecedor = db.Usuarios.Find(fornecedor.ResponsavelId);
                usuarioFornecedor.Fornecedor = fornecedor;
                db.Fornecedores.Add(fornecedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ResponsavelId = new SelectList(db.Usuarios.Where(x => x.Tipo == TipoDeUsuario.Fornecedor), "Id", "Nome");
            return View(fornecedor);
        }

        // GET: Fornecedores/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = db.Fornecedores.Find(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ResponsavelId = new SelectList(db.Usuarios.Where(x => x.Tipo == TipoDeUsuario.Fornecedor), "Id", "Nome");
            return View(fornecedor);
        }

        // POST: Fornecedores/Editar/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nome,CNPJ,Ativo")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fornecedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ResponsavelId = new SelectList(db.Usuarios.Where(x => x.Tipo == TipoDeUsuario.Fornecedor), "Id", "Nome");
            return View(fornecedor);
        }

        // GET: Fornecedores/Deletar/5
        public ActionResult Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = db.Fornecedores.Find(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // POST: Fornecedores/Deletar/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult Confirmar(int id)
        {
            Fornecedor fornecedor = db.Fornecedores.Find(id);
            db.Fornecedores.Remove(fornecedor);
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

        [HttpPost]
        public JsonResult PesquisarUsuario(string pesquisa)
        {
            List<Usuario> resultado = db.Usuarios.Where(x => x.Nome.StartsWith(pesquisa)).Select(x => new Usuario
            {
                Id = x.Id,
                Nome = x.Nome
            }).ToList();
            return new JsonResult { Data = resultado, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}
