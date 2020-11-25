using ServicioNegocio.EF;
using ServicioNegocio.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web.Mvc.Filters;

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
            Session["listaProvincias"] = "";
            List<ServicioNegocio.EF.Consorcio> consorcios = consorcioService.Listar(id);

            if ("".Equals(Session["listaProvincias"]))
            {
                List<Provincia> provincias = provinciaService.Listar();
                Session["listaProvincias"] = provincias;
            }
            
            return View(consorcios);
        }

        public ActionResult ViewCrear()
        {
            //List<Provincia> provincias = provinciaService.Listar();
            ViewBag.provincias = Session["listaProvincias"];
            return View();
        }

        [HttpPost]
        public ActionResult Guardar(ServicioNegocio.EF.Consorcio consorcio, string accion)
        {
            int id = (int)Session["idUser"];
            string vista = "Listar";

            switch (accion)
            {

                case "Guardar":
                    consorcio.IdUsuarioCreador = id;
                    consorcioService.Guardar(consorcio);
                    return RedirectToAction("Listar");

                case "GuardarCrear":
                    consorcio.IdUsuarioCreador = id;
                    consorcioService.Guardar(consorcio);
                    return RedirectToAction("ViewCrear");

                case "GuardarCrearUnidad":
                    return Redirect("/productos/lista");
            }
            return RedirectToAction(vista);
        }

        public ActionResult ViewEliminarConsorcio(string id) {

            TempData["idConsorcio"] = id;

            return View();
        }
        public ActionResult Eliminar(string id)
        {
            int idConsorcio = int.Parse((String) id);

            consorcioService.Eliminar(idConsorcio);

            return RedirectToAction("Listar");
        }

        //[SiteMapTitle("title")]
        public ActionResult ViewEditar(string id)
        {
            ServicioNegocio.EF.Consorcio consorcio = new ServicioNegocio.EF.Consorcio();
            int idConsorcio = int.Parse((String) id);

            consorcio= consorcioService.Buscar(idConsorcio);

            ViewBag.provincias = Session["listaProvincias"];

            //ViewData["Title"] = "Consorcio \"Villanova\"";

            return View(consorcio);
        }

        [HttpPost]
        public ActionResult GuardarEdicion(ServicioNegocio.EF.Consorcio consorcio)
        {
            int id = (int)Session["idUser"];
            consorcio.IdUsuarioCreador = id;

            consorcioService.Editar(consorcio);

            return RedirectToAction("Listar");
        }

        //Para el breadCrumb
        private static void SetConsorcioBreadcrumbTitle(string id)
        {
            ServicioNegocio.EF.Consorcio consorcio = new ServicioNegocio.EF.Consorcio();
            int idConsorcio = int.Parse((String)id);

            ConsorcioService consorcioService1 = new ConsorcioService();
            consorcio = consorcioService1.Buscar(idConsorcio);

            string nombreConsorcio = consorcio.Nombre;

            var node = SiteMaps.Current.CurrentNode;
            FindParentNode(node, "ConsorcioX", $"Consorcio \"{nombreConsorcio}\"");
        }

        private static void FindParentNode(ISiteMapNode node, string oldTitle, string newTitle)
        {
            if (node.Title == oldTitle)
            {
                node.Title = newTitle;
            }
            else
            {
                if (node.ParentNode != null)
                {
                    FindParentNode(node.ParentNode, oldTitle, newTitle);
                }
            }
        }

    }
}