﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class CorteDeCaja
    {
        private int _idCorteDeCaja;
        private DateTime _fechaCorteDeCaja;
        private double _totalIngresos;
        private double _dineroRestante;
        private string _turno;

        [DataMember]
        public int IdCorteDeCaja { get { return _idCorteDeCaja; } set { _idCorteDeCaja = value; } }
        [DataMember]
        public DateTime FechaCorteDeCaja { get { return _fechaCorteDeCaja; } set { _fechaCorteDeCaja = value; } }
        [DataMember]
        public double TotalIngresos { get { return _totalIngresos; } set { _totalIngresos = value; } }
        [DataMember]
        public double DineroRestante { get { return _dineroRestante; } set { _dineroRestante = value; } }
        [DataMember]
        public string Turno { get { return _turno; } set { _turno = value; } }

    }
}
