using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosLinqEscolar.Modelo
{
    public static class UsuarioDAO
    {
        public static DataClasesEscolarUVDataContext obtenerCadenaConexion()
        {
            DataClasesEscolarUVDataContext conexionBD =
                new DataClasesEscolarUVDataContext(global::System.Configuration.
                ConfigurationManager.ConnectionStrings["escolaruvConnectionString"].ConnectionString);
            return conexionBD;
        }
        public static List<usuario> obtenerUsuarios()
        {

            DataClasesEscolarUVDataContext conexionBD = obtenerCadenaConexion();

            IQueryable<usuario> usuariosBD = from usuarioQuery in conexionBD.usuario
                                          select usuarioQuery;
            return usuariosBD.ToList();
        }

        public static usuario iniciarSesion(string username, string password)
        {
            DataClasesEscolarUVDataContext conexionBD = obtenerCadenaConexion();

            
            var usuarioLogin = (from usuarioQuery in conexionBD.usuario
                                where usuarioQuery.username == username && 
                                      usuarioQuery.password == password
                                select usuarioQuery).FirstOrDefault();
            return usuarioLogin;
        }

        public static Boolean GuardarUsuario(usuario UsuarioNuevo)
        {
            try
            {
                DataClasesEscolarUVDataContext conexionBD = obtenerCadenaConexion();

                var usuario = new usuario()
                {
                    nombre = UsuarioNuevo.nombre,
                    apellidoPaterno = UsuarioNuevo.apellidoPaterno,
                    apellidoMaterno = UsuarioNuevo.apellidoMaterno,
                    username = UsuarioNuevo.username,
                    password = UsuarioNuevo.password
                };
                conexionBD.usuario.InsertOnSubmit(usuario);
                conexionBD.SubmitChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}