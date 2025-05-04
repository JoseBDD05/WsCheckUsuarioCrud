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
using System.Text;

namespace wsCheckUsuario
{
	public partial class Formulario_web2 : System.Web.UI.Page
	{
		protected async void Page_Load(object sender, EventArgs e)
		{
            //Validacion de 1er carga de pagina (postback)
            if(Page.IsPostBack == false)
            {
                //Llamada para ejecucion del metodo
                await cargaDatosTipoUsuario();
            }
		}
        // Creación del método asíncrono para ejecutar el
        // endpoint vwTipoUsuario
        private async Task cargaDatosTipoUsuario()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Configuración de la peticion HTTP
                    string apiUrl = "https://localhost:44311/check/usuario/vwtipousuario";
                    // Ejecución del endpoint
                    HttpResponseMessage respuesta = await client.GetAsync(apiUrl);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();

                    // Validación del estatus OK
                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado = await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);
                        // ------------------------------------------
                        JArray jsonArray = (JArray)objRespuesta.datos["vwtipousuario"];
                        // Convertir JArray a DataTable
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
                        // -------------------------------------------
                        // Visualización de los datos formateados DropDownList
                        DropDownList1.DataSource = dt;
                        DropDownList1.DataTextField = "descripcion";
                        DropDownList1.DataValueField = "clave";
                        DropDownList1.DataBind();
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>" +
                                       "alert('Error de conexión con el servicio');" +
                                       "</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>" +
                               "alert('Error de la aplicación, intentar nuevamente');" +
                               "</script>");
            }
        }
        // Creación del método asíncrono para ejecutar el
        // endpoint spInsUsuario
        private async Task cargaDatos()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Configuración del Json que se enviará
                    String data = @"{
                                  ""nombre"":""" + TextBox2.Text + "\"," +
                                  "\"apellidoPaterno\":\"" + TextBox3.Text + "\"," +
                                  "\"apellidoMaterno\":\"" + TextBox4.Text + "\"," +
                                  "\"usuario\":\"" + TextBox5.Text + "\"," +
                                  "\"contrasena\":\"" + TextBox6.Text + "\"," +
                                  "\"ruta\":\"" + TextBox7.Text + "\"," +
                                  "\"tipo\":\"" + DropDownList1.SelectedValue + "\"" +
                                  "}";
                    // Configuración del contenido del <body> a enviar
                    HttpContent contenido = new StringContent
                                (data, Encoding.UTF8, "application/json");
                    // Ejecución de la petición HTTP
                    string apiUrl = "https://localhost:44311/check/usuario/spInsUsuario";
                    // ----------------------------------------------
                    HttpResponseMessage respuesta =
                        await client.PostAsync(apiUrl, contenido);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();
                    // ---------------------------------------------------

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado =
                                await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        // Bandera de estatus del proceso
                        if (objRespuesta.ban == 0)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('Usuario registrado exitosamente');" +
                                           "</script>");
                            Response.Write("<script language='javascript'>" +
                                           "document.location.href='Formulario web2.aspx';" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 1)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El nombre de usuario ya existe');" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 2)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El usuario ya existe');" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 3)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El tipo de usuario no existe');" +
                                           "</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>" +
                                       "alert('Error de conexión con el servicio');" +
                                       "</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>" +
                               "alert('Error de la aplicación, intentar nuevamente');" +
                               "</script>");
            }
        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox2.Text == "")
            {
                Response.Write("<script language='javascript'>" +
                               "alert('El nombre esta vacio');" +
                               "</script>");
            }
            else
            {
                if (TextBox3.Text == "")
                {
                    Response.Write("<script language='javascript'>" +
                                   "alert('El Apellido paterno esta vacio');" +
                                   "</script>");
                }
                else
                {
                    if (TextBox4.Text == "")
                    {
                        Response.Write("<script language='javascript'>" +
                                       "alert('El Apellido Materno esta vacio');" +
                                       "</script>");
                    }
                    else
                    {
                        if (TextBox5.Text == "")
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El usuario esta vacio');" +
                                           "</script>");
                        }
                        else
                        {
                            if (TextBox6.Text == "")
                            {
                                Response.Write("<script language='javascript'>" +
                                               "alert('La contraseña esta vacio');" +
                                               "</script>");
                            }
                            else
                            {
                                if (TextBox7.Text == "")
                                {
                                    Response.Write("<script language='javascript'>" +
                                                   "alert('La ruta de la foto esta vacio');" +
                                                   "</script>");
                                }else
                                {
                                    await cargaDatos();
                                }
                            }
                            }

                        }
                }
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Código que manejará el evento
        }

        protected async void Button3_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "")
            {
                Response.Write("<script language='javascript'>alert('Debe ingresar una clave para eliminar');</script>");
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string clave = TextBox1.Text;
                    string apiUrl = $"https://localhost:44311/check/usuario/spDelUsuario?clave={clave}";

                    HttpResponseMessage respuesta = await client.GetAsync(apiUrl);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado = await respuesta.Content.ReadAsStringAsync();
                        clsApiStatus objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        if (objRespuesta.datos != null && objRespuesta.datos["mensaje"] != null)
                        {
                            string mensaje = objRespuesta.datos["mensaje"].ToString();

                            if (mensaje == "1")
                            {
                                Response.Write("<script language='javascript'>alert('El usuario que se quiere eliminar por la clave no existe');</script>");
                            }
                            else if (mensaje == "0")
                            {
                                Response.Write("<script language='javascript'>alert('El registro fue correctamente eliminado');</script>");

                                // Opcional: Limpiar los campos del formulario
                                TextBox1.Text = "";
                                TextBox2.Text = "";
                                TextBox3.Text = "";
                                TextBox4.Text = "";
                                TextBox5.Text = "";
                                TextBox6.Text = "";
                                TextBox7.Text = "";
                                DropDownList1.ClearSelection();
                            }
                            else
                            {
                                Response.Write("<script language='javascript'>alert('Respuesta desconocida del servicio');</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script language='javascript'>alert('Error al procesar la respuesta del servicio');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>alert('Error de conexión con el servicio');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>alert('Error inesperado: " + ex.Message + "');</script>");
            }
        }


        protected async void Button4_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "")
            {
                Response.Write("<script language='javascript'>alert('Debe ingresar una clave');</script>");
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string clave = TextBox1.Text;
                    string apiUrl = "https://localhost:44311/check/usuario/spBuscarUsuarioPorClave?clave=" + clave;
                    HttpResponseMessage respuesta = await client.GetAsync(apiUrl);

                    clsApiStatus objRespuesta = new clsApiStatus();

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado = await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        if (objRespuesta.ban == 1 && objRespuesta.datos["mensaje"] != null &&
                            objRespuesta.datos["mensaje"].ToString() == "NO_EXISTE")
                        {
                            Response.Write("<script language='javascript'>alert('El usuario que se quiere buscar por la clave no existe');</script>");
                        }
                        else
                        {
                            // Llenar los campos con los datos encontrados
                            TextBox2.Text = objRespuesta.datos["nombre"].ToString();
                            TextBox3.Text = objRespuesta.datos["apellidoPaterno"].ToString();
                            TextBox4.Text = objRespuesta.datos["apellidoMaterno"].ToString();
                            TextBox5.Text = objRespuesta.datos["usuario"].ToString();
                            TextBox6.Text = objRespuesta.datos["contrasena"].ToString();
                            TextBox7.Text = objRespuesta.datos["ruta"].ToString();

                            // Seleccionar el tipo en el DropDownList
                            string tipoUsuario = objRespuesta.datos["tipo"].ToString();
                            DropDownList1.SelectedValue = tipoUsuario;
                        }
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>alert('Error al conectar con el servicio');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>alert('Error inesperado: " + ex.Message + "');</script>");
            }
        }

        // Función que ejecuta la solicitud HTTP para modificar el usuario
        private async Task modificaDatos()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Configuración del Json que se enviará para modificar el usuario
                    string data = @"{
                ""cve"": """ + TextBox1.Text + "\"," +   // Asumiendo que TextBox1 es para la clave (cve) que no se modifica
                        "\"nombre\":\"" + TextBox2.Text + "\"," +
                        "\"apellidoPaterno\":\"" + TextBox3.Text + "\"," +
                        "\"apellidoMaterno\":\"" + TextBox4.Text + "\"," +
                        "\"usuario\":\"" + TextBox5.Text + "\"," +
                        "\"contrasena\":\"" + TextBox6.Text + "\"," +
                        "\"ruta\":\"" + TextBox7.Text + "\"," +
                        "\"tipo\":\"" + DropDownList1.SelectedValue + "\"" +
                        "}";

                    // Configuración del contenido del <body> a enviar
                    HttpContent contenido = new StringContent(data, Encoding.UTF8, "application/json");

                    // Ejecución de la petición HTTP
                    string apiUrl = "https://localhost:44311/check/usuario/spUpdUsuario";  // Ajusta la URL si es necesario

                    HttpResponseMessage respuesta = await client.PostAsync(apiUrl, contenido);

                    // Validación de recepción de respuesta JSON
                    clsApiStatus objRespuesta = new clsApiStatus();

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado = await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        // Bandera de estatus del proceso
                        if (objRespuesta.ban == 0)
                        {
                            // Usuario modificado exitosamente
                            Response.Write("<script language='javascript'>" +
                                           "alert('Usuario modificado exitosamente');" +
                                           "</script>");
                            Response.Write("<script language='javascript'>" +
                                           "document.location.href='Formulario web2.aspx';" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 1)
                        {
                            // El usuario no se encontró
                            Response.Write("<script language='javascript'>" +
                                           "alert('El nombre de usuario no existe');" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 2)
                        {
                            // El nombre de usuario ya existe
                            Response.Write("<script language='javascript'>" +
                                           "alert('El usuario ya existe');" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 3)
                        {
                            // El tipo de usuario no existe
                            Response.Write("<script language='javascript'>" +
                                           "alert('El tipo de usuario no existe');" +
                                           "</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>" +
                                       "alert('Error de conexión con el servicio');" +
                                       "</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>" +
                               "alert('Error de la aplicación, intentar nuevamente');" +
                               "</script>");
            }
        }

        // Manejo del click del botón "Modificar" (Button2_Click)
        protected async void Button2_Click(object sender, EventArgs e)
        {
            // Validación de los campos
            if (TextBox2.Text == "")
            {
                Response.Write("<script language='javascript'>" +
                               "alert('El nombre está vacío');" +
                               "</script>");
            }
            else if (TextBox3.Text == "")
            {
                Response.Write("<script language='javascript'>" +
                               "alert('El apellido paterno está vacío');" +
                               "</script>");
            }
            else if (TextBox4.Text == "")
            {
                Response.Write("<script language='javascript'>" +
                               "alert('El apellido materno está vacío');" +
                               "</script>");
            }
            else if (TextBox5.Text == "")
            {
                Response.Write("<script language='javascript'>" +
                               "alert('El usuario está vacío');" +
                               "</script>");
            }
            else if (TextBox6.Text == "")
            {
                Response.Write("<script language='javascript'>" +
                               "alert('La contraseña está vacía');" +
                               "</script>");
            }
            else if (TextBox7.Text == "")
            {
                Response.Write("<script language='javascript'>" +
                               "alert('La ruta de la foto está vacía');" +
                               "</script>");
            }
            else
            {
                // Si todo está validado correctamente, realizamos la solicitud para modificar el usuario
                await modificaDatos();
            }
        }



    }
}