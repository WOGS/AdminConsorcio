using ServicioNegocio.EF;
using ServicioNegocio.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consorcio.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioService usuarioService;

        public UsuarioController() {
            usuarioService = new UsuarioService();
        }

        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            Session["MsjError"] = "";
            Session["idUser"] = "";
            Usuario usuarioExist = usuarioService.login(usuario);

            if (usuarioExist.Email!= null)
            {
                Session["idUser"] = usuarioExist.IdUsuario;
                return Redirect("/Consorcio/listar");
            }
            else {
                Session["MsjError"] = "Email y/o Contraseña inválidos";
                return Redirect("/Home/Ingresar");
            }
               
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Alta(Usuario usuario)
        {
            String mensaje = "";
            String ruta = "";
            Session["MsjError"] = "";

            mensaje = usuarioService.Alta(usuario);

            if (mensaje.Equals("error")) {

                Session["MsjError"] = "El mail ya se encuentra en uso, pruebe utilizando otro";
                ruta = "/Usuario/Registrar";
            }
            else {
                ruta = "/Home/Ingresar";
            }

            return Redirect(ruta);
        }

        public ActionResult Salir()
        {
            Session["idUser"] = null;
            return Redirect("/Home/Inicio");
        }
    }
}