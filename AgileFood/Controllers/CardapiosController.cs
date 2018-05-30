﻿using System;
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
    public class CardapiosController : Controller
    {
        private AgiliFoodContext db = new AgiliFoodContext();

        // GET: Cardapios
        public ActionResult Index()
        {
            var cardapios = db.Cardapios.Include(c => c.Fornecedor);
            return View(cardapios.ToList());
        }

        // GET: Cardapios/Details/5
        public ActionResult Details(int? id)
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

        // GET: Cardapios/Create
        public ActionResult Create()
        {
            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "Id", "Nome");
            return View();
        }

        // POST: Cardapios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,DataDeRegistro,Ativo,FornecedorId,SegundaFeira,TercaFeira,QuartaFeira,QuintaFeira,SextaFeira,Sabado,Domingo")] Cardapio cardapio)
        {
            if (ModelState.IsValid)
            {
                db.Cardapios.Add(cardapio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "Id", "Nome", cardapio.FornecedorId);
            return View(cardapio);
        }

        // GET: Cardapios/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "Id", "Nome", cardapio.FornecedorId);
            return View(cardapio);
        }

        // POST: Cardapios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,DataDeRegistro,Ativo,FornecedorId,SegundaFeira,TercaFeira,QuartaFeira,QuintaFeira,SextaFeira,Sabado,Domingo")] Cardapio cardapio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cardapio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "Id", "Nome", cardapio.FornecedorId);
            return View(cardapio);
        }

        // GET: Cardapios/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Cardapios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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
