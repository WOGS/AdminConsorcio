using ServicioNegocio.EF;
using ServicioNegocio.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServicioNegocio.Models;

namespace ApiExpensas.Controllers
{
    public class ExpensaController : ApiController
    {
        ExpensasService expensasService;
        ConsorcioService consorcioService;

        public ExpensaController()
        {
            expensasService = new ExpensasService();
            consorcioService = new ConsorcioService();
        }
        [HttpGet]
        public ExpensaModel CalcularExpensa(int id)
        {
            ExpensaModel expensaModel = new ExpensaModel();
           
            expensaModel = expensasService.calcularExpensas(id);
            int cantidadUnidades = consorcioService.ContarUnidades(id);
            expensaModel.cantidadUnidades = cantidadUnidades;
            
            return expensaModel;
        }
    }
}
