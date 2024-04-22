using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTarea.Dominio.Interfaces;

namespace AppTarea.Aplicacion.Interfaces
{
    public interface IServicioLogin<TEntidad>
        :ILogin<TEntidad>, IAgregar<Usuario>
    {
    }
}
