using ServicioNegocio.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioNegocio.Repository
{
    public class ProvinciaRepository
    {
        Contexto contexto;

        public ProvinciaRepository() {
            contexto = new Contexto();
        }

       public List<Provincia> Listar() {
            List<Provincia> provincias = contexto.Provincia.ToList();
            return provincias;
        }
    }
}
