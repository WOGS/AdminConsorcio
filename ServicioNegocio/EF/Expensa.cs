//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServicioNegocio.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Expensa
    {
        public int IdExpensa { get; set; }
        public int AnioExpensa { get; set; }
        public int MesExpensa { get; set; }
        public int GastoTotal { get; set; }
        public int ExpensaUnidad { get; set; }
        public int IdConsorcio { get; set; }
    
        public virtual Consorcio Consorcio { get; set; }
    }
}
