//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServicioModelo.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Consorcio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Consorcio()
        {
            this.Gasto = new HashSet<Gasto>();
            this.Unidad = new HashSet<Unidad>();
        }
    
        public int IdConsorcio { get; set; }
        public string Nombre { get; set; }
        public int IdProvincia { get; set; }
        public string Ciudad { get; set; }
        public string Calle { get; set; }
        public int Altura { get; set; }
        public int DiaVencimientoExpensas { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public Nullable<int> IdUsuarioCreador { get; set; }
    
        public virtual Provincia Provincia { get; set; }
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gasto> Gasto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unidad> Unidad { get; set; }
    }
}