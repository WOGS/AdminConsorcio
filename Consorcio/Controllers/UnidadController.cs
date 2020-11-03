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
        string consorcioEditado = "";

        public UnidadController()
        {
            unidadService = new UnidadService();

        }
        // GET: Unidad
        public ActionResult Listar(string id)
        {
            Session["idConsorcio"] = "";
            Session["idConsorcio"] = id;

            consorcioEditado = id;
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
            
            String idConsorcio = Session["idConsorcio"].ToString();
            String id = Session["idUser"].ToString();

            unidad.IdUsuarioCreador = int.Parse(id);
            unidad.IdConsorcio = int.Parse(idConsorcio);

            unidadService.Guardar(unidad);

            return RedirectToAction("Listar/"+idConsorcio.ToString());
        }

/*        public ActionResult ViewEliminarUnidad(string idUnidad) {

            TempData["idUnidad"] = "id";

            return View();
        }*/


        public ActionResult ViewEliminar(int idUnidad)
        {
           // int idUnidad = int.Parse((String)TempData["idUnidad"]);

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
           Unidad unidadEditada = unidadService.Buscar(unidad.IdUnidad);
            unidadService.Editar(unidad);
            //string idConsorcioDeUnidadEditada = unidadEditada.IdConsorcio.ToString();

            return RedirectToAction(actionName: "Listar/"+ unidadEditada.IdConsorcio.ToString());
        }
    }
}