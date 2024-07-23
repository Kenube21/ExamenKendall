using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ExamenKendall.CapaVistas
{
    public partial class VotacionesVista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Bingresar_Click(object sender, EventArgs e)
        {
            // Validar si se ingresaron datos en los TextBoxes
            if (string.IsNullOrEmpty(Tid.Text) ||
                string.IsNullOrEmpty(Tcandidato.Text))
            {
                lblMensaje.Text = "Rellenar todos los Espacios";
                return; // Terminar la ejecución del método aquí si no se ingresaron todos los datos
            }

            int votanteId;
            int candidatoId;

            // Validar y convertir los datos de entrada
            if (!int.TryParse(Tid.Text, out votanteId) ||
                !int.TryParse(Tcandidato.Text, out candidatoId))
            {
                lblMensaje.Text = "ID Votante y ID Candidato deben ser números válidos.";
                return;
            }

            // Obtener la cadena de conexión del archivo web.config
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            // Consulta de inserción
            string insertQuery = "INSERT INTO Votos (VotanteID, CandidatoID) VALUES (@VotanteID, @CandidatoID); SELECT SCOPE_IDENTITY();";

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
                        // Insertar datos
                        command.Parameters.AddWithValue("@VotanteID", votanteId);
                        command.Parameters.AddWithValue("@CandidatoID", candidatoId);

                        // Ejecutar el comando y obtener el ID generado
                        int nuevoVotoID = Convert.ToInt32(command.ExecuteScalar());

                        // Confirmar la transacción
                        transaction.Commit();

                        // Mostrar el resultado
                        lblMensaje.Text = $"Voto insertado con ID: {votanteId} y con Candidato ID: {candidatoId}";
                        lblMensaje.Text = $"ID del Voto es: {nuevoVotoID}";

                        // Limpiar los campos del formulario
                        Tid.Text = string.Empty;
                        Tcandidato.Text = string.Empty;
                    }
                    catch (SqlException ex)
                    {
                        // Revertir la transacción en caso de error
                        transaction.Rollback();

                        // Verificar si el error es por clave foránea
                        if (ex.Number == 547) // Código de error para violación de clave foránea
                        {
                            lblMensaje.Text = "El VotanteID o CandidatoID no existe en la base de datos.";
                        }
                        else
                        {
                            lblMensaje.Text = $"Ocurrió un error: {ex.Message}";
                        }
                    }
                }
            }
        }
    }
}