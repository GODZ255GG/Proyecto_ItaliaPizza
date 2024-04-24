//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Insumos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Insumos()
        {
            this.IngredientesReceta = new HashSet<IngredientesReceta>();
            this.InventarioDeInsumo = new HashSet<InventarioDeInsumo>();
            this.ProveedorInsumo = new HashSet<ProveedorInsumo>();
        }
    
        public int idInsumos { get; set; }
        public string nombre { get; set; }
        public string marca { get; set; }
        public string tipo { get; set; }
        public string cantidadDeEmpaque { get; set; }
        public string codigoInsumo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IngredientesReceta> IngredientesReceta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventarioDeInsumo> InventarioDeInsumo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProveedorInsumo> ProveedorInsumo { get; set; }
    }
}
