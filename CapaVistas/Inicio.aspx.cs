using ExamenKendall.CapaDatos;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ExamenKendall.CapaVistas
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Bingresar_Click(object sender, EventArgs e)
        {
            // Validar si se ingresaron datos en los TextBoxes
            if (string.IsNullOrEmpty(Tid.Text) ||
                string.IsNullOrEmpty(Tnombre.Text) ||
                string.IsNullOrEmpty(Tapellido1.Text) ||
                string.IsNullOrEmpty(Tapellido2.Text) ||
                string.IsNullOrEmpty(Tcorreo.Text))
            {
                lblMensaje.Text = "Rellenar todos los Espacios";
                return; // Terminar la ejecución del método aquí si no se ingresaron todos los datos
            }

            // Validar si el ID es un número entero válido
            if (!int.TryParse(Tid.Text, out int id))
            {
                lblMensaje.Text = "El ID debe ser un número entero.";
                return;
            }

            // Obtener los valores de los TextBoxes
            Votantes votantes = new Votantes()
            {
                Id = id,
                Nombre = Tnombre.Text,
                Apellido1 = Tapellido1.Text,
                Apellido2 = Tapellido2.Text,
                Correo = Tcorreo.Text
            };

            // Obtener la cadena de conexión del archivo web.config
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            // Consulta de inserción
            string enableIdentityInsertQuery = "SET IDENTITY_INSERT Votantes ON;";
            string insertQuery = "INSERT INTO Votantes (VotanteID, Nombre, Apellido1, Apellido2, Correo ) VALUES (@VotanteID, @Nombre, @Apellido1, @Apellido2, @Correo)";
            string disableIdentityInsertQuery = "SET IDENTITY_INSERT Votantes OFF;";

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
                        command.Parameters.AddWithValue("@VotanteID", votantes.Id);
                        command.Parameters.AddWithValue("@Nombre", votantes.Nombre);
                        command.Parameters.AddWithValue("@Apellido1", votantes.Apellido1);
                        command.Parameters.AddWithValue("@Apellido2", votantes.Apellido2);
                        command.Parameters.AddWithValue("@Correo", votantes.Correo);
                        command.ExecuteNonQuery();

                        // Deshabilitar IDENTITY_INSERT
                        command.CommandText = disableIdentityInsertQuery;
                        command.ExecuteNonQuery();

                        // Confirmar la transacción
                        transaction.Commit();

                        // Mostrar el resultado
                        lblMensaje.Text = $"Votante insertado con ID: {votantes.Id}";
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