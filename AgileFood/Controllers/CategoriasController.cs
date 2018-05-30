using AgileFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgileFood.Controllers
{
    public class CategoriasController : Controller
    {

        private AgiliFoodContext db = new AgiliFoodContext();

        // GET: Categorias
        public ActionResult Index()
        {
            var categorias = db.Categorias;
            return View(categorias.ToList());
        }
    }
}