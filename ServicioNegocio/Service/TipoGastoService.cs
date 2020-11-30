using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicioNegocio.EF;
using ServicioNegocio.Repository;

namespace ServicioNegocio.Service
{
    public class TipoGastoService
    {
        TipoGastoRepository tipoGastoRespository;

        public TipoGastoService()
        {
            tipoGastoRespository = new TipoGastoRepository();
        }

        public List<TipoGasto> Listar()
        {
            List<TipoGasto> tiposgastos = tipoGastoRespository.Listar();
            return tiposgastos;
        }
        
    }
}
