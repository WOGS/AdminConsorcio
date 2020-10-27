﻿using ServicioNegocio.EF;
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

        public void Guardar(Consorcio consorcio)
        {
            consorcioRepository.Guardar(consorcio);
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
    }
}
