using Consorcio.Models;
using Newtonsoft.Json;
using ServicioNegocio.EF;
using ServicioNegocio.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        public ActionResult Listar(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 0;
            }

            int id = (int)Session["idUser"];
            Session["listaProvincias"] = "";

            if ("".Equals(Session["listaProvincias"]))
            {
                List<Provincia> provincias = provinciaService.Listar();
                Session["listaProvincias"] = provincias;
            }

            int totalregistros = 0;
            List<ServicioNegocio.EF.Consorcio> consorcios = consorcioService.PaginarConsorcio(posicion.GetValueOrDefault(), ref totalregistros, id);
            ViewBag.TotalRegistros = totalregistros;
            return View(consorcios);
        }

        public ActionResult ViewCrear()
        {
            //List<Provincia> provincias = provinciaService.Listar();
            ViewBag.provincias = Session["listaProvincias"];

            ConsorcioModel consorcioModel = new ConsorcioModel();

            consorcioModel.provincias = (List<Provincia>)Session["listaProvincias"];

            return View(consorcioModel);
        }

        [HttpPost]
        public ActionResult Guardar(Models.ConsorcioModel consorcio, string accion)
        {
            int id = (int)Session["idUser"];
            string vista = "Listar";

            ServicioNegocio.EF.Consorcio consorcioEF = new ServicioNegocio.EF.Consorcio();

            consorcioEF.Nombre = consorcio.Nombre;
            consorcioEF.IdUsuarioCreador = id;
            consorcioEF.Altura = consorcio.Altura;
            consorcioEF.Calle = consorcio.Calle;
            consorcioEF.Ciudad = consorcio.Ciudad;
            consorcioEF.DiaVencimientoExpensas = consorcio.DiaVencimientoExpensas;
            consorcioEF.IdProvincia = consorcio.idProvincia;
            int idNuevoConsorcio = consorcioService.Guardar(consorcioEF);

            switch (accion)
            {
                case "Guardar":
                    return RedirectToAction("Listar");

                case "GuardarCrear":
                    return RedirectToAction("ViewCrear");

                case "GuardarCrearUnidad":
                    Session["idConsorcio"] = idNuevoConsorcio;
                    return Redirect("/Unidad/ViewCrear");
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

        public ActionResult ViewEditar(string id)
        {
            ServicioNegocio.EF.Consorcio consorcio = new ServicioNegocio.EF.Consorcio();
            int idConsorcio = int.Parse((String) id);

            consorcio= consorcioService.Buscar(idConsorcio);

            int contarUnidades = consorcioService.ContarUnidades(idConsorcio);

            ViewBag.contarUnidades = contarUnidades;

            ViewBag.provincias = Session["listaProvincias"];

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

        public ActionResult Expensas(string id)
        {
            var url = $"https://localhost:44321/api/Expensa/" + id;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            List<Expensa> result = new List<Expensa>();

            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        result = JsonConvert.DeserializeObject<List<Expensa>>(responseBody);
                       // string resultado = JsonConvert.DeserializeObject<string>(responseBody);
                        //TempData["cantGolesEquipo"] = resultado;
                        //TempData["equipoSel"] = id;
                    }
                }
            }
            return View(result);
        }
    }
}