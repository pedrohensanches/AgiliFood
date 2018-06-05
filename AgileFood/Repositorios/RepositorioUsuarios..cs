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

    }
}