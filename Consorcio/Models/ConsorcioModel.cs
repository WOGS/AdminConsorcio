using ServicioNegocio.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Consorcio.Models
{
    public class ConsorcioModel
    {
        public int idProvincia { get; set; }

        [Required(ErrorMessage = "Ingresar nombre")]
         public string Nombre { get; set; }

        [Required]
        public int Provincia { get; set; }

        [Required(ErrorMessage = "Ingresar ciudad")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "Ingresar calle")]
        public string Calle { get; set; }

        [Required(ErrorMessage = "Ingresar altura")]
        public int Altura { get; set; }

        [Required(ErrorMessage = "Ingresar dia de vencimiento de expensas")]
        [Range(1, 31, ErrorMessage = "Ingresar una fecha correcto")]
        public int DiaVencimientoExpensas { get; set; }

        public List<Provincia> provincias { get; set; }

    }
}