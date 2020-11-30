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

        public void Guardar(Gasto gasto)
        {
            DateTime today = DateTime.Now;
            gasto.FechaCreacion = today;
            contexto.Gasto.Add(gasto);
            contexto.SaveChanges();
        }
        
        public Gasto Buscar(int idGasto)
        {
            Gasto gasto = ObtenerPorId(idGasto);
            return gasto;
        }

        public Gasto ObtenerPorId(int idGasto)
        {
            Gasto gasto;
            gasto = contexto.Gasto.Find(idGasto);
            return gasto;
        }

        public void Editar(Gasto gasto)
        {
            Gasto gastoEditado = ObtenerPorId(gasto.IdGasto);
            gastoEditado.Nombre = gasto.Nombre;
            gastoEditado.Descripcion = gasto.Descripcion;
            gastoEditado.FechaGasto = gasto.FechaGasto;
            gastoEditado.AnioExpensa = gasto.AnioExpensa;
            gastoEditado.MesExpensa = gasto.MesExpensa;
            gastoEditado.ArchivoComprobante = "/Gastos/"+gasto.ArchivoComprobante;
            gastoEditado.Monto = gasto.Monto;
            contexto.SaveChanges();
        }



    }
}
