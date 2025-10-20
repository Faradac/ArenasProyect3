using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArenasProyect3.Modulos.Comercial
{
    public partial class ReportesComercial : Form
    {
        private static readonly HttpClient client = new HttpClient();

        //CONSTRUCTOR DEL MANTENIMIENTO - MANTENIEMINTO
        public ReportesComercial()
        {
            InitializeComponent();
        }

        //PRIMERA CARGA DE MI MANTENIMIENTOS
        private void ReportesComercial_Load(object sender, EventArgs e)
        {

        }

        //REPORTES DE REQEURIEMITNOS---------------------------------------------------------------
        //HABILITAR REQUERIEMINTOS
        private void btnReportesRequerimeinto_Click(object sender, EventArgs e)
        {
            panelReportesRequerimiento.Visible = true;
        }

        //EVENTO ASINCRONICO PARA GENERAR REPORTES EN POWER BI
        private async void btnGenerarReportesPowerBi_Click(object sender, EventArgs e)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Conexion.ConexionMaestra.conexion;
                SqlCommand cmd = new SqlCommand("ReporteComercial_MostrarRequerimientosXGrafico", con);
                cmd.Parameters.AddWithValue("@fechadesde", Desde.Value);
                cmd.Parameters.AddWithValue("@fechahasta", Hasta.Value);
                cmd.CommandType = CommandType.StoredProcedure; 
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                


                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    int aprobados = Convert.ToInt32(row["RequerimientosAprobados"]);
                    int pendientes = Convert.ToInt32(row["RequerimientosPendientes"]);
                    int desaprobados = Convert.ToInt32(row["RequerimientosDesaprobados"]);

                    //OBJETO CON LOS DATOS A ENVIAR A POWER BI
                    var datosPowerBI = new[]
                    {
                new
                {
                    Aprobados = aprobados,
                    Pendientes = pendientes,
                    Desaprobados = desaprobados
                }
            };
                    //URL DEL DATASET PARA LA INSERCION FILAS EN MI DATASET DE POWER BI
                    string urlDataset = "https://api.powerbi.com/beta/b4a40545-7779-4b38-aff7-1f1738f80840/datasets/6db55274-bfeb-453f-bd5d-4a0ecc948d6f/rows?key=l7cpy5YCatGv%2B65yjrqlTx2HTjeCzXczD8B0cUnlnUxgB70ZU2MAcBShGtL%2FGVx%2FYncADLAyDQyZ0yTQ%2BYqEjA%3D%3D";

                    using (HttpClient client = new HttpClient())
                    {
                        //SERIALIZAR LOS DATOS A FORMATO JSON
                        string json = Newtonsoft.Json.JsonConvert.SerializeObject(datosPowerBI);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(urlDataset, content);

                        if (!response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Error al enviar los datos a Power BI: " + response.StatusCode);
                            return;
                        }
                    }

                    // URL DE MI INFORME EN POWER BI
                    string urlPowerBi = "https://app.powerbi.com/groups/me/reports/d937b6bc-baaa-430b-825b-6254fcdd3d08/288507e5e67a51e87ce0?experience=power-bi";

                    //ABRIR NAVEGADOR PREDETERMINADO Y DIRIGIRLO A MI INFORME EN POWER BI
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = urlPowerBi,
                        UseShellExecute = true
                    });

                    MessageBox.Show("Datos enviados correctamente y Power BI abierto.");
                }
                else
                {
                    MessageBox.Show("No hay datos para enviar a Power BI.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }

            //try
            //{
            //DataTable dt = new DataTable();
            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = Conexion.ConexionMaestra.conexion;
            //SqlCommand cmd = new SqlCommand("ReporteComercial_MostrarRequerimientosXGrafico", con);
            //cmd.Parameters.AddWithValue("@fechadesde", Desde.Value);
            //cmd.Parameters.AddWithValue("@fechahasta", Hasta.Value);

            //    cmd.CommandType = CommandType.StoredProcedure;
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    da.Fill(dt);


            //    con.Close();

            //    if (dt.Rows.Count > 0)
            //    {
            //        DataRow row = dt.Rows[0];

            //        int aprobados = Convert.ToInt32(row["RequerimientosAprobados"]);
            //        int pendientes = Convert.ToInt32(row["RequerimientosPendientes"]);
            //        int desaprobados = Convert.ToInt32(row["RequerimientosDesaprobados"]);
            //        int total = aprobados + pendientes + desaprobados;

            //        var datosPowerBi = new[]
            //        {
            //        new
            //        {
            //                 Aprobados = aprobados,
            //                Pendientes = pendientes,
            //                Desaprobados = desaprobados,
            //        }

            //    };

            //        string urlPowerBi = "https://api.powerbi.com/beta/b4a40545-7779-4b38-aff7-1f1738f80840/datasets/6db55274-bfeb-453f-bd5d-4a0ecc948d6f/rows?key=l7cpy5YCatGv%2B65yjrqlTx2HTjeCzXczD8B0cUnlnUxgB70ZU2MAcBShGtL%2FGVx%2FYncADLAyDQyZ0yTQ%2BYqEjA%3D%3D\r\n";

            //        HttpClient client = new HttpClient();

            //        string json = Newtonsoft.Json.JsonConvert.SerializeObject(datosPowerBi);
            //        var content = new StringContent(json, Encoding.UTF8, "application/json");
            //        HttpResponseMessage response = await client.PostAsync(urlPowerBi, content);

            //        if (!response.IsSuccessStatusCode)
            //        {
            //            MessageBox.Show("Error al enviar los datos a Power BI: " + response.StatusCode);
            //        }
            //        else
            //        {
            //            MessageBox.Show("Datos enviados correctamente a Power BI.");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }
    }
}

