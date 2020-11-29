using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Consorcio.Models
{
    public class UsuarioModel
    {
        [Required(ErrorMessage = "Ingresar un Email")]
        [EmailAddress(ErrorMessage = "Ingresar un Email valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingresar una contraseña")]
        public string Password { get; set; }

    }
}