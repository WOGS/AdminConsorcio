using ServicioNegocio.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioNegocio.Models
{
    public class ExpensaModel
    {
        public List<Expensa> expensas { get; set; }
        public int gastosActuales { get; set; }
        public int montoPorUnidad { get; set; }
        public int cantidadUnidades { get; set; }
        public int expensaMes { get; set; }
    }
}
