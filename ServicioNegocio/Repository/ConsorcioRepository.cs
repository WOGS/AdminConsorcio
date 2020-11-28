using ServicioNegocio.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioNegocio.Repository
{
    public class ConsorcioRepository
    {
        Contexto contexto;

        public ConsorcioRepository() {
            contexto = new Contexto();
        }

        public List<Consorcio> Listar(int idUsuario) {

            List<Consorcio> consorcios = (from con in contexto.Consorcio where con.IdUsuarioCreador == idUsuario select con).ToList();

            return consorcios;
        }

        public void Guardar(Consorcio consorcio)
        {
            DateTime today = DateTime.Now;
            consorcio.FechaCreacion = today;
            contexto.Consorcio.Add(consorcio);
            contexto.SaveChanges();
        }
                
        public void Eliminar(int idConsorcio)
        {
            Consorcio consorcio = ObtenerPorId(idConsorcio);
            if (consorcio != null)
            {
                contexto.Consorcio.Remove(consorcio);
            }

            contexto.SaveChanges();
        }

        public Consorcio Buscar(int idConsorcio)
        {
            Consorcio consorcio = ObtenerPorId(idConsorcio);

            return consorcio;
        }

        public Consorcio ObtenerPorId(int idConsorcio){
            Consorcio consorcio;
            consorcio = contexto.Consorcio.Find(idConsorcio);
            return consorcio;
        }

        public void Editar(Consorcio consorcio){
            Consorcio consorcioUpdate = ObtenerPorId(consorcio.IdConsorcio);
            consorcioUpdate.Nombre = consorcio.Nombre;
            consorcioUpdate.IdProvincia = consorcio.IdProvincia;
            consorcioUpdate.Ciudad = consorcio.Ciudad;
            consorcioUpdate.Calle = consorcio.Calle;
            consorcioUpdate.Altura = consorcio.Altura;
            consorcioUpdate.DiaVencimientoExpensas = consorcio.DiaVencimientoExpensas;
        contexto.SaveChanges();
        }

        public List<Consorcio> PaginarConsorcio(int posicion, ref int totalregistros, int idUsuario)
        {
            totalregistros = this.contexto.Consorcio.Count();
            var consulta = (from con in contexto.Consorcio where con.IdUsuarioCreador == idUsuario select con).OrderBy(z => z.IdConsorcio).Skip(posicion).Take(4).ToList();
            List<Consorcio> consorcios = consulta;
            return consorcios;
        }
    }
}
