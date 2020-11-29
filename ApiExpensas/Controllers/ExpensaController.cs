using ServicioNegocio.EF;
using ServicioNegocio.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
        public List<Expensa> CalcularExpensa(int id)
        {
            List<Expensa> expensas = new List<Expensa>();

            expensas = expensasService.calcularExpensas(id);

            int gasto = expensasService.gastoTotalMes(id);

            int cantidadUnidades = consorcioService.ContarUnidades(id);

            return expensas;
        }
    }
}
