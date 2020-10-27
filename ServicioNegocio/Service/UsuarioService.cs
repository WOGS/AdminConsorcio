using ServicioNegocio.EF;
using ServicioNegocio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioNegocio.Service
{
   public class UsuarioService
    {
        UsuarioRepository usuarioRepository;

        public UsuarioService() {
            usuarioRepository = new UsuarioRepository();
        }
        public Usuario login(Usuario usuario) {
            Usuario usuarioExist = usuarioRepository.login(usuario);

            return usuarioExist;
        }

        public string Alta(Usuario usuario)
        {
            String mensaje = usuarioRepository.Alta(usuario);

            return mensaje;
        }
    }
}
