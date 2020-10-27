using ServicioNegocio.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioNegocio.Repository
{
    public class UsuarioRepository
    {
        Contexto contexto;
        public UsuarioRepository()
        {
            contexto = new Contexto();
        }
        public Usuario login(Usuario usuario){

            Usuario user = new Usuario();
            user = buscarUsuario(usuario);
                if (user.Email != null) {
                    Usuario userUpdate = new Usuario();
                    DateTime today = DateTime.Now;
                    userUpdate = contexto.Usuario.Find(user.IdUsuario);
                    userUpdate.FechaUltLogin = today;
                    contexto.SaveChanges();
                }
                return user;                       
        }

        public String Alta(Usuario usuario)
        {
            Usuario usu = new Usuario();
            usu = buscarUsuario(usuario);
            String mensaje = "";

            if (usu.Email==null)
            {
                DateTime today = DateTime.Now;
                usuario.FechaRegistracion = today;
                contexto.Usuario.Add(usuario);
                contexto.SaveChanges();
                mensaje = "ok";
            }
            else {
                mensaje = "error";
            }

            return mensaje;
        }

        public Usuario buscarUsuario(Usuario usuario) {
            Usuario user = new Usuario();
            try
            {
            user = (from us in contexto.Usuario where us.Email == usuario.Email && us.Password == usuario.Password select us).First();

            }
             catch{
            }
            return user;
        }
    }
}
