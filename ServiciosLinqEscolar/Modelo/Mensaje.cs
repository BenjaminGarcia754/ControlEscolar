using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosLinqEscolar.Modelo
{
    public class Mensaje
    {
        public Mensaje() { }
        public bool confirmacion { get; set; }
        public string mensaje { get; set; }
        public usuario usuario { get; set;}
    }
}