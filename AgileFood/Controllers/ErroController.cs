using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgileFood.Controllers
{
    public class ErroController : Controller
    {
        // GET: Erro403
        public ActionResult AcessoNegado()
        {
            return View();
        }
    }
}