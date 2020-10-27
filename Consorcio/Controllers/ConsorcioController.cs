using ServicioNegocio.EF;
using ServicioNegocio.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consorcio.Controllers
{
    public class ConsorcioController : Controller
    {
        ConsorcioService consorcioService;
        ProvinciaService provinciaService;


        public ConsorcioController()
        {
            consorcioService = new ConsorcioService();
            provinciaService = new ProvinciaService();

        }
        // GET: Consorcio
        public ActionResult Listar()
        {
            int id = (int)Session["idUser"];
            List<ServicioNegocio.EF.Consorcio> consorcios = consorcioService.Listar(id);
           // List<Provincia> provincias = provinciaService.Listar();
            
            return View(consorcios);
        }

        public ActionResult ViewCrear()
        {
            List<Provincia> provincias = provinciaService.Listar();
            ViewBag.provincias = provincias;

            return View();
        }

        [HttpPost]
        public ActionResult Guardar(ServicioNegocio.EF.Consorcio consorcio)
        {
            int id = (int)Session["idUser"];
            consorcio.IdUsuarioCreador = id;

            consorcioService.Guardar(consorcio);

            return RedirectToAction("Listar");
        }

        public ActionResult ViewEliminarConsorcio(string id) {

            TempData["idConsorcio"] = "id";

            return View();
        }
        public ActionResult Eliminar(){
            int idConsorcio = int.Parse((String)TempData["idConsorcio"]);

            consorcioService.Eliminar(idConsorcio);

            return RedirectToAction("Listar");
        }

        public ActionResult ViewEditar(string id)
        {
            ServicioNegocio.EF.Consorcio consorcio = new ServicioNegocio.EF.Consorcio();
            int idConsorcio = int.Parse((String) id);

            consorcio= consorcioService.Buscar(idConsorcio);

            return View(consorcio);
        }

        [HttpPost]
        public ActionResult GuardarEdicion(ServicioNegocio.EF.Consorcio consorcio)
        {
            consorcioService.Editar(consorcio);

            return RedirectToAction("Listar");
        }
    }
}