using AgileFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AgileFood.Controllers
{
    public class HomeController : BaseController
    {

        private AgiliFoodContext db = new AgiliFoodContext();

        public ActionResult Index()
        {
            var fornecedores = db.Fornecedores.Where(x => x.Ativo == true);
            return View(fornecedores.ToList());
        }

        public ActionResult Escolher(Fornecedor fornecedor)
        {
            if (fornecedor == null)
            {
                return HttpNotFound();
            }

            TempData["Fornecedor"] = fornecedor;
            return RedirectToAction("Adicionar", "Pedidos");
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Seja bem vindo(a)!";
            return View();
        }

    }
}