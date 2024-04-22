using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTarea.Aplicacion.Interfaces;
using AppTarea.Dominio.Modelos;
using AppTarea.Dominio.Response;
using AppTarea.Dominio.Interfaces.Repositorio;


namespace AppTarea.Aplicacion.Servicios
{
    public class LoginServicio : IServicioLogin<Login>
    {
        private readonly IRrepositorioLogin<Login> repoLogin;
        public LoginServicio(IRrepositorioLogin<Login> _repoLogin) {
            repoLogin = _repoLogin;
        }

        public async Task<Response<Usuario>> Agregar(Usuario entidad)
        {
            return await repoLogin.Agregar(entidad);
        }

        public async Task<Response<Usuario>> loguear(Login entidad)
        {
            return await repoLogin.loguear(entidad);    
        }


    }
}
