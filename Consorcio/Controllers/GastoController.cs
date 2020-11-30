using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServicioNegocio.Service;
using ServicioNegocio.EF;
using System.Text;

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
        public ActionResult Listar(string id, int? posicion)
        {
            string gastoEditado = "";

            if (id != null)
            {
                Session["idConsorcio"] = id;
                gastoEditado = id;
            }
            else
            {
                gastoEditado = (String)Session["idConsorcio"];
            }

            if (posicion == null)
            {
                posicion = 0;

            }

            int idConsorcio = int.Parse(gastoEditado);            
            Session["nombreConsorcio"] = consorcioService.getNombreById(idConsorcio);
            int totalRegistros = 0;
            List<Gasto> gastos = gastoService.PaginarGastos(posicion.GetValueOrDefault(), ref totalRegistros, idConsorcio);
            ViewBag.TotalRegistros = totalRegistros;

            //List<Gasto> gastos = gastoService.Listar(idConsorcio);

            return View(gastos);
        }

        public ActionResult ViewCrear()
        {
            CargarComboTipoGastoEnViewBag();
            ViewBag.nombreConsorcio = Session["nombreConsorcio"];
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
                        //gastoService.Guardar(gasto);
                        NuevoGastoOkNotify(gasto.Nombre);
                        return RedirectToAction("ViewCrear");


                 }
            }
            ViewBag.nombreConsorcio = Session["nombreConsorcio"];
            CargarComboTipoGastoEnViewBag();
            return View("ViewCrear");
        }

        public ActionResult ViewEditar(string id)
        {
            CargarComboTipoGastoEnViewBag();
            ServicioNegocio.EF.Gasto gasto = new ServicioNegocio.EF.Gasto();
            int idGasto = int.Parse((String)id);
            ViewBag.nombreConsorcio = Session["nombreConsorcio"];           

            gasto = gastoService.Buscar(idGasto);
            Session["archivoComprobante"] = gasto.ArchivoComprobante;
            Session["idGasto"] = id;
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

        public ActionResult NuevoGastoOkNotify(string nombreGasto)
        {
            return JavaScript("<script language='javascript' type='text/javascript'>alert('Gasto " + nombreGasto + "creado con éxito');</script>");
        }
    }
}
