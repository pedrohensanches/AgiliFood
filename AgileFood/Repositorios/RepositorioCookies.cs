using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgileFood.Repositorios
{
    public class RepositorioCookies
    {

        public static void RegistraCookieAutenticacao(long IDUsuario)
        {
            //Criando um objeto cookie
            HttpCookie UserCookie = new HttpCookie("UserCookieAuthentication");

            //Setando o ID do usuário no cookie
            UserCookie.Values["IDUsuario"] = AgileFood.Repositorios.RepositorioCriptografia.Criptografar(IDUsuario.ToString());

            //Definindo o prazo de vida do cookie
            UserCookie.Expires = DateTime.Now.AddDays(1);

            //Adicionando o cookie no contexto da aplicação
            HttpContext.Current.Response.Cookies.Add(UserCookie);
        }

        public static void RemoveCookieAuthentication()
        {
            HttpContext.Current.Response.Cookies["UserCookieAuthentication"].Expires = DateTime.Now.AddDays(-1);
        }

    }
}