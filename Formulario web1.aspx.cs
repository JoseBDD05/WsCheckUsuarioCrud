using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wsCheckUsuario.Models;

namespace wsCheckUsuario
{
	public partial class Formulario_web1 : System.Web.UI.Page
	{
		protected async void Page_Load(object sender, EventArgs e)
		{
            // Configurar el evento PageIndexChanging del GridView 2
            GridView2.PageIndexChanging += GridView2_PageIndexChanging;

            await cargaDatosApi();
        }

        private void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Actualizar el indice de la pagina del gridview
            // Actualizar los datos del GridView
            GridView2.PageIndex = e.NewPageIndex;
            GridView2.DataBind();
            //throw new NotImplementedException();
        }

        // Metodo asincrono para ejecutar vwRptUsuario
        private async Task cargaDatosApi()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Ejecución de la petición de un endpoint a una webApi
                    string apiUrl = "https://localhost:44311/check/usuario/vwrptusuario?filtro=" + TextBox1.Text;
                    HttpResponseMessage respuesta =
                                    await client.GetAsync(apiUrl);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();

                    // Validación de estado de ejecución
                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado = await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);
                        // ------------------------------------------
                        JArray jsonArray = (JArray)objRespuesta.datos["vwRptUsuario"];
                        // Convertir JArray a DataTable
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
                        // -------------------------------------------
                        GridView2.DataSource = dt;
                        GridView2.DataBind();
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>alert('Error de conexión con webapi');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>alert('Error inesperado ...');</script>");
            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}