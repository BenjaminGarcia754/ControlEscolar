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

        public static Mensaje iniciarSesion(string username, string password)
        {
            Mensaje nuevoMensaje = new Mensaje();
            try
            {
                DataClasesEscolarUVDataContext conexionBD = obtenerCadenaConexion();
                var usuarioLogin = (from usuarioQuery in conexionBD.usuario
                                    where usuarioQuery.username == username &&
                                          usuarioQuery.password == password
                                    select usuarioQuery).FirstOrDefault();
                nuevoMensaje.usuario = usuarioLogin;
                nuevoMensaje.confirmacion = true;
                nuevoMensaje.mensaje = "Usuario encontrado correctamente";
            }
            catch (Exception ex)
            {
                nuevoMensaje.confirmacion=false;
                nuevoMensaje.mensaje = "El usuario " + username + "no se ha podido encontrar";
            }
           
            return nuevoMensaje;
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

        public static Boolean EditarUsuario(usuario usuarioEdicion)
        {
            bool cambiosCorrectos = false;
            try
            {
                DataClasesEscolarUVDataContext conexionBD = obtenerCadenaConexion();

                usuario usuarioTemporal = (from usuarioQuery in conexionBD.usuario
                                           where usuarioEdicion.idUsuario == usuarioQuery.idUsuario
                                           select usuarioQuery).FirstOrDefault();

                if (usuarioTemporal != null)
                {
                    usuarioTemporal.nombre = usuarioEdicion.nombre;
                    usuarioTemporal.apellidoPaterno = usuarioEdicion.apellidoPaterno;
                    usuarioTemporal.apellidoMaterno = usuarioEdicion.apellidoMaterno;
                    usuarioTemporal.password = usuarioEdicion.password;
                    conexionBD.SubmitChanges();
                    cambiosCorrectos = true;
                }
            }
            catch (Exception)
            {
                cambiosCorrectos = false;
            }
            
            return cambiosCorrectos;
        }

        public static Boolean EliminarUsuario(int idUsuario)
        {
            bool cambiosCorrectos = false;
            try
            {
                DataClasesEscolarUVDataContext conexionBD = obtenerCadenaConexion();
                usuario usuarioAEliminar = (from usuario in conexionBD.usuario
                                            where usuario.idUsuario == idUsuario
                                            select usuario).FirstOrDefault();

                if (usuarioAEliminar!=null)
                {
                    conexionBD.usuario.DeleteOnSubmit(usuarioAEliminar);
                    conexionBD.SubmitChanges();
                    cambiosCorrectos = true;
                }
            }
            catch (Exception)
            {
                cambiosCorrectos = false;
            }
            return cambiosCorrectos;
        }

    }
}