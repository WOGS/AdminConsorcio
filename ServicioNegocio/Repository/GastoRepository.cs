using ServicioNegocio.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioNegocio.Repository
{
    class GastoRepository
    {
        Contexto contexto;

        public GastoRepository()
        {
            contexto = new Contexto();
        }

        public List<Gasto> Listar(int idConsorcio)
        {
            //se le pasa id del consorcio para obtener todos sus gastos
            List<Gasto> gastos = (from con in contexto.Gasto where con.IdConsorcio == idConsorcio select con).ToList();

            return gastos;
        }






    }
}
