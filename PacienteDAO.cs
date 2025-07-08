using System;
using System.Data.SqlClient;

namespace Medico
{
    public class PacienteDAO
    {
        private readonly string cadenaConexion = Conexion.Cadena;

        public void InsertarPaciente(string nombre, DateTime fechaNacimiento, string dni, string sexo, string direccion, string telefono)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand("InsertarPaciente", conexion))
                    {
                        comando.CommandType = System.Data.CommandType.StoredProcedure;

                        comando.Parameters.AddWithValue("@Nombre", nombre);
                        comando.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                        comando.Parameters.AddWithValue("@Dni", dni);
                        comando.Parameters.AddWithValue("@Sexo", sexo);
                        comando.Parameters.AddWithValue("@Direccion", direccion);
                        comando.Parameters.AddWithValue("@Telefono", telefono);

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar paciente: " + ex.Message);
            }
        }
    }
}
