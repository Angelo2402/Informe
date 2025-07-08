using System;
using System.Data.SqlClient;

namespace Medico
{
    public class UsuarioDAO
    {
        private readonly string cadenaConexion = Conexion.Cadena;

        public bool ValidarUsuario(string usuario, string clave)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand("ValidarUsuario", conexion))
                    {
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Usuario", usuario);
                        comando.Parameters.AddWithValue("@Clave", clave);

                        int resultado = (int)comando.ExecuteScalar();
                        return resultado > 0;
                    }
                }
            }
            catch (Exception)
            {
                throw; // Manejo fuera de la UI
            }
        }
    }
}
