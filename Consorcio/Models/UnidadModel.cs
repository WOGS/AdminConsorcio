using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Consorcio.Models
{
    public class UnidadModel
    {
        public int IdUnidad { get; set; }
        [Required(ErrorMessage = "Ingresar nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingresar nombre del propietario")]
        public string NombrePropietario { get; set; }

        [Required(ErrorMessage = "Ingresar apellido del propietario")]
        public string ApellidoPropietario { get; set; }

        [Required(ErrorMessage = "Ingresar un Email")]
        [EmailAddress(ErrorMessage ="Ingresar un Email valido")]
        public string EmailPropietario { get; set; }

        [Range(1, 1000, ErrorMessage = "Ingresar una superficie entre 1 y 1000")]
        public string Superficie { get; set; }

    }
}