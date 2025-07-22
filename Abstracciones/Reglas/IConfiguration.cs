using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Reglas
{
    public interface IConfigurationApp
    {
     
        public string ObtenerMetodo(string seccion, string nombre);
        public string ObtenerUrl(string llave);
    }
}
