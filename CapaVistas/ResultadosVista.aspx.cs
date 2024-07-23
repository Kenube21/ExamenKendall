using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamenKendall.CapaVistas
{
    public partial class ResultadosVista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Bingresar_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            string query = @"
        SELECT C.CandidatoID, C.Nombre, COALESCE(R.TotalVotos, 0) AS Votos
        FROM Candidatos C
        LEFT JOIN Resultados R ON C.CandidatoID = R.CandidatoID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dtResultados = new DataTable();
                adapter.Fill(dtResultados);

                // Calcular el total de votos
                int totalVotos = dtResultados.AsEnumerable().Sum(row => row.Field<int>("Votos"));

                if (totalVotos > 0)
                {
                    DataTable dtConPorcentaje = dtResultados.Clone();
                    dtConPorcentaje.Columns.Add("Porcentaje", typeof(double));

                    foreach (DataRow row in dtResultados.Rows)
                    {
                        DataRow newRow = dtConPorcentaje.NewRow();
                        newRow["Nombre"] = row["Nombre"];
                        newRow["Votos"] = row["Votos"];
                        newRow["Porcentaje"] = (row.Field<int>("Votos") / (double)totalVotos) * 100;
                        dtConPorcentaje.Rows.Add(newRow);
                    }

                    // Establecer el origen de datos del GridView
                    GridViewResultados.DataSource = dtConPorcentaje;
                    GridViewResultados.DataBind();

                    // Encontrar el ganador
                    DataRow ganador = dtConPorcentaje.AsEnumerable().OrderByDescending(row => row.Field<int>("Votos")).First();
                    Tganador.Text = ganador.Field<string>("Nombre");
                    Tporcentaje.Text = $"{ganador.Field<double>("Porcentaje"):F2}%";
                    lblMensaje.Text = "Resultados actualizados.";
                }
                else
                {
                    Tganador.Text = "N/A";
                    Tporcentaje.Text = "N/A";
                    lblMensaje.Text = "No se han registrado votos.";
                }
            }
        }
    }
}