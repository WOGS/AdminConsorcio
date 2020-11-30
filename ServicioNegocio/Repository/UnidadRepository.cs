using ServicioNegocio.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioNegocio.Repository
{
    class UnidadRepository
    {
        Contexto contexto;

        public UnidadRepository()
        {
            contexto = new Contexto();
        }

        public List<Unidad> Listar(int idConsorcio)
        {
            // se le debe pasar el id de consorcio para traer todas las unidades del consorcio
           List<Unidad> unidades = (from con in contexto.Unidad where con.IdConsorcio == idConsorcio select con).ToList();

            return unidades;
        }

        public void Guardar(Unidad unidad)
        {

            Unidad uni = new Unidad();
       //     uni = Buscar(unidad.IdUnidad);
            String mensaje = "";

             DateTime today = DateTime.Now;
             unidad.FechaCreacion = today;
             contexto.Unidad.Add(unidad);
             contexto.SaveChanges();
            mensaje = "ok";
            
           
        }

        public void Eliminar(int idUnidad)
        {
            Unidad unidad = ObtenerPorId(idUnidad);
            if (unidad != null)
            {
                contexto.Unidad.Remove(unidad);
            }

            contexto.SaveChanges();
        }

        public Unidad Buscar(int idUnidad)
        {
            Unidad unidad = ObtenerPorId(idUnidad);

            return unidad;
        }

        public Unidad ObtenerPorId(int idUnidad)
        {
            Unidad unidad;
            unidad = contexto.Unidad.Find(idUnidad);
            return unidad;
        }

        public void Editar(Unidad unidad)
        {
            Unidad unidadUpdate = ObtenerPorId(unidad.IdUnidad);
            unidadUpdate.Nombre = unidad.Nombre;
            unidadUpdate.NombrePropietario = unidad.NombrePropietario;
            unidadUpdate.ApellidoPropietario = unidad.ApellidoPropietario;
            unidadUpdate.EmailPropietario = unidad.EmailPropietario;
            unidadUpdate.Superficie = unidad.Superficie;
            contexto.SaveChanges();
        }

        public List<Unidad> PaginarUnidades(int posicion, ref int totalregistros, int idConsorcio)
        {
            totalregistros = this.contexto.Unidad.Count();
            var consulta = (from con in contexto.Unidad where con.IdConsorcio == idConsorcio select con)
                .OrderBy(z => z.IdUnidad).Skip(posicion).Take(4).ToList();
            List<Unidad> unidades = consulta;
            return unidades;
        }

    }
}
