using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consorcio.Controllers
{
    public class HomeController : Controller
    {

        public HomeController() { 
        }
        // GET: Home
        public ActionResult Inicio()
        {
            Session["idUser"] = "";
            return View();
        }

        public ActionResult Ingresar()
        {
            return View();
        }


        public ActionResult Registrarse()
        {
            return View();
        }

        public ActionResult Salir()
        {
            return View();
        }
    }
}