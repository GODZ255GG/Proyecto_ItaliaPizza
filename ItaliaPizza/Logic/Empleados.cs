﻿using System.ServiceModel;
using System.Runtime.Serialization;

namespace Logic
{
    [DataContract]
    public class Empleados
    {
        private int _idEmpleados;
        private string _nombre;
        private string _apellidoPaterno;
        private string _apellidoMaterno;
        private string _telefono;
        private string _correo;
        private string _contraseña;
        private string _foto;
        private string _rol;
        private bool _status;

        #region Propiedades
        [DataMember]
        public int IdEmpleados { get { return _idEmpleados; } set { _idEmpleados = value; } }
        [DataMember]
        public string Nombre { get { return _nombre;} set {  _nombre = value; } }
        [DataMember]
        public string ApellidoPaterno { get { return _apellidoPaterno;} set {  _apellidoPaterno = value;} }
        [DataMember]
        public string ApellidoMaterno { get { return _apellidoMaterno;} set { _apellidoMaterno = value;} }
        [DataMember]
        public string  Telefono { get { return _telefono;} set { _telefono = value; } }
        [DataMember]
        public string Correo { get { return _correo; } set { _correo = value; } }
        [DataMember]
        public string Contraseña { get { return _contraseña; } set { _contraseña = value; } }
        [DataMember]
        public string Foto { get { return _foto; } set { _foto = value; } }
        [DataMember]
        public string Rol {  get { return _rol; } set { _rol = value; } }
        [DataMember]
        public bool Status { get { return _status; } set { _status = value; } }
        #endregion
    }
}
