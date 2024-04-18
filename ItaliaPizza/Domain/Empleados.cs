
namespace Domain
{
    public class Empleados
    {
        #region Singletone
        public static Empleados EmpleadosClient { get; set; }
        #endregion

        public int IdEmpleados { get; set; }
        public string Nombre { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public string Telefono { get; set; }
        public string Correo {  get; set; }
        public string Contraseña { get; set; }
        public string Foto { get; set; }
        public string Rol { get; set; }
    }
}
