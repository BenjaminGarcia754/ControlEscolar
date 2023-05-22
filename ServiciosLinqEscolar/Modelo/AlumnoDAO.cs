using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosLinqEscolar.Modelo
{
    public static class AlumnoDAO
    {
        public static List<usuario> obtenerUsuarios()
        {
            DataClasesEscolarUVDataContext conexionBD= new DataClasesEscolarUVDataContext("escolaruvConnectionString");

            IQueryable<usuario> usuariosBD = from usuarioQuery in conexionBD.usuario
                                          select usuarioQuery;
            return usuariosBD.ToList();
        }

        public static void iniciarSesion()
        {

        }
    }
}