using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    [DataContract]
    public class Recetas
    {
        private int _idRecetas;
        private string _nombre;
        private string _descripcionPreparación;

        #region Propiedades
        [DataMember]
        public int IdRecetas { get { return _idRecetas; } set { _idRecetas = value; } }
        [DataMember]
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        [DataMember]
        public string DescripcionPreparación { get { return _descripcionPreparación; } set { _descripcionPreparación = value; } }
        #endregion
    }
}
