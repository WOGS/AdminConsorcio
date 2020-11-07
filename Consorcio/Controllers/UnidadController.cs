using Microsoft.Ajax.Utilities;
using ServicioNegocio.EF;
using ServicioNegocio.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace Consorcio.Controllers
{
    public class UnidadController : Controller
    {
        UnidadService unidadService;
        
        public UnidadController()
        {
            unidadService = new UnidadService();

        }
        // GET: Unidad
        public ActionResult Listar(string id)
        {
            string consorcioEditado = "";

            if (id!=null)
            {
                Session["idConsorcio"] = id;
                consorcioEditado = id;
            } else
            {
                consorcioEditado = (String)Session["idConsorcio"];
            }
           
            int idConsorcio = int.Parse(consorcioEditado);

            List<Unidad> unidades = unidadService.Listar(idConsorcio);

            return View(unidades);
        }

        public ActionResult ViewCrear()
        {

            return View();
        }



        [HttpPost]
        public ActionResult Guardar(ServicioNegocio.EF.Unidad unidad, string accion)
        {
            String idConsorcio = Session["idConsorcio"].ToString();
            String id = Session["idUser"].ToString();

            string vista = "Listar";

            switch (accion)

            {
                case "Guardar":
                    unidad.IdUsuarioCreador = int.Parse(id);
                    unidad.IdConsorcio = int.Parse(idConsorcio);
                    unidadService.Guardar(unidad);
                    return RedirectToAction("Listar");
                case "GuardarCrearOtro":

                    unidad.IdUsuarioCreador = int.Parse(id);
                    unidad.IdConsorcio = int.Parse(idConsorcio);
                    unidadService.Guardar(unidad);                       

                    return RedirectToAction("ViewCrear");

            }

            return RedirectToAction(vista);
        }

        public ActionResult ViewEliminar(int idUnidad)
        {
            // int idUnidad = int.Parse((String)TempData["idUnidad"]);

            unidadService.Eliminar(idUnidad);

            return RedirectToAction("Listar");
        }

        public ActionResult ViewEditar(string id)
        {
            ServicioNegocio.EF.Unidad unidad = new ServicioNegocio.EF.Unidad();
            int idUnidad = int.Parse((String)id);

            unidad = unidadService.Buscar(idUnidad);

            return View(unidad);
        }

        [HttpPost]
        public ActionResult GuardarEdicion(ServicioNegocio.EF.Unidad unidad)
        {
            Unidad unidadEditada = unidadService.Buscar(unidad.IdUnidad);
            unidadService.Editar(unidad);
            //string idConsorcioDeUnidadEditada = unidadEditada.IdConsorcio.ToString();

            return RedirectToAction(actionName: "Listar/" + unidadEditada.IdConsorcio.ToString());
        }

    }
}