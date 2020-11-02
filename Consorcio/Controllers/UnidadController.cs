using ServicioNegocio.EF;
using ServicioNegocio.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consorcio.Controllers
{
    public class UnidadController : Controller
    {
        UnidadService  unidadService;


        public UnidadController()
        {
            unidadService = new UnidadService();

        }
        // GET: Consorcio
        public ActionResult Listar(string id)
        {
            int idConsorcio = int.Parse(id);

            List<Unidad> unidades = unidadService.Listar(idConsorcio);
            
            return View(unidades);
        }

        public ActionResult ViewCrear()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Guardar(ServicioNegocio.EF.Unidad unidad)
        {
            int id = (int)Session["idUser"];
            unidad.IdUsuarioCreador = id;

            unidadService.Guardar(unidad);

            return RedirectToAction("Listar");
        }

        public ActionResult ViewEliminarUnidad(string id) {

            TempData["idUnidad"] = "id";

            return View();
        }
        public ActionResult Eliminar(){
            int idUnidad = int.Parse((String)TempData["idUnidad"]);

            unidadService.Eliminar(idUnidad);

            return RedirectToAction("Listar");
        }

        public ActionResult ViewEditar(string id)
        {
            ServicioNegocio.EF.Unidad unidad = new ServicioNegocio.EF.Unidad();
            int idUnidad = int.Parse((String) id);

            unidad= unidadService.Buscar(idUnidad);

            return View(unidad);
        }

        [HttpPost]
        public ActionResult GuardarEdicion(ServicioNegocio.EF.Unidad unidad)
        {
            unidadService.Editar(unidad);

            return RedirectToAction("Listar");
        }
    }
}