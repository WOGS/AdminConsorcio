using Microsoft.Ajax.Utilities;
using ServicioNegocio.EF;
using ServicioNegocio.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web.Mvc.Filters;

namespace Consorcio.Controllers
{
    public class UnidadController : Controller
    {
        UnidadService unidadService;
        ConsorcioService consorcioService;

        public UnidadController()
        {
            unidadService = new UnidadService();
            consorcioService = new ConsorcioService();
        }

        // GET: Unidad
        [SiteMapTitle("title")]
        public ActionResult Listar(string id, int? posicion)
        {
            string consorcioEditado = "";
            ServicioNegocio.EF.Consorcio consorcio = new ServicioNegocio.EF.Consorcio();
            if (posicion == null)
            {
                posicion = 0;
            }

            if (id != null)
            {
                if (Session["idConsorcio"] != null)
                {
                    id = (string)Session["idConsorcio"];
                }
                else
                {
                    Session["idConsorcio"] = id;
                }
            }
            else
            {
                id = (string)Session["idConsorcio"];
            }

            int totalregistros = 0;
            int idConsorcio = int.Parse(id);

            List<Unidad> unidades = unidadService.PaginarUnidades(posicion.GetValueOrDefault(), ref totalregistros, idConsorcio);
            ViewBag.TotalRegistros = totalregistros;
            consorcio = consorcioService.Buscar(idConsorcio);

            Session["nombreConsorcio"] = consorcio.Nombre;

            SetConsorcioBreadcrumbTitle(consorcio.Nombre, null);

            return View(unidades);
        }

        [SiteMapTitle("title")]
        public ActionResult ViewCrear()
        {
            string accion = "Crear Unidad";
            string nombreCon = (string)Session["nombreConsorcio"];
            SetConsorcioBreadcrumbTitle(nombreCon, null);
            SetUnidadBreadcrumbTitle(null, accion);
           
            return View();
        }

        [HttpPost]
        public ActionResult Guardar(ServicioNegocio.EF.Unidad unidad, string accion)
        {
            String idConsorcio = Session["idConsorcio"].ToString();
            String id = Session["idUser"].ToString();

            string vista = "Listar";
            if (ModelState.IsValid) 
            { 
                switch (accion)
                {
                    case "Guardar":
                        unidad.IdUsuarioCreador = int.Parse(id);
                        unidad.IdConsorcio = int.Parse(idConsorcio);
                        unidadService.Guardar(unidad);
                        return RedirectToAction("Listar/"+ idConsorcio);
                    case "GuardarCrearOtro":

                        unidad.IdUsuarioCreador = int.Parse(id);
                        unidad.IdConsorcio = int.Parse(idConsorcio);
                        unidadService.Guardar(unidad);                       

                        return RedirectToAction("ViewCrear");
                }
            }
            return RedirectToAction(vista);
        }

        public ActionResult ViewEliminarUnidad(string id)
        {

            TempData["idUnidad"] = id;

            return View();
        }

        public ActionResult Eliminar(String id)
        {    
            int idUnidad = int.Parse((String)id);
            unidadService.Eliminar(idUnidad);
       
            return RedirectToAction("Listar");
        }

        [SiteMapTitle("title")]
        public ActionResult ViewEditar(string id)
        {
            ServicioNegocio.EF.Unidad unidad = new ServicioNegocio.EF.Unidad();
            int idUnidad = int.Parse((String)id);

            unidad = unidadService.Buscar(idUnidad);

            ViewBag.nombreUni = unidad.Nombre;
            string accion = "Editando Unidad";
            string nombreCon = (string)Session["nombreConsorcio"];
            SetConsorcioBreadcrumbTitle(nombreCon, null);
            SetUnidadBreadcrumbTitle(unidad.Nombre, accion);

            return View(unidad);
        }

        [HttpPost]
        public ActionResult GuardarEdicion(ServicioNegocio.EF.Unidad unidad)
        {
            Unidad unidadEditada = unidadService.Buscar(unidad.IdUnidad);
            if (ModelState.IsValid)
            {
               
            unidadService.Editar(unidad);
       
            
            }
       return RedirectToAction(actionName: "Listar/" + unidadEditada.IdConsorcio.ToString());
        }

        private static void SetConsorcioBreadcrumbTitle(string nombre, string accion)
        {
            
            string nombreConsorcio = nombre;
            var node = SiteMaps.Current.CurrentNode;
            if (accion != null)
            {
                FindParentNode(node, "ConsorcioX", $"Consorcio \"{nombreConsorcio}\" > {accion} ");
            }
            else 
            {
                FindParentNode(node, "ConsorcioX", $"Consorcio \"{nombreConsorcio}\" ");
            }
        }

        private static void SetUnidadBreadcrumbTitle(string nombre, string accion)
        {
            string nombreUnidad = nombre;
            var node = SiteMaps.Current.CurrentNode;
            if (accion != null)
            {
                FindParentNode(node, "UnidadX", $"Unidad \"{nombreUnidad}\" > {accion} ");
            }
            else 
            {
                FindParentNode(node, "UnidadX", $"Unidad \"{nombreUnidad}\" ");
            }

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