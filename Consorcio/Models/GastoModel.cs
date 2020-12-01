using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Consorcio.Models
{
    public class GastoModel
    {
        public int IdGasto { get; set; }

        [Required(ErrorMessage = "El Nombre es requeridooo")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdConsorcio { get; set; }

        [Required(ErrorMessage = "El Tipo de Gasto es requerido")]
        public int IdTipoGasto { get; set; }

        [Required(ErrorMessage = "La Fecha es requerida")]
        public System.DateTime FechaGasto { get; set; }

        [Required(ErrorMessage = "El Año de Expensa es requerido")]
        [Range(2010, 2021, ErrorMessage = "El Año debe estar entre 2010 y 2021")]
        public int AnioExpensa { get; set; }

        [Required(ErrorMessage = "El Mes de Expensa es requerido")]
        [Range(1, 12, ErrorMessage = "El mes debe estar entre 1 y 12")]
        public int MesExpensa { get; set; }
        public string ArchivoComprobante { get; set; }
        [Required(ErrorMessage = "El Monto es requerido")]
        [Range(0, 9999999999999999.99, ErrorMessage = "El Monto debe ser mayor a 0")]
        public decimal Monto { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public int IdUsuarioCreador { get; set; }

    }
}