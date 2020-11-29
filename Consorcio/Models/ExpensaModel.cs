using ServicioNegocio.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consorcio.Models
{
    public class ExpensaModel
    {
        public List<Expensa> expensas { get; set; }

        public int gastosActuales { get; set; }
        public int montoPorUnidad { get; set; }
        public int cantidadUnidades { get; set; }
    }
}