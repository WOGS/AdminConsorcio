using ServicioNegocio.EF;
using ServicioNegocio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioNegocio.Service
{
    public class UnidadService
    {
        UnidadRepository unidadRepository;


        public UnidadService()
        {
            unidadRepository = new UnidadRepository();
        }

        public List<Unidad> Listar(int idConsorcio)
        {
            List<Unidad> unidades = unidadRepository.Listar(idConsorcio);
            return unidades;
        }

        public void Guardar(Unidad unidad)
        {
            unidadRepository.Guardar(unidad);
        }

        public void Eliminar(int idUnidad)
        {
            unidadRepository.Eliminar(idUnidad);
        }

        public Unidad Buscar(int idUnidad)
        {
            Unidad unidad = new Unidad();
            unidad = unidadRepository.Buscar(idUnidad);

            return unidad;
        }

        public void Editar(Unidad unidad)
        {
            unidadRepository.Editar(unidad);
        }

    }
}
