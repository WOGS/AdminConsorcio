using ServicioNegocio.EF;
using ServicioNegocio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioNegocio.Service
{
    public class ConsorcioService
    {
            ConsorcioRepository consorcioRepository;

            public ConsorcioService() {
                consorcioRepository = new ConsorcioRepository();
            }

            public List<Consorcio> Listar(int idUsuario)
            {
                List<Consorcio> consorcios = consorcioRepository.Listar(idUsuario);
                return consorcios;
            }

        public int Guardar(Consorcio consorcio)
        {
            int id = consorcioRepository.Guardar(consorcio);

            return id;
        }

        public void Eliminar(int idConsorcio)
        {
            consorcioRepository.Eliminar(idConsorcio);
        }

        public Consorcio Buscar(int idConsorcio)
        {
            Consorcio consorcio = new Consorcio();
            consorcio = consorcioRepository.Buscar(idConsorcio);

            return consorcio;
        }

        public void Editar(Consorcio consorcio)
        {
            consorcioRepository.Editar(consorcio);
        }

        public List<Consorcio> PaginarConsorcio(int posicion, ref int totalregistros, int idUsuario)
        {
            List<Consorcio> consorcios = consorcioRepository.PaginarConsorcio(posicion, ref totalregistros, idUsuario);

            return consorcios;
        }

        public int ContarUnidades(int Id)
        {
            int cantidad = consorcioRepository.ContarUnidades(Id);

            return cantidad;
        }

        public string getNombreById(int idConsorcio)
        {
            string nombreConsorcio = consorcioRepository.getNombreById(idConsorcio);

            return nombreConsorcio;
        }
    }
}
