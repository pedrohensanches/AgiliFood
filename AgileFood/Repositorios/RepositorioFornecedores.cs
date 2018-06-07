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

        internal static List<Fornecedor> RetornaFornecedores(string pesquisaNome, int? pesquisaStatus, IQueryable<Fornecedor> fornecedores)
        {
            fornecedores = fornecedores.AsQueryable();
            switch (pesquisaStatus)
            {
                case 0: fornecedores = fornecedores.Where(u => u.Ativo == false); break;
                case 1: fornecedores = fornecedores.Where(u => u.Ativo == true); break;
            }
            if (!String.IsNullOrEmpty(pesquisaNome))
            {
                fornecedores = fornecedores.Where(u => u.Nome.Contains(pesquisaNome));
            }
            return fornecedores.OrderBy(u => u.Nome).ToList();
        }
    }
}