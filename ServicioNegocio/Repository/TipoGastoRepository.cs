using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicioNegocio.EF;

namespace ServicioNegocio.Repository
{
    class TipoGastoRepository
    {
        Contexto contexto;

        public TipoGastoRepository()
        {
            contexto = new Contexto();
        }

        public List<TipoGasto> Listar()
        {
            //List<TipoGasto> tiposgastos = (from con in contexto.TipoGasto select con).ToList();
            return contexto.TipoGasto.ToList(); ;
        }
    }
}
