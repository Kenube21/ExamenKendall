using ExamenKendall.CapaDatos;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace ExamenKendall.CapaVistas
{
    public partial class CandidatosVista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Bingresar_Click(object sender, EventArgs e)
        {
            // Validar si se ingresaron datos en los TextBoxes
            if (string.IsNullOrEmpty(Tcandidato.Text) ||
                string.IsNullOrEmpty(Tnombre.Text)    ||
                string.IsNullOrEmpty(Tapellido1.Text) ||
                string.IsNullOrEmpty(Tapellido2.Text) ||
                string.IsNullOrEmpty(Tpartido.Text))
            {
                lblMensaje.Text = "Rellenar todos los Espacios";
                return; // Terminar la ejecución del método aquí si no se ingresaron todos los datos
            }

            // Validar si el ID es un número entero válido
            if (!int.TryParse(Tcandidato.Text, out int id))
            {
                lblMensaje.Text = "El ID debe ser un número entero.";
                return;
            }

            // Obtener los valores de los TextBoxes
            Candidatos candidatos = new Candidatos()
            {
                Id = id,
                Nombre = Tnombre.Text,
                Apellido1 = Tapellido1.Text,
                Apellido2 = Tapellido2.Text,
                Partido = Tpartido.Text
            };

            // Obtener la cadena de conexión del archivo web.config
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            // Consulta de inserción
            string enableIdentityInsertQuery = "SET IDENTITY_INSERT Candidatos ON;";
            string insertQuery = "INSERT INTO Candidatos (CandidatoID, Nombre, Apellido1, Apellido2, Partido ) VALUES (@CandidatoID, @Nombre, @Apellido1, @Apellido2, @Partido)";
            string disableIdentityInsertQuery = "SET IDENTITY_INSERT Candidatos OFF;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir la conexión
                connection.Open();
                // Crear el comando
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    SqlTransaction transaction = connection.BeginTransaction();
                    command.Transaction = transaction;

                    try
                    {
                        // Habilitar IDENTITY_INSERT
                        command.CommandText = enableIdentityInsertQuery;
                        command.ExecuteNonQuery();

                        // Insertar datos
                        command.CommandText = insertQuery;
                        command.Parameters.AddWithValue("@CandidatoID", candidatos.Id);
                        command.Parameters.AddWithValue("@Nombre", candidatos.Nombre);
                        command.Parameters.AddWithValue("@Apellido1", candidatos.Apellido1);
                        command.Parameters.AddWithValue("@Apellido2", candidatos.Apellido2);
                        command.Parameters.AddWithValue("@Partido", candidatos.Partido);
                        command.ExecuteNonQuery();

                        // Deshabilitar IDENTITY_INSERT
                        command.CommandText = disableIdentityInsertQuery;
                        command.ExecuteNonQuery();

                        // Confirmar la transacción
                        transaction.Commit();

                        // Mostrar el resultado
                        lblMensaje.Text = $"Candidato insertado con ID: {candidatos.Id}";
                    }
                    catch (Exception ex)
                    {
                        // Revertir la transacción en caso de error
                        transaction.Rollback();
                        lblMensaje.Text = $"Ocurrió un error: {ex.Message}";
                    }
                }
            }
        }
    }
}