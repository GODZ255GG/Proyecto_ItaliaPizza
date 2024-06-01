

namespace Domain
{
    public class Insumo
    {
        #region Singletone
        public static Insumo InsumoClient { get; set; }
        #endregion
        public int IdInsumos { get; set; }
        public string Nombre { get; set; }
        public string CantidadDeEmpaque { get; set; }
        public string Marca { get; set; }
        public int CantidadTotal { get; set; }


    }
}
