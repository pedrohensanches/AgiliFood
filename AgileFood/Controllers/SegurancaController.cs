using AgileFood.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgileFood.Controllers
{
    public class SegurancaController : Controller
    {
        [HttpGet]
        public JsonResult AutenticacaoDeUsuario(string Email, string Senha)
        {
            if (RepositorioUsuarios.AutenticarUsuario(Email, Senha))
            {
                return Json(new { OK = true, Mensagem = "Redirecionando..." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { OK = false, Mensagem = "E-mail ou senha inválida, tente novamente!" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public void Logout()
        {
            RepositorioCookies.RemoveCookieAuthentication();
        }
    }
}