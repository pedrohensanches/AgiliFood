using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgileFood.Models;
using AgileFood.Repositorios;

namespace AgileFood.Filtros
{
    [HandleError]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AutorizacaoDeAcesso : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext FiltroDeContexto)
        {
            var Controller = FiltroDeContexto.ActionDescriptor.ControllerDescriptor.ControllerName;
            var Action = FiltroDeContexto.ActionDescriptor.ActionName;

            if (Controller != "Home" || Action != "Login")
            {
                Usuario usuarioLogado = RepositorioUsuarios.VerificaSeOUsuarioEstaLogado();
                if (usuarioLogado == null)
                {
                    FiltroDeContexto.RequestContext.HttpContext.Response.Redirect("/Home/Login?Url=" + FiltroDeContexto.HttpContext.Request.Url.LocalPath);
                }
                else
                {
                    if (Controller == "Pedidos")
                    {
                        if (usuarioLogado.Tipo == TipoDeUsuario.Fornecedor) FiltroDeContexto.RequestContext.HttpContext.Response.Redirect("/acesso-negado");
                    }
                }
            }
        }
    }
}