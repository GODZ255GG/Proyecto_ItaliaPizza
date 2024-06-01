

namespace Domain
{
    public class Producto
    {
        #region Singletone
        public static Producto ProductoClient { get; set; }
        #endregion

        public int IdProductos { get; set; }
        public string Nombre { get; set; }
        public string CodigoProducto { get; set; }
        public string Marca { get; set; }
        public int CantidadTotal { get; set; }
    }
}
