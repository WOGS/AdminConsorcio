using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicioNegocio.Repository;
using ServicioNegocio.EF;

namespace ServicioNegocio.Service
{
    public class GastoService
    {
        GastoRepository gastoRepository;


        public GastoService()
        {
            gastoRepository = new GastoRepository();
        }

        public List<Gasto> Listar(int idConsorcio)
        {
            List<Gasto> gastos = gastoRepository.Listar(idConsorcio);
            return gastos;
        }

        public void Guardar(Gasto gasto)
        {
            gastoRepository.Guardar(gasto);
        }

        public Gasto Buscar(int idGasto)
        {
            Gasto gasto = new Gasto();
            gasto = gastoRepository.Buscar(idGasto);
            return gasto;
        }

        public void Editar(Gasto gasto)
        {
            gastoRepository.Editar(gasto);
        }

        public List<Gasto> PaginarGastos(int posicion, ref int totalRegistros, int idConsorcio)
        {
            List<Gasto> gastos = gastoRepository.PaginarGastos(posicion, ref totalRegistros, idConsorcio);
            return gastos;
        }
    }
}
