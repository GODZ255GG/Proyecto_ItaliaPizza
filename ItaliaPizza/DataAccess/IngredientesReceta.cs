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
    
    public partial class IngredientesReceta
    {
        public int idIngredientesReceta { get; set; }
        public int Insumos_idInsumos { get; set; }
        public int Recetas_idRecetas { get; set; }
    
        public virtual Insumos Insumos { get; set; }
        public virtual Receta Receta { get; set; }
    }
}
