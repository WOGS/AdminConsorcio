using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicioNegocio.Service;
using ServicioNegocio.EF;

namespace Consorcio.Controllers
{
    public class GastoController : Controller
    {

        GastoService gastoService;
        TipoGastoService tipoGastoService;

        public GastoController()
        {
            gastoService = new GastoService();
            tipoGastoService = new TipoGastoService();
        }

        //GET: Gasto
        public ActionResult Listar(string id)
        {
            int idConsorcio = int.Parse(id);

            List<Gasto> gastos = gastoService.Listar(idConsorcio);

            return View(gastos);
        }

        public ActionResult ViewCrear()
        {
            return View();
        }

        public void CargarComboTipoGastoEnViewBag()
        {
            List<TipoGasto> tiposgastos = tipoGastoService.Listar();
            ViewBag.tiposgastos = tiposgastos;
        }

    }
}
