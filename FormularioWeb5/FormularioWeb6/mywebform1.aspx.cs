using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using FormularioWeb6.ServiceReference1;
using FormularioWeb6.ServiceReference2; //agregar

namespace FormularioWeb6
{
    public partial class mywebform1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
          
        {
            if (!IsPostBack)
            {
                CargarCiudades();
            }
        }
        /* private string[] readFile()
         {
             string[] lines = File.ReadAllLines("D:\\Resources\\UNSA\\CIENCIAS DE LA COMPUTACION\\3ER SEMESTRE\\DESARROLLO BASADO EN PLATAFORMAS\\LISLAB6\\Ciudades.txt");
             return lines;
         }*/
        private void CargarCiudades()
        {
            Service1Client client = new Service1Client();
            string[] Ciudades = client.getCiudades();
            client.Close();

            Array.Sort(Ciudades);
            Ciudad.Items.Clear();
            Ciudad.Items.Add("SELECCIONA UNA OPCION");

            foreach (string s in Ciudades)
            {
                Ciudad.Items.Add(s);
            }
        }


        /* private void serviceCall2()
         {
             Service2Client client = new Service2Client();
             string[] servi2 = ObtenerDatos_Resumen();
             client.ObtenerResumen(servi2);
         }
         private String[] ObtenerDatos_Resumen()
         { 
             string[] inforesumen = new string[7];                           // Creamos un arreglo de 7 elementos para almacenar los datos del formulario
                                                                             // Asignamos los valores de los controles del formulario al arreglo
             inforesumen[0] = "NOMBRE : "+ Server.HtmlEncode(nombre.Text);
             inforesumen[1] = "APELLIDOS: " + Server.HtmlEncode(apellidos.Text);
             inforesumen[2] = "SEXO : " + (masculino.Checked ? "Masculino" : "Femenino");
             inforesumen[3] = "CORREO:"+Server.HtmlEncode(correo.Text);
             inforesumen[4] = "DIRECCION : "+Server.HtmlEncode(direccion.Text);
             inforesumen[5] = "CIUDAD: "+ Server.HtmlEncode(ciudad.SelectedItem.Text);
             inforesumen[6] = "REQUERIMIENTO : "+Server.HtmlEncode(requerimiento.Text);
             return inforesumen;
         }

         protected void EnviarClick(object sender, EventArgs e)
         {
             if (ValidarCampos())
             {                                                                         
                 string[] servi2 = ObtenerDatos_Resumen();                           // Llamamos a ObtenerDatos_Resumen para guardar los datos del formulario
                                                                                     // Construimos el resumen usando un StringBuilder
                 StringBuilder sb = new StringBuilder();
                 sb.Append("RESUMEN DE LOS DATOS LLENADOS:<br />");
                 sb.Append("NOMBRE: " + Server.HtmlEncode(servi2[0]) + "<br />");
                 sb.Append("APELLIDOS: " + Server.HtmlEncode(servi2[1]) + "<br />");
                 sb.Append("SEXO: " + Server.HtmlEncode(servi2[2]) + "<br />");
                 sb.Append("CORREO: " + Server.HtmlEncode(servi2[3]) + "<br />");
                 sb.Append("DIRECCION: " + Server.HtmlEncode(servi2[4]) + "<br />");
                 sb.Append("CIUDAD: " + Server.HtmlEncode(servi2[5]) + "<br />");
                 sb.Append("REQUERIMIENTO : " + Server.HtmlEncode(servi2[6]) + "<br />");
                                                                                         // Continuamos con el código que muestra el resumen en el cuadroResumen
                 cuadroResumen.InnerHtml = sb.ToString();
                 cuadroResumen.Visible = true;

                 serviceCall2();

             }

             else
             { cuadroResumen.Visible = false; }                                          // Si la validación falla, ocultar el cuadro de resumen
         }


       

         private void EliminarInformacionDelFormulario()
         {
             nombre.Text = string.Empty;
             apellidos.Text = string.Empty;
             masculino.Checked = false;
             femenino.Checked = false;
             correo.Text = string.Empty;
             direccion.Text = string.Empty;
             ciudad.ClearSelection();
             requerimiento.Text = string.Empty;

             cuadroResumen.Visible = false;
         }

         protected void Eliminar_Click(object sender, EventArgs e)
         {
             EliminarInformacionDelFormulario();
         }
     }
        */

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(Nombre.Text))
            { return false; }

            if (string.IsNullOrEmpty(Apellidos.Text))
            { return false; }

            if (!(Masculino.Checked || Femenino.Checked))
            { return false; }

            string Correo = this.Correo.Text.Trim();
            string Dominio = Correo.Substring(Correo.LastIndexOf('@') + 1);

            if (Dominio != "unsa.edu.pe")
            { return false; }

            if (Ciudad.SelectedIndex == 0)
            { return false; }

            return true;
        }
        protected void EnviarClick(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                string Nombre = this.Nombre.Text;                                           // Obtener los datos ingresados en el formulario
                string Apellidos = this.Apellidos.Text;
                string Sexo = this.Masculino.Checked ? "Masculino" : "Femenino";
                string Email = this.Correo.Text;
                string Direccion = this.Direccion.Text;
                string Ciudad = this.Ciudad.SelectedValue;
                string Requerimiento = this.Requerimiento.Text;

                IService2 servicio2 = new ServiceReference2.Service2Client();                // Crear una instancia del servicio2 (suponiendo que la interfaz se llama "IService2")               
                servicio2.GuardarInformacion(Nombre, Apellidos, Sexo, Email,
                                      Direccion, Ciudad, Requerimiento);                                              // Llamar al método GuardarInformacion() para guardar los datos en la base de datos              

                HttpCookie cookie = new HttpCookie("UserInfo");
                cookie.Values["Sexo"] = Sexo;
                cookie.Values["Ciudad"] = Ciudad;
                cookie.HttpOnly = false;
                Response.Cookies.Add(cookie);
                createSession(Nombre, Apellidos);
                Response.Redirect("Auxiliar");

            }
            else
            { }

        }


        private void createSession(String Nombre, String Apellidos)
        {
            Session["Nombre"] = Nombre;
            Session["Apellidos"] = Apellidos;
        }

        protected void Eliminar_Click(object sender, EventArgs e)
        {
            EliminarInformacionDelFormulario();
        }

        private void EliminarInformacionDelFormulario()
        {
            Nombre.Text = string.Empty;
            Apellidos.Text = string.Empty;
            Masculino.Checked = false;
            Femenino.Checked = false;
            Correo.Text = string.Empty;
            Direccion.Text = string.Empty;
            Ciudad.ClearSelection();
            Requerimiento.Text = string.Empty;

        }

        [WebMethod]
        public static String getInfo(String Nombre, String Apellidos)
        {
            string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=DBWeb1;Integrated Security=True";

            string query = "SELECT COUNT(*) FROM TablaInformacion2 WHERE Nombre = @Nombre AND Apellidos = @Apellidos";

            int count = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", Nombre);
                    command.Parameters.AddWithValue("@Apellidos", Apellidos);
                    connection.Open();
                    count = (int)command.ExecuteScalar();
                }
            }

            if (count > 0)
            {
                return "El usuario " + Nombre + " " + Apellidos  + " ya está registrado.";
            }
            else
            {
                return "El usuario " + Nombre + " " + Apellidos + " no está registrado.";
            }

        }
    }
}


