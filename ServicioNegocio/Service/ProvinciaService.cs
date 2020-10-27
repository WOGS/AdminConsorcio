using ServicioNegocio.EF;
using ServicioNegocio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ServicioNegocio.Service
{
    public class ProvinciaService
    {
        ProvinciaRepository provinciaRepository;

        public ProvinciaService() {

            provinciaRepository = new ProvinciaRepository();
        }

        public List<Provincia> Listar() {

            List<Provincia> provincias = provinciaRepository.Listar();

            return provincias;
        }

    }
}
