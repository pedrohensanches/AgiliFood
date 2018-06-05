using AgileFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AgileFood.Repositorios
{
    public class RepositorioUsuarios
    {

        public static bool AutenticarUsuario(string Email, string Senha)
        {
            var SenhaCriptografada = FormsAuthentication.HashPasswordForStoringInConfigFile(Senha, "sha1");
            try
            {
                using (AgiliFoodContext db = new AgiliFoodContext())
                {
                    var queryAutenticaUsuarios = db.Usuarios.Where(x => x.Email == Email && x.Senha == SenhaCriptografada).SingleOrDefault();

                    if (queryAutenticaUsuarios == null)
                    {
                        return false;
                    }
                    else
                    {
                        RepositorioCookies.RegistraCookieAutenticacao(queryAutenticaUsuarios.Id);
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Usuario RecuperaUsuarioPorID(int Id)
        {
            try
            {
                using (AgiliFoodContext db = new AgiliFoodContext())
                {
                    var usuario = db.Usuarios.Where(u => u.Id == Id).SingleOrDefault();
                    return usuario;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Usuario VerificaSeOUsuarioEstaLogado()
        {
            var Usuario = HttpContext.Current.Request.Cookies["UserCookieAuthentication"];
            if (Usuario == null)
            {
                return null;
            }
            else
            {
                int iDUsuario = Convert.ToInt32(RepositorioCriptografia.Descriptografar(Usuario.Values["IDUsuario"]));

                var usuarioRetornado = RecuperaUsuarioPorID(iDUsuario);
                return usuarioRetornado;

            }
        }

        public static Usuario UsuarioLogado()
        {
            var Usuario = HttpContext.Current.Request.Cookies["UserCookieAuthentication"];
            if (Usuario != null)
            {
                int iDUsuario = Convert.ToInt32(RepositorioCriptografia.Descriptografar(Usuario.Values["IDUsuario"]));

                var usuarioRetornado = RecuperaUsuarioPorID(iDUsuario);
                return usuarioRetornado;
            }
            return null;
        }

    }
}