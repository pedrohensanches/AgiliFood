using AgileFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileFood.Repositorios
{
    public class RepositorioFornecedores
    {
        public static Fornecedor RecuperaFornecedorLogado(IQueryable<Usuario> usuarios)
        {
            var Usuario = HttpContext.Current.Request.Cookies["UserCookieAuthentication"];
            int iDUsuario = Convert.ToInt32(RepositorioCriptografia.Descriptografar(Usuario.Values["IDUsuario"]));
            var usuario = usuarios.Where(u => u.Id == iDUsuario).SingleOrDefault();
            return usuario.Fornecedor;
        }
    }
}