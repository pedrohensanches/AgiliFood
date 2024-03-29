﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AgileFood.Models;
using AgileFood.Repositorios;

namespace AgileFood.Controllers
{
    public class UsuariosController : BaseController
    {
        private AgiliFoodContext db = new AgiliFoodContext();

        // GET: Usuarios
        public ActionResult Index(string pesquisaNome, int? pesquisaTipo)
        {
            List<Usuario> usuarios = RepositorioUsuarios.RetornaUsuarios(pesquisaNome, pesquisaTipo);
            if (Request.IsAjaxRequest()) return PartialView("_Usuarios", usuarios);
            return View(usuarios);
        }

        // GET: Usuarios/Cadastrar
        public ActionResult Cadastrar()
        {
            return View();
        }

        // POST: Usuarios/Cadastrar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar([Bind(Include = "Id,Nome,CPF,Email,Senha,Telefone,Tipo")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                //usuario.Senha = RepositorioCriptografia.Criptografar(usuario.Senha);
                usuario.Senha = FormsAuthentication.HashPasswordForStoringInConfigFile(usuario.Senha, "sha1");
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuarios/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Editar/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nome,CPF,Email,Telefone,Tipo")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.Entry(usuario).Property(u => u.Senha).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Deletar/5
        public ActionResult Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Deletar/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult Confirmar(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
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
