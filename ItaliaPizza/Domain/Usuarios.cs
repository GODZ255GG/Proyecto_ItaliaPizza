using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Usuarios
    {
        #region Singletone
        public static Usuarios UsuariosClient { get; set; }
        #endregion

        public int IdUsuarios { get; set; }
        public string Nombre { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public int Telefono { get; set; }
        public string Correo {  get; set; }
        public string Contraseña { get; set; }
        public string Foto { get; set; }
        public string Rol { get; set; }
    }
}
