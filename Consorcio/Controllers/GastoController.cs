using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicioNegocio.Service;
using ServicioNegocio.EF;
using System.Text;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web.Mvc.Filters;

namespace Consorcio.Controllers
{
    public class GastoController : Controller
    {

        GastoService gastoService;
        TipoGastoService tipoGastoService;
        ConsorcioService consorcioService;

        public GastoController()
        {
            gastoService = new GastoService();
            tipoGastoService = new TipoGastoService();
            consorcioService = new ConsorcioService();
        }

        //GET: Gasto
        [SiteMapTitle("title")]
        public ActionResult Listar(string id, int? posicion)
        {
            ServicioNegocio.EF.Consorcio consorcio = new ServicioNegocio.EF.Consorcio();

            if (id != null)
            {
                if (Session["idConsorcio"] != null)
                {
                    id = (string)Session["idConsorcio"];
                } else
                {
                    Session["idConsorcio"] = id;
                }
            } else
            {
                id = (string)Session["idConsorcio"];
            }
            
            
            if (posicion == null)
            {
                posicion = 0;

            }

            int idConsorcio = int.Parse(id);            
            consorcio = consorcioService.Buscar(idConsorcio);
            Session["nombreConsorcio"] = consorcio.Nombre;
            int totalRegistros = 0;
            List<Gasto> gastos = gastoService.PaginarGastos(posicion.GetValueOrDefault(), ref totalRegistros, idConsorcio);
            ViewBag.TotalRegistros = totalRegistros;

            SetConsorcioBreadcrumbTitle(consorcio.Nombre, null);

            return View(gastos);
        }

        [SiteMapTitle("title")]
        public ActionResult ViewCrear(string mensaje)
        {

            if (!string.IsNullOrEmpty(mensaje))
            {
                ViewBag.mensaje = mensaje;
            } 
            CargarComboTipoGastoEnViewBag();
            ViewBag.nombreConsorcio = Session["nombreConsorcio"];
            string accion = "Crear Gasto";
            string nombreCon = (string)Session["nombreConsorcio"];
            SetConsorcioBreadcrumbTitle(nombreCon, null);
            SetGastoBreadcrumbTitle(null, accion);
            return View();
        }

        public void CargarComboTipoGastoEnViewBag()
        {
            List<TipoGasto> tiposgastos = tipoGastoService.Listar();
            ViewBag.tiposgastos = tiposgastos;
        }

        [HttpPost]
        public ActionResult Guardar(ServicioNegocio.EF.Gasto gasto, string accion)
        {
            String idConsorcio = Session["idConsorcio"].ToString();
            String idUser = Session["idUser"].ToString();

            if (ModelState.IsValid)
            {
                 switch (accion)
                 {
                    case "Guardar":
                        gasto.IdConsorcio = int.Parse(idConsorcio);
                        gasto.IdUsuarioCreador = int.Parse(idUser);
                        gasto.ArchivoComprobante = "/Gastos/" + gasto.ArchivoComprobante;
                        gastoService.Guardar(gasto);
                        return RedirectToAction("Listar");

                    case "GuardarCrear":
                        gasto.IdConsorcio = int.Parse(idConsorcio);
                        gasto.IdUsuarioCreador = int.Parse(idUser);
                        gasto.ArchivoComprobante = "/Gastos/" + gasto.ArchivoComprobante;
                        gastoService.Guardar(gasto);
                        return RedirectToAction("ViewCrear", new {mensaje = "El gasto "+ gasto.Nombre + " creado con éxito" });

                 }
            }
            ViewBag.nombreConsorcio = Session["nombreConsorcio"];
            CargarComboTipoGastoEnViewBag();
            return View("ViewCrear");
        }

        [SiteMapTitle("title")]
        public ActionResult ViewEditar(string id)
        {
            CargarComboTipoGastoEnViewBag();
            ServicioNegocio.EF.Gasto gasto = new ServicioNegocio.EF.Gasto();
            int idGasto = int.Parse((String)id);
            ViewBag.nombreConsorcio = Session["nombreConsorcio"];
             
            gasto = gastoService.Buscar(idGasto);
            Session["archivoComprobante"] = gasto.ArchivoComprobante;
            Session["idGasto"] = id;

            ViewBag.nombreGasto = gasto.Nombre;
            string accion = "Editando Gasto";
            string nombreCon = (string)Session["nombreConsorcio"];
            SetConsorcioBreadcrumbTitle(nombreCon, null);
            SetGastoBreadcrumbTitle(gasto.Nombre, accion);

            return View(gasto);
        }

        [HttpPost]
        public ActionResult GuardarEdicion(ServicioNegocio.EF.Gasto gasto, string accion)
        {
            String idConsorcio = Session["idConsorcio"].ToString();
            String idUser = Session["idUser"].ToString();
            String archivoComprobante = Session["archivoComprobante"].ToString();

            Gasto gastoEditado = gastoService.Buscar(gasto.IdGasto);
            gasto.IdConsorcio = int.Parse(idConsorcio);
            gasto.IdUsuarioCreador = int.Parse(idUser);

            switch (accion)
            {
                case "VerComprobante":
                    return VerComprobante(archivoComprobante);

                case "Guardar":
                    if (ModelState.IsValid)
                    {                        
                        gastoService.Editar(gasto);
                        return RedirectToAction(actionName: "Listar/" + gastoEditado.IdConsorcio.ToString());
                    } else
                    {
                        ViewBag.nombreConsorcio = Session["nombreConsorcio"];
                        CargarComboTipoGastoEnViewBag();
                        return RedirectToAction(actionName:"ViewEditar/"+ gastoEditado.IdGasto.ToString());
                    }                       
            }
            //string id = Session["idGasto"].ToString();
            return View("ViewEditar");            
        }

        public ActionResult VerComprobante(string archivoComprobante)
        {           

            string filename = archivoComprobante;
            string filepath = System.Web.HttpContext.Current.Server.MapPath("~") + filename;
            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            string contentType = MimeMapping.GetMimeMapping(filepath);

           
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = filename,
                Inline = true,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(filedata, contentType);
        }

        public ActionResult VerComprobanteDesdeListar(string id)
        {
            ServicioNegocio.EF.Gasto gasto = new ServicioNegocio.EF.Gasto();
            int idGasto = int.Parse((String)id);
            gasto = gastoService.Buscar(idGasto);
            return VerComprobante(gasto.ArchivoComprobante);

        }

        private static void SetConsorcioBreadcrumbTitle(string nombre, string accion)
        {
            string nombreConsorcio = nombre;
            var node = SiteMaps.Current.CurrentNode;
            if (accion != null)
            {
                FindParentNode(node, "ConsorcioX", $"Consorcio \" {nombreConsorcio}\" > {accion} ");
            }
            else
            {
                FindParentNode(node, "ConsorcioX", $"Consorcio \" {nombreConsorcio}\" ");
            }
        }
 
        private static void SetGastoBreadcrumbTitle(string nombre, string accion)
        {
            string nombreGasto = nombre;
            var node = SiteMaps.Current.CurrentNode;
            if (accion != null)
            {
                FindParentNode(node, "GastoX", $"Gasto \"{nombreGasto}\" > {accion}​​​​ ");
            }
            else
            {
                FindParentNode(node, "GastoX", $"Gasto \"{nombreGasto}​​​​\" ");
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


        public ActionResult ViewEliminarGasto(string id)
        {

            TempData["idGasto"] = id;

            return View();
        }
        public ActionResult Eliminar(string id)
        {
            int idGasto = int.Parse((String)id);

            consorcioService.Eliminar(idGasto);

            return RedirectToAction("Listar");
        }
    }
}
